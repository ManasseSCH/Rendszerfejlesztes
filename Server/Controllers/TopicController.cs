using Elfie.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Rendszerfejl.Models;
using Rendszerfejl.Services;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Server.Server_Services;
using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly HelloWorldHandler _helloWorldHandler;

        public TopicController(HelloWorldHandler helloWorldHandler)
        {
            _helloWorldHandler = helloWorldHandler;
        }
        [HttpGet("allTopics")]
        
        public async Task<IEnumerable<TopicModel>>allTopics()
        {
            TopicsDAO_Server topicsDAO = new TopicsDAO_Server();
            await _helloWorldHandler.SendMessageToAllAsync("new comment");
            return topicsDAO.GetAllTopics();
        }

        [HttpGet("searchfor/{searchTerm}")]
        public ActionResult<IEnumerable<TopicModel>> SearchResults(string searchTerm)
        {
            TopicsDAO_Server topics = new TopicsDAO_Server();
            List<TopicModel> topicList = topics.SearchTopics(searchTerm);

            return topicList;
        }
        [HttpGet("mycomments/{UserId}")]
        
        public ActionResult<IEnumerable<TopicModel>> myComments(int UserId)
        {
            TopicsDAO_Server topics = new TopicsDAO_Server();
            List<TopicModel> topicList = topics.GetTopicsById(UserId);
            return topicList;
        }
        [HttpPost("AddFavourite")] 
        public async Task<IActionResult> AddFavourite([FromBody]  Favorite_topicsModel fav) // Ez hozza létre (Visszajelzest nem ad) | database-ben tudod megnezni
        {
            try
            {
                TopicsDAO_Server topics = new TopicsDAO_Server();

                topics.AddFavourite(fav);

                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
