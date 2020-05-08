using AutoMapper;
using Newtonsoft.Json;
using PagedList;
using SmartGrocery.WebApi.Contracts.Customer;
using SmartGrocery.WebUI.Models.Customer;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartGrocery.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMapper mapper;
        private readonly HttpClient client;

        public CustomerController(IMapper mapper, HttpClient client)
        {
            this.mapper = mapper;
            this.client = client;
        }

        [HttpGet]
        public async Task<ActionResult> Summary(int? page, CancellationToken cancellationToken)
        {
            var response = await client.GetAsync("customers", cancellationToken);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var contract = JsonConvert.DeserializeObject<CustomerDetailsContract[]>(json);
            var viewModel = mapper.Map<CustomerViewModel[]>(contract);

            int pageNumber = page ?? 1;
            int pageSize = int.Parse(ConfigurationManager.AppSettings["DefaultPageSize"]);

            return View("Summary", viewModel.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<ActionResult> Details(string customerNumber, CancellationToken cancellationToken)
        {
            var response = await client.GetAsync($"customers/{customerNumber}", cancellationToken);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var contract = JsonConvert.DeserializeObject<CustomerDetailsContract>(json);
            var viewModel = mapper.Map<CustomerViewModel>(contract);

            return View("Details", viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CreateCustomerViewModel();

            return PartialView("_Create", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCustomerViewModel viewModel, CancellationToken cancellationToken)
        {
            var contract = mapper.Map<CreateCustomerRequest>(viewModel);

            var request = await client.PostAsJsonAsync("customers", contract, cancellationToken);
            request.EnsureSuccessStatusCode();

            return RedirectToAction("Summary");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string customerNumber, CancellationToken cancellationToken)
        {
            var response = await client.GetAsync($"customers/{customerNumber}", cancellationToken);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var contract = JsonConvert.DeserializeObject<CustomerDetailsContract>(json);
            var viewModel = mapper.Map<CreateCustomerViewModel>(contract);

            return PartialView("_Edit", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CreateCustomerViewModel viewModel, CancellationToken cancellationToken)
        {
            var request = mapper.Map<EditCustomerRequest>(viewModel);

            var response = await client.PutAsJsonAsync("customers", request, cancellationToken);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Summary");
        }
    }
}