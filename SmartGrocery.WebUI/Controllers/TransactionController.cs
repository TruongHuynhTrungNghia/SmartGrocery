using AutoMapper;
using Newtonsoft.Json;
using PagedList;
using SmartGrocery.Infrastructure;
using SmartGrocery.WebApi.Contracts.Transaction;
using SmartGrocery.WebUI.Models.Products;
using SmartGrocery.WebUI.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartGrocery.WebUI.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IMapper mapper;
        private readonly HttpClient client;
        private readonly EmotionalRPCClient emotionalRPCClient;

        public TransactionController(IMapper mapper, 
            HttpClient client, 
            EmotionalRPCClient emotionalRPCClient)
        {
            this.mapper = mapper;
            this.client = client;
            this.emotionalRPCClient = emotionalRPCClient;
        }

        [HttpGet]
        public async Task<ActionResult> Summary(int? page, CancellationToken cancellationToken)
        {
            var response = await client.GetAsync("transactions", cancellationToken);
            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadAsStringAsync();
            var viewModel = JsonConvert.DeserializeObject<List<TransactionViewModel>>(contract);

            int pageSize = int.Parse(ConfigurationManager.AppSettings["DefaultPageSize"]);
            int pageNumber = page ?? 1;
            return View("Summary", viewModel.ToPagedList(pageNumber, pageSize));
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
            viewModel.CreatedBy = User.Identity.Name;
            viewModel.LastUpdatedBy = User.Identity.Name;
            viewModel.CreateNewProductSnapshot();

            return View("Create", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(TransactionDetailsViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            viewModel.CalculateProductPrice();
            var request = mapper.Map<CreateTransactionRequest>(viewModel);

            var response = await client.PostAsJsonAsync("transactions", request, cancellationToken);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Summary");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid Id, CancellationToken cancellationToken)
        {
            var response = await client.GetAsync($"transactions/{Id}", cancellationToken);
            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadAsStringAsync();
            var viewModel = JsonConvert.DeserializeObject<TransactionDetailsViewModel>(contract);
            viewModel.LastUpdatedBy = User.Identity.Name;
            viewModel.LastUpdatedAt = DateTime.Now;
            if (viewModel.ProductSnapshots.Length == 0)
            {
                viewModel.CreateNewProductSnapshot();
            }
            else
            {
                foreach(var product in viewModel.ProductSnapshots)
                {
                    product.DisplayPrice = product.Price;
                }
            }

            return PartialView("_Edit", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TransactionDetailsViewModel viewModel, CancellationToken cancellationToken)
        {
            var request = mapper.Map<EditTransactionRequest>(viewModel);

            var response = await client.PutAsJsonAsync("transactions", request, cancellationToken);
            response.EnsureSuccessStatusCode();
            var json = response.Content.ReadAsStringAsync();
            var transactionId = JsonConvert.DeserializeObject<Guid>(json.Result);

            return RedirectToAction("Details", new { Id = transactionId });
        }

        [HttpGet]
        public ActionResult GetNewProductSnapshot()
        {
            return PartialView("EditorTemplates/_ProductSnapshot", new ProductSnapshotViewModel());
        }

        public ActionResult StoreVideo(string base64image)
        {
            if (string.IsNullOrEmpty(base64image))
            {
                return Json(new { result = emotionalRPCClient.EmptyEmotionData }, JsonRequestBehavior.AllowGet);
            }

            var image = base64image.Substring(22);

            //Using the emotion detection module
            byte[] bytes = Convert.FromBase64String(image);

            var response = emotionalRPCClient.SendEmotionDataToServer(bytes);
            ViewData["CustomerEmotionData"] = new List<EmotionalData>();

            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }
    }
}