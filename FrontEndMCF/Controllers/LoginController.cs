using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace FrontEndMCF.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet("Login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("LoginGET")]
        public JsonResult Login(string username, string password) 
        {
            dynamic result = GetLoginFromBackend(username,password);
            return Json(result);
        }

        private object GetLoginFromBackend(string username, string password)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = "https://localhost:7253/api/Login/Login?username=" + username + "&password=" + password;
                return Task.Run(() => GetAsynchronous(httpClient,url));
            }
        }

        private async Task<object> GetAsynchronous(HttpClient httpClient, string url)
        {
            using HttpResponseMessage response = await httpClient.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync();
            return new { result };
        }
    }
}
