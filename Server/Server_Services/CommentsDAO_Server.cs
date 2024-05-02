
using Microsoft.Data.SqlClient;
using Rendszerfejl.Models;

namespace Server.Server_Services
{
	public class CommentsDAO_Server
	{
		string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Rendszerfejl;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

		public List<CommentModel> GetCommentsFromSelected(int id)
		{
			List<CommentModel> foundComments = new List<CommentModel>();
			

			using(BloggingContext bloggingContext = new BloggingContext()) 
			{
			foundComments=bloggingContext.Comments.Where(c=>c.TopicId==id).ToList();
				
			}



			return foundComments;

		}

		public void CreateComment(CommentModel comment)
		{
            //string sqlStatement = "INSERT INTO comments (user_id, topic_id, body, timestamp) VALUES (@UserId, @TopicId, @Body, @Timestamp)";
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //	SqlCommand command = new SqlCommand(sqlStatement, connection);
            //	command.Parameters.AddWithValue("@UserId", comment.UserId);
            //	command.Parameters.AddWithValue("@TopicId", comment.TopicId);
            //	command.Parameters.AddWithValue("@Body", comment.Body);
            //	command.Parameters.AddWithValue("@Timestamp", comment.Timestamp);
            //	try
            //	{
            //		connection.Open();
            //		int rowsAffected = command.ExecuteNonQuery();
            //		if (rowsAffected == 1)
            //		{
            //			Console.WriteLine("Comment added successfully.");
            //		}
            //		else
            //		{
            //			Console.WriteLine("Failed to add comment.");
            //		}
            //	}
            //	catch (Exception ex)
            //	{
            //		Console.WriteLine("Error: " + ex.Message);
            //	}
            //}

            using (BloggingContext bg = new BloggingContext())
            {
                CommentModel newcomment = new CommentModel();

                newcomment.UserId = comment.UserId;
                newcomment.TopicId = comment.TopicId;
                newcomment.Body = comment.Body;
                newcomment.Timestamp = comment.Timestamp;
                //Console.WriteLine(comment.Id + "\t" + comment.UserId + "\t" + comment.TopicId + "\t" + comment.Body + "\t" + comment.Timestamp);
                bg.Add<CommentModel>(newcomment);

                bg.SaveChanges();

            }


        }

       
	}
}
