using Rendszerfejl.Models;

namespace Rendszerfejl.Services
{
    public class SecurityService
    {
        UsersDAO usersDAO = new UsersDAO();
        List<UserModel> knownUsers = new List<UserModel> ();
        public SecurityService()
        { 
            
        }
        public bool IsValid(UserModel user)
        {
            return usersDAO.FindUserByNameAndPassword(user);

        }
    }
}
