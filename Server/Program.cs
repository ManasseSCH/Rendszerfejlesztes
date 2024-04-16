using Microsoft.EntityFrameworkCore;
using Server;


var builder = WebApplication.CreateBuilder(args);

    // Your database operations here

// Add services to the container.
//BloggingContext bloggingContext = new BloggingContext();
//Microsoft.EntityFrameworkCore.DbContextOptionsBuilder DbCOB= new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder();
//bloggingContext.configure_db_manually(DbCOB);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
