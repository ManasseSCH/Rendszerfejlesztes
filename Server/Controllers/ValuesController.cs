using Elfie.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Rendszerfejl.Models;
using Rendszerfejl.Services;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("test")]
        public IEnumerable<TopicModel> test()
        {
            TopicsDAO topicsDAO = new TopicsDAO();
            return topicsDAO.GetAllTopics();
        }
        [HttpGet("searchfor/{searchTerm}")]
        public ActionResult<IEnumerable<TopicModel>> SearchResults(string searchTerm)
        {
            TopicsDAO topics = new TopicsDAO();
            List<TopicModel> topicList = topics.SearchTopics(searchTerm);

            return topicList;
        }
    }
}
