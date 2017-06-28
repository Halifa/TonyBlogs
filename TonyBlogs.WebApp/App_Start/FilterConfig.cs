using System.Web;
using System.Web.Mvc;
using TonyBlogs.WebApp.Filters;

namespace TonyBlogs.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExcepFilter());
        }
    }
}