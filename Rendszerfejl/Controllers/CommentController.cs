using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using Rendszerfejl.Models;
using Rendszerfejl.Services;
using System.Text;
using System;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Rendszerfejl.Controllers
{
    public class CommentController : ParentController
    {
        public async Task<IActionResult> ViewComments(int id)
        {
            string jwt = HttpContext.Session.GetString("jwt");
            string str = await getString("comment/viewcomments/" + id,jwt);
            
            List<CommentModel> myList = JsonConvert.DeserializeObject<List<CommentModel>>(str);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(HttpContext.Session.GetString("jwt"));
            var claims = token.Claims;

            Claim subClaim = claims.FirstOrDefault(C => C.Type == "Id");

            int userID = int.Parse(subClaim.Value);
            ViewBag.Number = userID;

            return View("ViewComments",myList); 
        }

        public IActionResult CreateComment(int id) // Ez csak elvisz arra a cshtml-re, ahol létre lehet hozni
        {
            @ViewBag.Message = id;

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(HttpContext.Session.GetString("jwt"));
            var claims = token.Claims;

            Claim subClaim = claims.FirstOrDefault(C => C.Type == "Id");

            int userID = int.Parse(subClaim.Value);
            ViewBag.Number = userID;
            return View("CreateComment");
            
        }

        public async Task<IActionResult> CreateNewComment(string id, CommentModel commentModel) // Ez hozza létre
        {


            string url = "https://localhost:7062/api/comment/viewcomments/create";
            // JSON data to send in the request body

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(HttpContext.Session.GetString("jwt"));
            var claims = token.Claims;
            Claim subClaim = claims.FirstOrDefault(C => C.Type == "Id");

            string json = System.Text.Json.JsonSerializer.Serialize(commentModel);
            commentModel.TopicId = Convert.ToInt32(id);
            commentModel.UserId = int.Parse(subClaim.Value);
            commentModel.Timestamp= DateTime.Now; 



            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("jwt"));
                // Set the Content-Type header to indicate JSON data
                List<CommentModel> myList = new List<CommentModel>();

                try
                {
                    JsonContent content = JsonContent.Create(commentModel);
                    await client.PostAsync(url,content);
                    //await ViewComments(Convert.ToInt32(id));
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);    
                }
                return View("CreateComment");
            }
        }


    }
}
