using Microsoft.AspNetCore.Mvc;
using Rendszerfejl.Models;
using Rendszerfejl.Services;
using System.Net.Http.Headers;

namespace Rendszerfejl.Controllers
{
    [ApiController]
    [Route("api/TopicControllerAPI")]
    public class TopicControllerAPI : ControllerBase
    {
        [HttpGet]
        public IEnumerable<TopicModel> Index()
        {
            TopicsDAO topicsDAO = new TopicsDAO();
            return topicsDAO.GetAllTopics();
        }

        [HttpGet("searchfor/{searchTerm}")]
        public ActionResult <IEnumerable<TopicModel>> SearchResults(string searchTerm) 
        {
            TopicsDAO topics = new TopicsDAO();
            List<TopicModel> topicList = topics.SearchTopics(searchTerm);  
            
            return topicList;
        }
        /*public IActionResult SearchForm(int id) // Nem lehet megvalósítani (nem is kell, ki is törölhetném, példa kedvéért hagytam itt)
        {
            return View();
        } */
    }
}
