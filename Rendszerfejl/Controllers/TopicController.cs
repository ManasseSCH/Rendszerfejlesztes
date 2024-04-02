using Microsoft.AspNetCore.Mvc;
using Rendszerfejl.Services;
using System.Net.Http.Headers;

namespace Rendszerfejl.Controllers
{
    public class TopicController : Controller
    {
        public IActionResult Index()
        {
            TopicsDAO topicsDAO = new TopicsDAO();
            return View( topicsDAO.GetAllTopics());
        }
    }
}
