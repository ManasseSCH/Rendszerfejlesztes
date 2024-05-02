using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rendszerfejl.Models;
using Rendszerfejl.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rendszerfejl.Controllers
{
    public class LoginController : ParentController //(Controller->ParentController) ösosztályból származik
    {

        public async Task <IActionResult> Index()
        {
            return View();

        }
        public async Task<IActionResult> ProcessLogin(UserModel userModel) //done migrating to server
        {


            string url = "https://localhost:7062/api/Login/login";
            UserDTO userDTO = new UserDTO();
            userDTO.password = userModel.password;
            userDTO.userName = userModel.userName;

            // JSON data to send in the request body
            string json = System.Text.Json.JsonSerializer.Serialize(userDTO);


            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Set the Content-Type header to indicate JSON data


                try
                {
                    // Send a POST request with the JSON data
                    HttpResponseMessage response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

                    // Check if the response is successful (status code in the range 200-299)
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Parse the response content as an integer
                        //string jwt = responseContent;
                        //var tokenHandler = new JwtSecurityTokenHandler();
                        //var jwtTokenObj = tokenHandler.ReadJwtToken(jwt);
                        //var claims = jwtTokenObj.Claims;
                        //var idClaim = claims.FirstOrDefault(c => c.Type == "Id");

                        HttpContext.Session.SetString("Id", responseContent);

                        string str = await getString("topic/allTopics");

                        List<TopicModel> myList = JsonConvert.DeserializeObject<List<TopicModel>>(str);
                        return View("~/Views/topic/index.cshtml",myList);
                        
                    }
                    else
                    {

                        ModelState.AddModelError("", "Invalid username or password");
                        return View("Index");
                    }
                }
                catch (HttpRequestException e)
                {
                    // Handle exception if request fails
                    Console.WriteLine("Error: " + e.Message);

                    ModelState.AddModelError("", "Invalid username or password");
                    return View("Index");
                }

            }
        }
            public async Task<IActionResult> LoginResult()
            {
                return View();
               
            }
    }
}
