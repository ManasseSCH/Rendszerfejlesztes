using Microsoft.Data.SqlClient;
using Rendszerfejl.Models;
using System.Xml.Linq;

namespace Rendszerfejl.Services
{
    public class CommentsDAO
    {
        //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Rendszerfejl;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        //public List<CommentModel> GetCommentsFromSelected(int id)
        //{
        //    List<CommentModel> foundComments = new List<CommentModel>();
        //    string sqlStatement = "SELECT c.* from comments as c join topics as t on t.id = c.topic_id where t.id = @Id"; // TODO
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand commmand = new SqlCommand(sqlStatement, connection);
        //        commmand.Parameters.AddWithValue("@Id",id);
        //        try
        //        {
        //            connection.Open();
        //            SqlDataReader reader = commmand.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                foundComments.Add(new CommentModel { Id = (int)reader[0], UserId = (int)reader[1], TopicId = (int)reader[2], Body = (string)reader[3], Timestamp = (DateTime)reader[4] });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }

        //    }
        //    return foundComments;

        //}

        //public void CreateComment(CommentModel comment)
        //{
        //    string sqlStatement = "INSERT INTO comments (user_id, topic_id, body, timestamp) VALUES (@UserId, @TopicId, @Body, @Timestamp)";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(sqlStatement, connection);
        //        command.Parameters.AddWithValue("@UserId", comment.UserId);
        //        command.Parameters.AddWithValue("@TopicId", comment.TopicId);
        //        command.Parameters.AddWithValue("@Body", comment.Body);
        //        command.Parameters.AddWithValue("@Timestamp", comment.Timestamp);
        //        try
        //        {
        //            connection.Open();
        //            int rowsAffected = command.ExecuteNonQuery();
        //            if (rowsAffected == 1)
        //            {
        //                Console.WriteLine("Comment added successfully.");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Failed to add comment.");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Error: " + ex.Message);
        //        }
        //    }
        //}

    }
}
