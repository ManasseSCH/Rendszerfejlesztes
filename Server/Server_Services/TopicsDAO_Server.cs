using Microsoft.CodeAnalysis.CSharp.Syntax;
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

            
            using (BloggingContext context = new BloggingContext())
            {
                var topics = new List<TopicModel>();
                foreach (var item in context.Topics) //Végigjárjuk a Topics táblát, hozzáadjuk elemenként a topics listához
                {
                    topics.Add(item);
                }
                return topics;
            }
        }
        public List<TopicModel> SearchTopics(string searchTerm)
        {


            //}
            //return foundTopics;


            using BloggingContext context = new BloggingContext();

                var topics = (from topic in context.Topics
                          join ttype in context.Ttypes on topic.TypeId equals ttype.Id
                          where ttype.Name.Contains(searchTerm)
                          select topic).ToList();

            return topics;





        }
    }
}
