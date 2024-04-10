using Rendszerfejl.Models;
using Rendszerfejl.Services;

namespace Server.Server_Services
{
    public class SecurityService_Server
    {
        UsersDAO_Server usersDAO = new UsersDAO_Server();
        List<UserModel> knownUsers = new List<UserModel>();
        public SecurityService_Server()
        {

        }
        public bool IsValid(UserDTO user)
        {
            UsersDAO_Server usersDAO = new UsersDAO_Server();
			return usersDAO.FindUserByNameAndPassword(user);

        }
    }
}
