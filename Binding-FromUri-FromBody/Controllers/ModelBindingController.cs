using System;
using System.Web.Http;
using Binding_FromUri_FromBody.Models;

namespace Binding_FromUri_FromBody.Controllers
{
    public class ModelBindingController : ApiController
    {
        public IHttpActionResult Get([FromUri] GeoPoint location)
        {
            if (location.Latitude == 0 || location.Longitude == 0)
            {
                return BadRequest("Location is empty!");
            }

            return Ok($"Latitude:{location.Latitude},Longitude:{location.Longitude}");
        }

        public IHttpActionResult Post([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name is empty!");
            }

            return Ok($"Hello {name}");
        }
    }
}
