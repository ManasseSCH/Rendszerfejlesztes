using Rendszerfejl.Models;
using Rendszerfejl.Services;

namespace Server.Server_Services
{
    public class SecurityService_Server
    {
        public int Id;
        UsersDAO_Server usersDAO = new UsersDAO_Server();
        List<UserModel> knownUsers = new List<UserModel>();
        public SecurityService_Server()
        {
            
        }
        public bool IsValid(UserDTO user)
        {
            UsersDAO_Server usersDAO = new UsersDAO_Server();
            bool result = usersDAO.FindUserByNameAndPassword(user);
            Id = usersDAO.Id;
            return result;

        }
    }
}
