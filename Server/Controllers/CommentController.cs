using Microsoft.AspNetCore.Mvc;
using Rendszerfejl.Models;
using Rendszerfejl.Services;
using Server.Server_Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Server;
using Microsoft.AspNetCore.Authorization;

namespace Rendszerfejl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CommentController : Controller
    {
        [Authorize]
        [HttpGet("viewcomments/{topicId}")]
        public ActionResult<IEnumerable<CommentModel>> ViewComments(int topicId)
        {

            CommentsDAO_Server commentsDAO = new CommentsDAO_Server();
            List<CommentModel> comments = new List<CommentModel>();
            
            comments = commentsDAO.GetCommentsFromSelected(topicId);
            return comments;

        }
        [Authorize]
        [HttpPost("viewcomments/create")]
        public async Task<IActionResult> CreateNewComment([FromBody] CommentModel comment) // Ez hozza létre
        {
            try 
            { 
                CommentsDAO_Server commentsDAO = new CommentsDAO_Server();
                
                commentsDAO.CreateComment(comment);
                return Ok();
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


    }
}
