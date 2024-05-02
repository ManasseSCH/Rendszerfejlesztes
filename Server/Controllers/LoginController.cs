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
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration iconfiguration)
        {
            _configuration = iconfiguration;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDTO user)
        {


            // Validate credentials and perform login logic here
            SecurityService_Server securityService = new SecurityService_Server();

            if (securityService.IsValid(user))
            {
                int id = securityService.Id;
                string token = CreateToken(user,id);
                return Ok(token);
            }
            else
            {
                // Return error response
                return BadRequest("Invalid credentials");
            }
        }
        private string CreateToken(UserDTO user, int id)
        {

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.userName));
            claims.Add(new Claim("Id", id.ToString()));
            if (user.userName == "user1")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Role1"));
            }
            else
            { 
                claims.Add(new Claim(ClaimTypes.Role, "Role2"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Appsettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims : claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds 
                    );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
