using System.Web;
using System.Web.Mvc;

namespace Binding_FromUri_FromBody
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
