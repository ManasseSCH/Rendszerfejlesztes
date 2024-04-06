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
             return View();
            
        }
        public IActionResult ProcessLogin(UserModel userModel)
        {
            ViewBag.Message = "asd";
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
        public async Task<IActionResult> LoginResult()
        {
            using (System.Net.Http.HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7062/api/values");
                response .EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message= await response.Content.ReadAsStringAsync();
                    return View();
                }
                return View();
                
            }
               
        }
    }
}
