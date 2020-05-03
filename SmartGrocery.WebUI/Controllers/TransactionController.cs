using AutoMapper;
using Newtonsoft.Json;
using SmartGrocery.WebApi.Contracts.Transaction;
using SmartGrocery.WebUI.Models.Products;
using SmartGrocery.WebUI.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartGrocery.WebUI.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IMapper mapper;
        private readonly HttpClient client;

        public TransactionController(IMapper mapper, HttpClient client)
        {
            this.mapper = mapper;
            this.client = client;
        }

        [HttpGet]
        public async Task<ActionResult> Summary(CancellationToken cancellationToken)
        {
            var response = await client.GetAsync("transactions", cancellationToken);
            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadAsStringAsync();
            var viewModel = JsonConvert.DeserializeObject<List<TransactionViewModel>>(contract);

            return View("Summary", viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid Id, CancellationToken cancellationToken)
        {
            var response = await client.GetAsync($"transactions/{Id}", cancellationToken);
            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadAsStringAsync();
            var viewModel = JsonConvert.DeserializeObject<TransactionDetailsViewModel>(contract);

            return View("Details", viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new TransactionDetailsViewModel();

            viewModel.CreateNewProductSnapshot();

            return View("Create", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(TransactionDetailsViewModel viewModel, CancellationToken cancellationToken)
        {
            var request = mapper.Map<CreateTransactionRequest>(viewModel);

            var response = await client.PostAsJsonAsync("transactions", request, cancellationToken);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Summary");
        }

        [HttpGet]
        public async Task<ActionResult> Edit (Guid Id, CancellationToken cancellationToken)
        {
            var response = await client.GetAsync($"transactions/{Id}", cancellationToken);
            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadAsStringAsync();
            var viewModel = JsonConvert.DeserializeObject<TransactionDetailsViewModel>(contract);

            return PartialView("_Edit", viewModel);
        }

        [HttpGet]
        public ActionResult GetNewProductSnapshot()
        {
            return PartialView("EditorTemplates/_ProductSnapshot", new ProductSnapshotViewModel());
        }
    }
}