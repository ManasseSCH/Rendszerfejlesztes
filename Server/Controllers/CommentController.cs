using Microsoft.AspNetCore.Mvc;
using Rendszerfejl.Models;
using Rendszerfejl.Services;
using Server.Server_Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Server;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Concurrent;

namespace Rendszerfejl.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CommentController : Controller
    {

        private readonly HelloWorldHandler _helloWorldHandler;

        public CommentController(HelloWorldHandler helloWorldHandler)
        {
            _helloWorldHandler = helloWorldHandler;
        }
        [Authorize(Roles = "Role1,Role2")]
        [HttpGet("viewcomments/{topicId}")]
        public ActionResult<IEnumerable<CommentModel>> ViewComments(int topicId)
        {

            CommentsDAO_Server commentsDAO = new CommentsDAO_Server();
            List<CommentModel> comments = new List<CommentModel>();

            comments = commentsDAO.GetCommentsFromSelected(topicId);
            return comments;

        }
        [Authorize(Roles = "Role2")]
        [HttpPost("viewcomments/create")]
        public async Task<IActionResult> CreateNewComment([FromBody] CommentModel comment) // Ez hozza létre
        {
            try
            {
                CommentsDAO_Server commentsDAO = new CommentsDAO_Server();

                commentsDAO.CreateComment(comment);
                List<int> ids = commentsDAO.getFavouriteIds(comment.TopicId);
                string numbersAsString = string.Join(", ", ids);
                await _helloWorldHandler.SendMessageToAllAsync(numbersAsString);
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        
    }
}
