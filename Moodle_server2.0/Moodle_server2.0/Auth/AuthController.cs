using Microsoft.AspNetCore.Mvc;
using Moodle_server2._0.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Authentication;
using System.Security.AccessControl;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Moodle_server2._0.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly moodleDataContext data;

        public AuthController( moodleDataContext data)
        {
            this.data = data;
        }

        [HttpPost("login")]
        public async Task<UserBack> Login(LoginModel login)
        {
            var user=await data.users!.FirstOrDefaultAsync(x=>x.username==login.UserName)??throw new Exception("Rossz felhasználónév vagy jelszó");

            if (VerifyPassword(login.Password, user.passwordHash!, user.passwordSalt!))
                return new UserBack
                {
                    Username = user.username,
                    Name = user.name,
                    Degree = data.degrees.FirstOrDefault(x => x.id == user.degree_id).name!
                };
            else throw new Exception("Rossz felhasználónév vagy jelszó");
        }

        private bool VerifyPassword(string password, byte[] pwHash, byte[] pwSalt)
        {
            using(var hmac=new HMACSHA512(pwSalt)) 
            {
                var convHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return convHash.SequenceEqual(pwHash);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        [HttpPost("AddUser")]
        public async Task AddUser(AddUser AddUser)
        {
            UserModel user = new UserModel();
            user.username = AddUser.Username;
            user.name = AddUser.Name;
            user.degree_id = AddUser.DegreeId;


            if (await data.users.AnyAsync(x => x.username.ToLower().Equals(AddUser.Username.ToLower())))
                throw new Exception("Felhasználó már létezik");

            CreatePasswordHash(AddUser.Password, out byte[] pwHash, out byte[] pwSalt);

            user.passwordHash = pwHash;
            user.passwordSalt = pwSalt;
            int uId = data.users.Count() + 1;
            await Console.Out.WriteLineAsync($"-----id:{uId}");
            await Console.Out.WriteLineAsync($"--------username: {user.username}, id: {user.id}, name: {user.name}");

            await data.users!.AddAsync(user);
            await data.SaveChangesAsync();
        }

    }
}
