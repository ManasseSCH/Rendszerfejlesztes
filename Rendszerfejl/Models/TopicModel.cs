using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace Rendszerfejl.Models
{
    public class TopicModel
    {
        public TopicModel(int Id,string Name,int TypeId,string Description) { this.Id = Id;this.Name = Name; this.TypeId = TypeId;this.Description = Description; }
        public TopicModel() { } 

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } 
        public int TypeId { get; set; }
        public string Description { get; set; } 
    }
}
