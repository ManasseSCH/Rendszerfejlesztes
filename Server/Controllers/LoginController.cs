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
                string token = CreateToken(user);
                return Ok(token);
            }
            else
            {
                // Return error response
                return BadRequest("Invalid credentials");
            }
        }
        private string CreateToken(UserDTO user)
        {
            List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.userName)
    };

            // Retrieve the key from configuration settings
            string base64Key = _configuration.GetSection("AppSettings:Token").Value;
            byte[] keyBytes = Convert.FromBase64String(base64Key);

            // Create a symmetric security key using the key bytes
            var key = new SymmetricSecurityKey(keyBytes);

            // Use HMAC-SHA256 algorithm with the symmetric key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Generate the JWT token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            // Write the JWT token as a string
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
