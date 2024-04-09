using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rendszerfejl.Models;
using Rendszerfejl.Services;

namespace Rendszerfejl.Controllers
{
    public class LoginController : ParentController //(Controller->ParentController) ösosztályból származik
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
        public async Task<IActionResult> ProcessLogin(UserModel userModel) //done migrating to server
        {

            string url = ("values/login/"+userModel.userName+"," + userModel.password);

			string str = await getString(url);

            if (str!=null)
            {
                List<TopicModel> myList = JsonConvert.DeserializeObject<List<TopicModel>>(str);
				return View("~/Views/Topic/Index.cshtml", myList);
			}
            
            ModelState.AddModelError("", "Invalid username or password");
            return View("Index", userModel);
        }
        public async Task<IActionResult> LoginResult()
        {
            return View();
               
        }
    }
}
