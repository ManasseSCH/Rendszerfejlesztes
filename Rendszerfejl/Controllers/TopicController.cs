using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rendszerfejl.Models;
using Rendszerfejl.Services;
using System.Net.Http.Headers;

namespace Rendszerfejl.Controllers
{
    public class TopicController : Controller
    {
        public static async Task<string> getString(string url)
        {
            string message = "";
            using (System.Net.Http.HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7062/api/"+url);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    message = await response.Content.ReadAsStringAsync();
                    
                }
                return message;

            }
        }
        public async Task<IActionResult> index()
        {
            string str = await getString("values/test");

            List<TopicModel> myList = JsonConvert.DeserializeObject<List<TopicModel>>(str);
            return View(myList);
        }
        public IActionResult SearchResults(string searchTerm) 
        {
            TopicsDAO topics = new TopicsDAO();
            List<TopicModel> topicList = topics.SearchTopics(searchTerm);  
            
            return View("index",topicList);
        }
        public IActionResult SearchForm(int id)
        {
            return View();
        }
    }
}
