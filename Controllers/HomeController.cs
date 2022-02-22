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
        private readonly MeterReaderContext _context;
        private readonly IMeterReadUploadBO _uploadBO;

        public HomeController(ILogger<HomeController> logger, MeterReaderContext context, IMeterReadUploadBO uploadBO)
        {
            _logger = logger;
            _context = context;
            _uploadBO = uploadBO;
        }

        public IActionResult Index()
        {
            var test = _context.Account.ToList();

            return View();
        }

        [HttpPost]
        public async Task<ViewResult> Index(IFormFile file)
        {
            await CheckHealthAsync(file);
            return View();
        }        

        public async Task<PartialViewResult> CheckHealthAsync(IFormFile file)
        {

            var client = new HttpClient();
            using var requestContent = new MultipartFormDataContent();
            using var fileStream = System.IO.File.OpenRead(file.FileName);
            requestContent.Add(new StreamContent(fileStream), "file", file.FileName);
            var response = await client.PostAsync("https://localhost:7027/meter-reading-uploads?firstLineHeaders=true", requestContent);
            var pageContents = await response.Content.ReadAsStringAsync();

            List<MeterReadingWithError> uploadResult = JsonConvert.DeserializeObject<List<MeterReadingWithError>>(pageContents);

            return PartialView(uploadResult);
            // var url = "https://localhost:7027/upload2";
            // HttpClient client = new HttpClient();

            // using var requestContent = new MultipartFormDataContent();
            // //using var fileStream = System.IO.File.OpenRead(file.FileName);
            //// requestContent.Add(new StreamContent(fileStream), "file", file.FileName);
            // var test = await client.PostAsync(url, requestContent);

            //var result = await HttpClient.PostAsync("https://localhost:7027/upload3?firtstLineHeaders=true", content);

            //MultipartFormDataContent multiContent = new MultipartFormDataContent();
            //byte[] data;
            //using (var br = new BinaryReader(file.OpenReadStream()))
            //    data = br.ReadBytes((int)file.OpenReadStream().Length);



            //ByteArrayContent bytes = new ByteArrayContent(data);
            //multiContent.Add(bytes, "file", file.FileName);
            //var response = await HttpClient.PostAsync("https://localhost:7027" + "/upload?firtstLineHeaders=true", multiContent);
            //var pageContents = await response.Content.ReadAsStringAsync();

            //string s = String.Empty;

            //using (var ms = new MemoryStream())
            //{
            //    file.CopyTo(ms);
            //    var fileBytes = ms.ToArray();
            //    s = Convert.ToBase64String(fileBytes);
            //    // act on the Base64 data
            //}

            //using (var client = new HttpClient())
            //{
            //    using (var content =
            //        new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
            //    {
            //        content.Add(new StreamContent(new MemoryStream(s)), "bilddatei", "upload.jpg");

            //        using
            //        (
            //           var message =
            //               await client.PostAsync("http://www.directupload.net/index.php?mode=upload", content))
            //        {
            //            var input = await message.Content.ReadAsStringAsync();


            //        }
            //    }
            //}

        }
    

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}