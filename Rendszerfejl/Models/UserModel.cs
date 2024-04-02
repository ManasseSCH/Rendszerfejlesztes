using System.ComponentModel;

namespace Rendszerfejl.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string name { get; set; }
        [PasswordPropertyText]    
        
        public string password { get; set; }        
    }
}
