using Microsoft.AspNetCore.Mvc;
using Rendszerfejl.Models;

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
            if (userModel.userName == "asd" && userModel.password == "asd")
            {
                return View("LoginSuccess", userModel);
            }
            return View("Index");
        }
    }
}
