using Microsoft.AspNetCore.Mvc;
using Rendszerfejl.Models;
using Rendszerfejl.Services;

namespace Rendszerfejl.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult ViewComments(int id)
        {
            CommentsDAO commentsDAO = new CommentsDAO();

            return View("ViewComments"/*, commentsDAO.GetCommentsFromSelected(id)*/); // under migration to server
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
