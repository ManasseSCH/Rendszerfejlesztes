using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rendszerfejl.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string userName { get; set; } = null!;
        public string name { get; set; } = null!;
        [PasswordPropertyText]

        public string password { get; set; } = null!;
    }
}
