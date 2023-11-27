using Microsoft.EntityFrameworkCore;
using ToDoAPI.DATA.EF.Models;
namespace ToDoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    //Conncection access for the API
                    policy.WithOrigins("OriginPolicy", "http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Initialize DB Connection
            builder.Services.AddDbContext<ToDoContext>(
                    options =>
                    {
                        options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoDB"));
                    });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors();

            app.Run();
        }
    }
}