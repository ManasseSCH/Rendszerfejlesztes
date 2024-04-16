using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Rendszerfejl.Controllers;
using Rendszerfejl.Models;
using System.Reflection.Metadata;

namespace Server
{
    public class BloggingContext : DbContext
    {
        
        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<CommentModel> Comments { get; set; } = null!;
        public DbSet<TopicModel> Topics { get; set; } = null!;
        public DbSet<TopicTypesModel> Ttypes { get; set; } = null!;
        public DbSet<Favorite_topicsModel> FavTopics { get; set; } =null!;






        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Rendszerfejl;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

                
        }


    }
}
