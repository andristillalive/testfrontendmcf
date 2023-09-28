using FrontEndMCF.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace FrontEndMCF.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("InsertHome")]
        public JsonResult InsertHome(RequestInsertData rid)
        {
            dynamic result = InsertDataFromBackend(rid);
            return Json(result);
        }

        private object InsertDataFromBackend(RequestInsertData rid)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = "https://localhost:7253/api/Home/InsertData";
                using StringContent jsonContent = new(JsonSerializer.Serialize(rid), Encoding.UTF8, "application/json");
                return Task.Run(() => PostAsynchronous(httpClient, url, jsonContent));
            }
        }

        private async Task<object> PostAsynchronous(HttpClient httpClient, string url, StringContent sc)
        {
            using HttpResponseMessage response = await httpClient.PostAsync(url, sc);
            string result = await response.Content.ReadAsStringAsync();
            return new { result };
        }

        public class RequestInsertData
        {
            public string AgreementNumber { get; set; } = null!;

            public string? BpkbNo { get; set; }

            public string? BranchId { get; set; }

            public DateTime? BpkbDate { get; set; }

            public string? FakturNo { get; set; }

            public DateTime? FakturDate { get; set; }

            public string? LocationId { get; set; }

            public string? PoliceNo { get; set; }

            public DateTime? BpkbDateIn { get; set; }

            public string? CreatedBy { get; set; }

            public DateTime? CreatedOn { get; set; }

            public string? LastUpdateBy { get; set; }

            public DateTime? LastUpdateOn { get; set; }
        }
    }
}