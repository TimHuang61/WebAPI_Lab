using Models.Northwind;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using WebGrease.Css.Extensions;

namespace Binding_CSVMediaFormatter.Formatters
{
    //繼承 BufferedMediaTypeFormatter
    public class ProductCsvFormatter : BufferedMediaTypeFormatter
    {
        public ProductCsvFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == typeof(Product))
            {
                return true;
            }
            else
            {
                return typeof(IEnumerable<Product>).IsAssignableFrom(type);
            }
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using (var writer = new StreamWriter(writeStream))
            {
                var products = value as IEnumerable<Product>;
                if (products != null)
                {
                    products.ForEach(p => WriteItem(p, writer));
                }
                
                var singleProduct = value as Product;
                if (singleProduct == null)
                {
                    throw new InvalidOperationException("Cannot serialize type");
                }

                WriteItem(singleProduct, writer);
            }
        }

        private void WriteItem(Product product, StreamWriter writer)
        {
            writer.WriteLine($"{Escape(product.ProductID)},{Escape(product.ProductName)},{Escape(product.CategoryID)},{Escape(product.UnitPrice)}");
        }

        static char[] specialChars = { ',', '\n', '\r', '"' };

        private string Escape(object o)
        {
            if (o == null)
            {
                return string.Empty;
            }

            string field = o.ToString();
            if (field.IndexOfAny(specialChars) != -1)
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }

            return field;
        }
    }
}