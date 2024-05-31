using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Moodle_server2._0.Models;

namespace Moodle_server2._0
{
    public class Program
    { 
        //public void configureServices(IServiceCollection services)
        //{
        //    services.AddDbContext
        //   // services.AddControllersWithView();
        //}
        public static void Main(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.



            //builder.Services.AddSqlite<moodleDataContext>(builder.Configuration.GetConnectionString("DefaultConnnection"));

            builder.Services.AddDbContext<moodleDataContext>();

            //builder.Services.AddDbContext<moodleData>(options =>
            //{
            //    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            //});


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

            app.UseCors(options => {
                options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        
    }
}