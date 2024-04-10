using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rendszerfejl.Models;
using Rendszerfejl.Services;

namespace Rendszerfejl.Controllers
{
    public class CommentController : ParentController
    {
        public async Task<IActionResult> ViewComments(int id)
        {
            string str = await getString("viewcomments/" + id);

            List<CommentModel> myList = JsonConvert.DeserializeObject<List<CommentModel>>(str);
            
            return View("ViewComments",myList); 
        }

        public IActionResult CreateComment() // Ez csak elvisz arra a cshtml-re, ahol létre lehet hozni
        {
            return View("CreateComment");
            
        }

        public IActionResult CreateNewComment(CommentModel commentModel) // Ez hozza létre
        {
            CommentsDAO commentsDAO = new CommentsDAO();
            commentModel.Timestamp = DateTime.Now;
            //commentsDAO.CreateComment(commentModel); under migration to server
            return View("CreateComment");
        }

        
    }
}
