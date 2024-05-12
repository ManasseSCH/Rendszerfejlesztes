
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Your database operations here

            // Add services to the container.
            //BloggingContext bloggingContext = new BloggingContext();
            //Microsoft.EntityFrameworkCore.DbContextOptionsBuilder DbCOB= new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder();
            //bloggingContext.configure_db_manually(DbCOB);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddWebSocketManager();// hozzáadja a WebSocketManager-t
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization Header using the Bearer scheme(\"bearer{token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();

            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                            ValidateIssuer = false,
                            ValidateAudience = false

                        };
                    });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();
            var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();//A WebSocketManager használatához szükséges
            var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;//A WebSocketManager használatához szükséges

            app.UseWebSockets(); // Enable WebSocket support

            

            app.MapWebSocketManager("/ws", serviceProvider.GetService<HelloWorldHandler>());//A WebSocketManager-t használjuk a HelloWorldHandlerrel

            

            app.MapControllers();

            app.Run();
        }

        
    }
}