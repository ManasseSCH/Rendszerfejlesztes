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
        [HttpGet("allTopics")]
        [Authorize]
        public IEnumerable<TopicModel> allTopics()
        {
            TopicsDAO_Server topicsDAO = new TopicsDAO_Server();
            return topicsDAO.GetAllTopics();
        }

        [HttpGet("searchfor/{searchTerm}")]
        public ActionResult<IEnumerable<TopicModel>> SearchResults(string searchTerm)
        {
            TopicsDAO_Server topics = new TopicsDAO_Server();
            List<TopicModel> topicList = topics.SearchTopics(searchTerm);

            return topicList;
        }

        
    }
}
