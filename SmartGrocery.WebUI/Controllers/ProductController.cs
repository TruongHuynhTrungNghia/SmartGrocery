using Autofac.Features.AttributeFilters;
using SmartGrocery.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Web.Mvc;

namespace SmartGrocery.WebUI.Features.Product
{
    public class ProductController : Controller
    {
        private readonly HttpClient httpClient;

        public ProductController()
        {
        }

        public ProductController([KeyFilter("SmartGroceryApi")]HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        [HttpGet]
        public ActionResult Product(CancellationToken cancellationToken)
        {
            var id = new Guid();
            var contract = httpClient.GetAsync($"products/{id}", cancellationToken);
            var viewModel = new List<ProductBaseViewModel>();
            
            return View(viewModel);
        }
    }
}