using MeterReader.Data;
using MeterReader.MeterReading;
using MeterReader.MeterReading.Model;
using MeterReader.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Globalization;

namespace MeterReader.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> Index(IFormCollection formCollection)
        {
            var firstLineHeader = !string.IsNullOrEmpty(formCollection["chkCheckbox"]) ? true : false;

            var client = new HttpClient();
            using var requestContent = new MultipartFormDataContent();
            using var fileStream = System.IO.File.OpenRead(formCollection.Files[0].FileName);
            requestContent.Add(new StreamContent(fileStream), "file", formCollection.Files[0].FileName);
            var response = await client.PostAsync($"https://{Request.Host}/meter-reading-uploads?firstLineHeaders={firstLineHeader}", requestContent);
            var pageContents = await response.Content.ReadAsStringAsync();

            List<MeterReadingWithError>? uploadResult = JsonConvert.DeserializeObject<List<MeterReadingWithError>>(pageContents);

            return View(uploadResult);
        }
            
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}