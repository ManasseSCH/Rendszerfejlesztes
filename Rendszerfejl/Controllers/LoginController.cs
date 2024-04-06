using Microsoft.AspNetCore.Mvc;
using Rendszerfejl.Models;
using Rendszerfejl.Services;

namespace Rendszerfejl.Controllers
{
    public class LoginController : Controller
    { 
        public IActionResult Index()
        {
            return View();
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
    }
}
