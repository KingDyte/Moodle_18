using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Moodle_server2._0.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Moodle_server2._0
{
    public class Program
    { 
        
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

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Token").Value)),
                       ValidateAudience = true,
                       ValidateLifetime = true
                   };
               }); 

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        
    }
}