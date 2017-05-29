using System.Linq;
using System.Web.Http;

using Models.Northwind;

namespace Model_PartialClass.Controllers
{
    public class ProductsController : ApiController
    {
        //測試MetadataType
        //會拋出  The ProductName field is required.
        //testdata:
        //{
        // "ProductID":1,
        // "QuantityPerUnit":"test"
        //}
        [HttpPost]
        public IHttpActionResult Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return Ok(string.Join(",", ModelState.Values.SelectMany(m => m.Errors.Select(e => e.ErrorMessage))));
            }

            return Ok("ok");
        }
    }
}
