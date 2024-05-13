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
            var socketUrl = HttpContext.Session.GetString("socketUrl");
            if (string.IsNullOrEmpty(socketUrl))
            {
                // If no existing connection, create a new one and store the URL in session
                HttpContext.Session.SetString("socketUrl", "wss://localhost:7062/ws");
            }
            ViewBag.socketUrl = HttpContext.Session.GetString("socketUrl");


            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(HttpContext.Session.GetString("jwt"));
            var claims = token.Claims;

            Claim subClaim = claims.FirstOrDefault(C => C.Type == "Id");

            int userID = int.Parse(subClaim.Value);
            ViewBag.Number = userID;


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
        public async Task<IActionResult>  AddFavourite(int id) // CreateComment.cshtml-bol szedi ki
        {
            string url = "https://localhost:7062/api/Topic/AddFavourite/"; // Ugyanazon az elven mukodik, mint a create comment
            // JSON data to send in the request body

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(HttpContext.Session.GetString("jwt")); // Ez a jwt token felel az authorizacioert
            var claims = token.Claims;
            Claim subClaim = claims.FirstOrDefault(C => C.Type == "Id"); // Itt kerjuk ki az aktualis felhasznalo userId-jat

            Favorite_topicsModel fav = new Favorite_topicsModel();  
            string json = System.Text.Json.JsonSerializer.Serialize(fav); //jsonbe atkuldjuk
            fav.TopicId = id;
            fav.UserId = int.Parse(subClaim.Value);
            



            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("jwt"));
                // Set the Content-Type header to indicate JSON data
                List<CommentModel> myList = new List<CommentModel>();

                try
                {
                    JsonContent content = JsonContent.Create(fav);
                    await client.PostAsync(url,content);
                    //await ViewComments(Convert.ToInt32(id));
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
                string str = await getString("topic/allTopics", HttpContext.Session.GetString("jwt"));

                List<TopicModel> myNewList = JsonConvert.DeserializeObject<List<TopicModel>>(str);
                return View("index",myNewList);

            }
        }


    }
}
