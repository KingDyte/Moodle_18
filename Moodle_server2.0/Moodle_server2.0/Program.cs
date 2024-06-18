using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Moodle_server2._0.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;
using Moodle_server2._0.WebSockets;

namespace Moodle_server2._0
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.



            
            builder.Services.AddDbContext<moodleDataContext>();
            builder.Services.AddSingleton < Moodle_server2._0.WebSockets.WebSocketManager>();
            builder.Services.AddTransient<WebSocketService>();


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

            
            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval=TimeSpan.FromMinutes(5)
            };

            app.UseWebSockets(webSocketOptions);

            


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

            app.Use(async (context, next) =>
            {
                if(context.Request.Path=="/ws")
                {
                    var websocketService = context.RequestServices.GetRequiredService<WebSocketService>();
                    await websocketService.HandleWebSocketConnection(context);
                }
                else await next();
               
            });

            app.Run();
        }

    }
}