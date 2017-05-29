using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Binding_ParameterBinding.Controllers
{
    public class ModelBindingController : ApiController
    {
        [HttpGet]
        public string SampleBinding(int id)
        {
            return id.ToString();
        }

        [HttpGet]
        public string OptionParams(int? id = null)
        {
            return id == null ? "id should not be null" : id.ToString();
        }

        //可同時支援get post
        [HttpGet, HttpPost]
        public string TwoMethod(int id)
        {
            return id.ToString();
        }

        [HttpPost]
        public string IdFromBody([FromBody]int id)
        {
            return id.ToString();
        }

        //Get:無法取得frombody的值
        [HttpGet, HttpPost]
        public string IdUriNameBody(int id, [FromBody]string name)
        {
            return $"Id:{id}, name:{name}";
        }

        [HttpPost]
        public string Customer(Customer customer)
        {
            return customer.ToString();
        }

        //web api 無法透過 POST body 來繫結兩個或以上的參數
        [HttpPost]
        public string MultiCustomer(Customer customer1, Customer customer2)
        {
            return $"{customer1.ToString()}, {customer2.ToString()}";
        }
    }
}
