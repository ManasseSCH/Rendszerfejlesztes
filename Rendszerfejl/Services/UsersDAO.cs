using Microsoft.Data.SqlClient;
using Rendszerfejl.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace Rendszerfejl.Services
{
    public class UsersDAO
    {
        //string connectionString2 =  @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = rendszerfejl; Integrated Security = True; Connect Timeout = 30; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";
        string connectionString =   @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Rendszerfejl;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public bool FindUserByNameAndPassword(UserModel user)
        {
            bool success = false;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @username AND password = @password";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 255).Value = user.userName;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 255).Value = user.password;
                try
                {
                    connection.Open();  
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception ex)
                { 
                    Console.WriteLine(ex.Message);
                }
                return success;
            }
        }
    }
}
