using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rendszerfejl.Models;
using Rendszerfejl.Services;

namespace Rendszerfejl.Controllers
{
    public class LoginController : Controller
    {

        public async Task <IActionResult> Index()
        {
            using (System.Net.Http.HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7062/api/values/test");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = await response.Content.ReadAsStringAsync();
                    return View();
                }
                return View();

            }

        }
        public async Task<IActionResult> ProcessLogin(UserModel userModel)
        {
             

            string str = await TopicController.getString("values/test");

            List<TopicModel> myList = JsonConvert.DeserializeObject<List<TopicModel>>(str);
            
            string valt = "Index";
            TopicsDAO topicsDAO = new TopicsDAO();
            SecurityService securityService = new SecurityService();    
            if (securityService.IsValid(userModel))
            {
                return View("~/Views/Topic/Index.cshtml", myList);
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(valt, userModel);
        }
        public async Task<IActionResult> LoginResult()
        {
            return View();
               
        }
    }
}
