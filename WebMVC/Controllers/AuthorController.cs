using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
;        }
        [HttpPost]
        public async Task<IActionResult>Create(Author model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(_client.BaseAddress + "/Authors/AddAuthor", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("getAuthors");
                }
            }
            catch (Exception ex)
            {
                return View (ex.Message);
                
            }
            return View();
        }
    }
}
