using Microsoft.AspNetCore.Mvc;
using Rendszerfejl.Models;
using Rendszerfejl.Services;

namespace Rendszerfejl.Controllers
{
    public class LoginController : Controller
    {
        public string asd { get; set; }
        public IActionResult Index()
        {
            using (LoginResult()) { return View(); }
            
        }
        public IActionResult ProcessLogin(UserModel userModel)
        {
            
            string valt = "Index";
            TopicsDAO topicsDAO = new TopicsDAO();
            SecurityService securityService = new SecurityService();    
            if (securityService.IsValid(userModel))
            {
                return View("~/Views/Topic/Index.cshtml", topicsDAO.GetAllTopics());
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(valt, userModel);
        }
        public async Task LoginResult()
        {
            using (System.Net.Http.HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:4244/values");
                response .EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message= await response.Content.ReadAsStringAsync();
                }
                
            }
               
        }
    }
}
