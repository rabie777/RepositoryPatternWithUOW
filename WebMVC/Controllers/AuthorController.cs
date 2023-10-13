using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class AuthorController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44314/api");
        private readonly HttpClient _client;
        public AuthorController(HttpClient client)
        { 
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
  
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var auth = new List<Author>();
            var response = await _client.GetAsync(_client.BaseAddress +"/Authors/AllAuthors");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                auth = JsonConvert.DeserializeObject<List<Author>>(data); 
            }
            return View(auth);
        }
    }
}
