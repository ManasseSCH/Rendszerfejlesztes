using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rendszerfejl.Models;

namespace Server.Server_Services
{
    public class UsersDAO_Server 
    {
        public int Id;
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Rendszerfejl;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public bool FindUserByNameAndPassword(UserDTO user)
        {
            

            bool success = false;

            using (BloggingContext context = new BloggingContext())
            {
                // csak megnézi, hogy létezik-e az adatbázisban password és username alapján
                success = context.Users.Any(u => u.userName == user.userName && u.password == user.password);
                if (success == true)
                {
                    UserModel user1 = context.Users.First(u => u.userName == user.userName);
                    Id = user1.id;
                }
            }

            return success;

        }


    }
}
