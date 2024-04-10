using Microsoft.AspNetCore.Mvc;
using Rendszerfejl.Models;
using Rendszerfejl.Services;
using Server.Server_Services;

namespace Rendszerfejl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        [HttpGet("viewcomments/{topicId}")]
        public ActionResult<IEnumerable<CommentModel>> ViewComments(int topicId)
        {

            CommentsDAO_Server commentsDAO = new CommentsDAO_Server();
            List<CommentModel> comments = new List<CommentModel>();
            
            comments = commentsDAO.GetCommentsFromSelected(topicId);
            return comments;

        }

        //public IActionResult CreateComment() // Ez csak elvisz arra a cshtml-re, ahol létre lehet hozni
        //{
        //    return View("CreateComment");

        //}

        //public IActionResult CreateNewComment(CommentModel commentModel) // Ez hozza létre
        //{
        //    CommentsDAO commentsDAO = new CommentsDAO();
        //    commentModel.Timestamp = DateTime.Now;
        //    //commentsDAO.CreateComment(commentModel); under migration to server
        //    return View("CreateComment");
        //}


    }
}
