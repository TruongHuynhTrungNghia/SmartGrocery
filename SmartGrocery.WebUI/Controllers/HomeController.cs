using System.Web.Mvc;

namespace SmartGrocery.WebUI.Home
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}