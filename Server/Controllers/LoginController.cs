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
    public class LoginController : ControllerBase
    {


		[HttpGet("{username},{password}")]
		public IEnumerable<TopicModel> login([FromRoute(Name ="username")] string username,[FromRoute (Name ="password")]string password)
		{

            SecurityService_Server securityService = new SecurityService_Server();  
            UserModel user = new UserModel();
            user.password = "password1";
            user.userName = "username1";
            user.id = 1;
            user.name = "User One";


           

            if (securityService.IsValid(user))  //TO DO: Valamiert mindig false a fuggveny eredmenye
            {
				TopicsDAO_Server topicsDAO = new TopicsDAO_Server();
                return topicsDAO.GetAllTopics();
			}
            
            return new List<TopicModel>();

        }
	}
}
