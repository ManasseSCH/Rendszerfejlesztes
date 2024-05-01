using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using Rendszerfejl.Models;
using Rendszerfejl.Services;
using System.Text;
using System;

namespace Rendszerfejl.Controllers
{
    public class CommentController : ParentController
    {
        public async Task<IActionResult> ViewComments(int id)
        {
            string str = await getString("comment/viewcomments/" + id);

            List<CommentModel> myList = JsonConvert.DeserializeObject<List<CommentModel>>(str);
            
            return View("ViewComments",myList); 
        }

        public IActionResult CreateComment(int id) // Ez csak elvisz arra a cshtml-re, ahol létre lehet hozni
        {
            @ViewBag.Message = id;
            return View("CreateComment");
            
        }

        public async Task<IActionResult> CreateNewComment(string id, CommentModel commentModel) // Ez hozza létre
        {

            string url = "https://localhost:7062/api/comment/viewcomments/create";
            // JSON data to send in the request body
            string tempId = HttpContext.Session.GetString("Id");
            int intTempId = int.Parse(tempId);
            string json = System.Text.Json.JsonSerializer.Serialize(commentModel);
            commentModel.TopicId = Convert.ToInt32(id);
            commentModel.UserId = intTempId;
            commentModel.Timestamp= DateTime.Now; 



            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
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
