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

        //[HttpPost]
        //public IActionResult CreateNewComment(CommentModel commentModel) // Ez hozza létre
        //{
        //    CommentsDAO_Server commentsDAO = new CommentsDAO_Server();
        //    commentModel.Timestamp = DateTime.Now;
        //    commentsDAO.CreateComment(commentModel);
        //    return View("CreateComment"); // return
        //}


    }
}
