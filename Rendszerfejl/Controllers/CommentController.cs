using Microsoft.AspNetCore.Mvc;
using Rendszerfejl.Services;

namespace Rendszerfejl.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult ViewComments(int id)
        {
            CommentsDAO commentsDAO = new CommentsDAO();

            return View("ViewComments", commentsDAO.GetCommentsFromSelected(id));
        }

        IActionResult CreateComment(string name)
        {

            return View();
        }

        
    }
}
