using Microsoft.AspNetCore.Mvc;
using Rendszerfejl.Models;
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
        public IActionResult SearchResults(string searchTerm) 
        {
            TopicsDAO topics = new TopicsDAO();
            List<TopicModel> topicList = topics.SearchTopics(searchTerm);  
            
            return View("index",topicList);
        }
        public IActionResult SearchForm(int id)
        {
            return View();
        }
    }
}
