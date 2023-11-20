using Microsoft.AspNetCore.Mvc;

namespace WillsPlatform.Web.Controllers
{
    public class ManageController : Controller
    {
        public IActionResult Questionnaires()
        {
            return View();
        }
    }
}
