using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Rendszerfejl.Models;
using System.Net.WebSockets;

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
        public List<TopicModel> GetTopicsById(int userID) 
        {
            using BloggingContext context = new BloggingContext();
            var topics = (from topic in context.Topics
                         join comment in context.Comments on topic.Id  equals comment.TopicId
                         where comment.UserId.Equals(userID) 
                         select topic).Distinct().ToList();

            return topics;  

        }
        public async void  AddFavourite(Favorite_topicsModel fav)
        {
           
                using (BloggingContext bg = new BloggingContext())
                {
                    // Check if the record already exists
                    bool exists = bg.FavTopics.Any(ft => ft.UserId == fav.UserId && ft.TopicId == fav.TopicId);

                    // If the record doesn't exist, add it
                    if (!exists)
                    {
                        Favorite_topicsModel ft = new Favorite_topicsModel();
                        ft.UserId = fav.UserId;
                        ft.TopicId = fav.TopicId;
                        bg.Add<Favorite_topicsModel>(ft);
                        bg.SaveChanges();
                    }
                }
                


        }



    }
}
