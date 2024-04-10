using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using Rendszerfejl.Models;
using Rendszerfejl.Services;

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

        public IActionResult CreateComment() // Ez csak elvisz arra a cshtml-re, ahol létre lehet hozni
        {
            return View("CreateComment");
            
        }

        //public async Task<IActionResult> CreateNewComment(CommentModel commentModel) // Ez hozza létre
        //{
        //    string str = await getString("comment/createcomment/"+commentModel);

        //    return View();

        //}

        
    }
}
