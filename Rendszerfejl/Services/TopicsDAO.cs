using Microsoft.Data.SqlClient;
using Rendszerfejl.Models;

namespace Rendszerfejl.Services
{
    public class TopicsDAO

    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Rendszerfejl;
                                    Integrated Security=True;Connect Timeout=;
                                    Encrypt=False;Trust Server Certificate=False;
                                    Application Intent=ReadWrite;Multi Subnet Failover=False";
     
        public List<TopicModel> GetAllTopics()
        { 
            List<TopicModel> foundTopics = new List<TopicModel>();
            string sqlStatement = "SELECT * FROM dbo.Topics";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand commmand = new SqlCommand(sqlStatement, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = commmand.ExecuteReader();
                    while (reader.Read())
                    {
                        foundTopics.Add(new TopicModel { Id = (int)reader[0], Name = (string)reader[1], TypeId = (int)reader[2], Description = (string)reader[3] });
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message); 
                }
                
            }
            return foundTopics;

        }
    }
}
