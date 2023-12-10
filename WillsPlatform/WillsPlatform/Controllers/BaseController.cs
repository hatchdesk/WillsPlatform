using Microsoft.AspNetCore.Mvc;
using WillsPlatform.Web.Models;

namespace WillsPlatform.Web.Controllers
{
    public class BaseController : Controller
    {
        protected List<Breadcrumb> InitializeBreadcrumbsList()
        {
            var breadcrumbs = new List<Breadcrumb>()
            {
                new("Home", "Home", "Index")
            };

            return breadcrumbs;
        }

        protected string GetCurrentControllerName()
        {
            return ControllerContext.RouteData.Values["controller"]?.ToString();
        }
    }
}
