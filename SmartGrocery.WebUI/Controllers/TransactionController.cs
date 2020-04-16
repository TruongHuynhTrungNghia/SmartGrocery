using AutoMapper;
using Newtonsoft.Json;
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
    }
}