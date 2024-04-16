using System.ComponentModel.DataAnnotations;

namespace Rendszerfejl.Models
{
    public class TopicTypesModel
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }

    }
}
