using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rendszerfejl.Models;
using Rendszerfejl.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;



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
            string str = await getString("topic/allTopics", HttpContext.Session.GetString("jwt"));

            List<TopicModel> myList = JsonConvert.DeserializeObject<List<TopicModel>>(str);
            return View(myList);
        }
        public async Task<IActionResult> SearchResults(string searchTerm)
        {
           
            string str = await getString("topic/searchfor/"+searchTerm, HttpContext.Session.GetString("jwt"));
            List<TopicModel> topicList = JsonConvert.DeserializeObject<List<TopicModel>>(str);
            return View("index", topicList);
        }
        public IActionResult SearchForm(int id)
        {
            return View();
        }

        public async Task<IActionResult> ShowMyComments()
        {
            HttpContext.Session.GetString("jwt");

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(HttpContext.Session.GetString("jwt"));
            var claims = token.Claims;

            Claim subClaim = claims.FirstOrDefault(C => C.Type == "Id");

            int userID = int.Parse(subClaim.Value);


            string str = await getString("topic/mycomments/" + userID, HttpContext.Session.GetString("jwt"));
            List<TopicModel> topicList = JsonConvert.DeserializeObject<List<TopicModel>>(str);
            return View("index", topicList);
        }

    }
}
