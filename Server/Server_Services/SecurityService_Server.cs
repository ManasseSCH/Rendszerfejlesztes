﻿using Rendszerfejl.Models;
using Rendszerfejl.Services;

namespace Server.Server_Services
{
    public class SecurityService_Server
    {
        UsersDAO usersDAO = new UsersDAO();
        List<UserModel> knownUsers = new List<UserModel>();
        public SecurityService_Server()
        {

        }
        public bool IsValid(UserModel user)
        {
            UsersDAO_Server usersDAO = new UsersDAO_Server();
			return usersDAO.FindUserByNameAndPassword(user);

        }
    }
}