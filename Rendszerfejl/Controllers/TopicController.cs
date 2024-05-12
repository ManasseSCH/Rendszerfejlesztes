using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rendszerfejl.Models;
using Rendszerfejl.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;



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


            using (ClientWebSocket client = new ClientWebSocket())
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(120));
                try
                {
                    Uri uri = new Uri("wss://localhost:7062/ws");
                    await client.ConnectAsync(uri, cts.Token);
                    var n = 0;
                    while (client.State == WebSocketState.Open)
                    {
                        string message = $"AddFavTopic{fav.TopicId},{fav.UserId}";
                        ArraySegment<byte> bytestosend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                        await client.SendAsync(bytestosend, WebSocketMessageType.Text, true, cts.Token);
                        var responsebuffer = new byte[1024];
                        var offset = 0;
                        var packet = 1024;
                        while (true)
                        {
                            ArraySegment<byte> recievedbyte = new ArraySegment<byte>(responsebuffer, offset, packet);
                            WebSocketReceiveResult response = await client.ReceiveAsync(recievedbyte, cts.Token);
                            var responsemessage = Encoding.UTF8.GetString(responsebuffer, offset, response.Count);
                            Console.WriteLine(responsemessage);
                            if (response.EndOfMessage) { break; }
                        }
                    }
                }
                catch (WebSocketException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

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
