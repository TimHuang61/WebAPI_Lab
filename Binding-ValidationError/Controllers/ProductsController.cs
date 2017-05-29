using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Binding_ValidationError.Filters;
using Models;

namespace Binding_ValidationError.Controllers
{
    public class ProductsController : ApiController
    {
        //[ModelValidationFilter]
        public IHttpActionResult Post(Product product)
        {
            return Ok(product);
        }
    }
}
