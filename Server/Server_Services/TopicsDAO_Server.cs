using Microsoft.Data.SqlClient;
using Rendszerfejl.Models;

namespace Server.Server_Services
{
    public class TopicsDAO_Server
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
        public List<TopicModel> SearchTopics(string searchTerm)
        {
            List<TopicModel> foundTopics = new List<TopicModel>();
            string sqlStatement = "select t.* from topics as t join topic_types as tt on t.type_id = tt.id WHERE tt.name like @Name";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Name", '%' + searchTerm + '%');
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
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
