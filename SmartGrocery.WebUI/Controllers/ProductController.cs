using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using SmartGrocery.WebApi.Contracts.BaseProduct;
using SmartGrocery.WebUI.Models.Products;

namespace SmartGrocery.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly HttpClient client;

        public ProductController(IMapper mapper, HttpClient client)
        {
            this.mapper = mapper;
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        [HttpGet]
        public async Task<ActionResult> Product(CancellationToken cancellationToken)
        {
            var response = await client.GetAsync("products", cancellationToken);
            response.EnsureSuccessStatusCode();
            
            var contract = await response.Content.ReadAsStringAsync();
            var viewModel = JsonConvert.DeserializeObject<List<ProductBaseViewModel>>(contract);

            return View("Details", viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ProductBaseViewModel();

            return PartialView("_Create", model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductBaseViewModel viewModel, CancellationToken cancellationToken)
        {
            var request = mapper.Map<CreateProductRequest>(viewModel);

            var response = await client.PostAsJsonAsync("products", request, cancellationToken);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Product");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id, CancellationToken cancellation)
        {
            var response = await client.GetAsync($"products/{id}", cancellation);
            
            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadAsStringAsync();
            var viewModel = JsonConvert.DeserializeObject<ProductBaseViewModel>(contract);

            return PartialView("_Edit", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProductBaseViewModel viewModel, CancellationToken cancellationToken)
        {
            var request = mapper.Map<EditProductRequest>(viewModel);

            var response = await client.PutAsJsonAsync("products", request, cancellationToken);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Product");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var response = await client.DeleteAsync($"products/{id}", cancellationToken);

            response.EnsureSuccessStatusCode();

            return RedirectToAction("Product");
        }

        [HttpGet]
        public async Task<ActionResult> GetProductByNumber(string productNumber, CancellationToken cancellationToken)
        {
            var response = await client.GetAsync($"products/transactions/{productNumber}", cancellationToken);

            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadAsStringAsync();
            var viewModel = JsonConvert.DeserializeObject<ProductSnapshotViewModel>(contract);

            return Json(new { result = viewModel }, JsonRequestBehavior.AllowGet);
        }
    }
}