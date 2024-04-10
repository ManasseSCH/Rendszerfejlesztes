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


		[HttpPost("login")]
        public IActionResult Login([FromBody] UserDTO user)
        {


            // Validate credentials and perform login logic here
            SecurityService_Server securityService = new SecurityService_Server();

            if (securityService.IsValid(user))
            {

                return Ok();
            }
            else
            {
                // Return error response
                return BadRequest("Invalid credentials");
            }
        }

    }
}
