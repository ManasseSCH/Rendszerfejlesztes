using Microsoft.Extensions.Configuration.UserSecrets;

namespace Rendszerfejl.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }  
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }

    }
}