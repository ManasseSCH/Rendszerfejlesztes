using Microsoft.Data.SqlClient;
using Rendszerfejl.Models;
using System.Xml.Linq;

namespace Rendszerfejl.Services
{
    public class CommentsDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Rendszerfejl;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public List<CommentModel> GetCommentsFromSelected(int id)
        {
            List<CommentModel> foundComments = new List<CommentModel>();
            string sqlStatement = "SELECT c.* from comments as c join topics as t on t.id = c.topic_id where t.id = @Id"; // TODO
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand commmand = new SqlCommand(sqlStatement, connection);
                commmand.Parameters.AddWithValue("@Id",id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = commmand.ExecuteReader();
                    while (reader.Read())
                    {
                        foundComments.Add(new CommentModel { Id = (int)reader[0], UserId = (int)reader[1], TopicId = (int)reader[2], Body = (string)reader[3], Timestamp = (DateTime)reader[4] });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return foundComments;

        }
    }
}
