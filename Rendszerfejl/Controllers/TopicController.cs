using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rendszerfejl.Models;
using Rendszerfejl.Services;
using System.Net.Http.Headers;



namespace Rendszerfejl.Controllers
{
    public class TopicController : ParentController //(Controller->ParentController)
    {

        //ősosztályba helyezve:

        //public static async Task<string> getString(string url)
        //{
        //    string message = "";
        //    using (System.Net.Http.HttpClient client = new HttpClient())
        //    {
        //        var response = await client.GetAsync("https://localhost:7062/api/"+url);
        //        response.EnsureSuccessStatusCode();
        //        if (response.IsSuccessStatusCode)
        //        {
        //            message = await response.Content.ReadAsStringAsync();
                    
        //        }
        //        return message;

        //    }
        //}

        public async Task<IActionResult> index()
        {
            string str = await getString("topic/allTopics");

            List<TopicModel> myList = JsonConvert.DeserializeObject<List<TopicModel>>(str);
            return View(myList);
        }
        public async Task<IActionResult> SearchResults(string searchTerm)
        {
           
            string str = await getString("topic/searchfor/"+searchTerm);
            List<TopicModel> topicList = JsonConvert.DeserializeObject<List<TopicModel>>(str);
            return View("index", topicList);
        }
        public IActionResult SearchForm(int id)
        {
            return View();
        }
    }
}
