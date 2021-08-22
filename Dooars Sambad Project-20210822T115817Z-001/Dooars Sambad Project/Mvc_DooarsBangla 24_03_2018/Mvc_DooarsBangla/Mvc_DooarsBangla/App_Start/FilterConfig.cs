using System.Web;
using System.Web.Mvc;

namespace Mvc_DooarsBangla
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}