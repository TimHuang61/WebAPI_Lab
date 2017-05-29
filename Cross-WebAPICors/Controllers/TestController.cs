using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Cross_WebAPICors.Controllers
{
    //Package Manager Console: Install-Package Microsoft.AspNet.WebApi.Cors
    //修改 App_Start/WebApiConfig.cs => config.EnableCors();
    //o注意: origins: 指定的 URL 最後不要有"/"，不然會請求不到。
    [EnableCors(origins: "http://localhost:3903", headers: "*", methods: "*")] 
    //使用 EnableCors 會有點問題 (methods="" or method="GET")default post、get 都會開啟
    //[EnableCors(origins: "http://localhost:3903", headers: "*", methods: "")]
    //[EnableCors(origins: "http://localhost:3903", headers: "*", methods: "GET")]
    public class TestController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("Get from test.")
            };
        }

        public HttpResponseMessage Post()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("Post from test.")
            };
        }

        public HttpResponseMessage Put()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("Put from test.")
            };
        }

        public HttpResponseMessage Delete()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("Delete from test.")
            };
        }
    }
}
