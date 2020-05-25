using AutoMapper;
using Newtonsoft.Json;
using PagedList;
using SmartGrocery.WebApi.Contracts.Transaction;
using SmartGrocery.WebUI.Models.Products;
using SmartGrocery.WebUI.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Emgu.CV;
using Emgu.CV.Structure;
//using Accord.Video.FFMPEG;
using System.Drawing;
using SmartGrocery.Infrastructure;

namespace SmartGrocery.WebUI.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IMapper mapper;
        private readonly HttpClient client;
        private readonly EmotionalRPCClient emotionalRPCClient;

        public TransactionController(IMapper mapper, HttpClient client, EmotionalRPCClient emotionalRPCClient)
        {
            this.mapper = mapper;
            this.client = client;
            this.emotionalRPCClient = emotionalRPCClient;
        }

        [HttpGet]
        public async Task<ActionResult> Summary(CancellationToken cancellationToken)
        {
            var response = await client.GetAsync("transactions", cancellationToken);
            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadAsStringAsync();
            var viewModel = JsonConvert.DeserializeObject<List<TransactionViewModel>>(contract);

            int pageSize = int.Parse(ConfigurationManager.AppSettings["DefaultPageSize"]);
            int pageNumber = 1;
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
            viewModel.LastUpdatedAt = DateTime.Now.ToString();
            if (viewModel.ProductSnapshots == null)
            {
                viewModel.CreateNewProductSnapshot();
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

        public async Task<ActionResult> StoreVideo(string base64image)
        {
            var image = base64image.Substring(22);  // remove data:image/png;base64,

            byte[] bytes = Convert.FromBase64String(image);

            var response = emotionalRPCClient.SendEmotionDataToServer(bytes);
            //foreach (string upload in Request.Files)
            //{
            //    //var path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
            //    var file = Request.Files[upload];


            //    //test();
            //    var videoByte = ConvertToMediaToByte(file);
            //    var response = emotionalRPCClient.SendEmotionDataToServer(videoByte);
            //    var videoBase64 = Convert.ToBase64String(videoByte);
            //    string result = System.Text.Encoding.UTF8.GetString(videoByte);

            //    var flag = System.IO.File.Exists(@"F:\Write.txt");

            //    using (FileStream fs = System.IO.File.OpenWrite(@"F:\Write.txt"))
            //    {
            //        using (StreamWriter sw = new StreamWriter(fs))
            //        {
            //            //sw.Write(DateTime.Now.ToString() + " sent email to " + email);
            //            sw.Write(result);
            //        }
            //        fs.Close();
            //    }

            //    using (FileStream fs = System.IO.File.OpenWrite(@"F:\Write1.txt"))
            //    {
            //        using (StreamWriter sw = new StreamWriter(fs))
            //        {
            //            sw.Write(videoBase64);
            //        }
            //        fs.Close();
            //    }

            //    if (file == null)
            //    {
            //        return Json(new { message = "Error" }, JsonRequestBehavior.AllowGet);
            //    }
            //}

            return Json(new { result = response}, JsonRequestBehavior.AllowGet);
        }

        private byte[] ConvertToMediaToByte(HttpPostedFileBase file)
        {
            var length = file.ContentLength;
            var bytes = new byte[length];

            using (var inputStream = file.InputStream)
            {
                inputStream.Read(bytes, 0, file.ContentLength);
            }

            return bytes;
        }
    }
}