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

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("test")]
        public IEnumerable<TopicModel> test()
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

		[HttpGet("login/{username},{password}")]
		public IEnumerable<TopicModel> login([FromRoute(Name ="username")] string username,[FromRoute (Name ="password")]string password)
		{
            SecurityService_Server securityService = new SecurityService_Server();  
            UserModel user = new UserModel();
            user.password = password;
            user.userName = username;
            if (securityService.IsValid(user))
            {
				TopicsDAO_Server topicsDAO = new TopicsDAO_Server();
				return topicsDAO.GetAllTopics();
			}
            return new List<TopicModel>();
		}
	}
}
