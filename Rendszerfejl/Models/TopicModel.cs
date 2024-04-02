using System.Configuration;

namespace Rendszerfejl.Models
{
    public class TopicModel
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int TypeId { get; set; }
        public string Description { get; set; } 
    }
}
