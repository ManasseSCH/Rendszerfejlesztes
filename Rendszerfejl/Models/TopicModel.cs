using System.Configuration;

namespace Rendszerfejl.Models
{
    public class TopicModel
    {
        public TopicModel(int Id,string Name,int TypeId,string Description) { this.Id = Id;this.Name = Name; this.TypeId = TypeId;this.Description = Description; }
        public TopicModel() { }
        public int Id { get; set; }
        public string Name { get; set; } 
        public int TypeId { get; set; }
        public string Description { get; set; } 
    }
}
