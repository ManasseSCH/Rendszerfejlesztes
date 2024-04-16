using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rendszerfejl.Models
{
    [PrimaryKey(nameof(UserId), nameof(TopicId))]
    public class Favorite_topicsModel
    {
        
       public int UserId { get; set; }
        
       public int TopicId { get; set;}
    }
}
