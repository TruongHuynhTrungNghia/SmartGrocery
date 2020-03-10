using SmartGrocery.WebUI.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SmartGrocery.WebUI.Features.Product
{
    public class ProductController : Controller
    {
        public ActionResult Product()
        {
            var viewModel = new List<ProductBaseViewModel>();
            return View(viewModel);
        }
    }
}