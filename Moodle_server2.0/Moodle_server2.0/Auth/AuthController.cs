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
        //private readonly IAuthService autS;
        private readonly moodleDataContext data;

        public AuthController( moodleDataContext data)
        {
            //this.autS = AS;
            this.data = data;
        }

        //[HttpPost("AddUser")]
        //public async Task<IActionResult> AddUser(AddUser newUser)
        //{
        //    await autS.AddUser(newUser);
        //    return Ok();
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginModel login)
        //{
        //    if (login == null) return BadRequest("Nincs adat");

        //    var user = data.users.SingleOrDefault(x => x.username == login.UserName);
        //    if (user == null) return Unauthorized("Nincs ilyen felhasználó");
        //    await Console.Out.WriteLineAsync($"uname:{user.username}, pw: {user.password}, login pw: {login.Password}");
        //    if (PasswordHandler.VerifyPassword(user.username, user.password, login.Password))
        //    {
        //        bool oktato = user.degree_id == data.degrees.SingleOrDefault(x => x.name == "Oktató").id;

        //        var claims = new[]
        //        {
        //            new Claim(ClaimTypes.NameIdentifier,user.id.ToString()),
        //            new Claim(ClaimTypes.Role,user.degree_id.ToString()),
        //            new Claim(ClaimTypes.GivenName,user.username),
        //            new Claim(ClaimTypes.Name,user.name),
        //        };

        //        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(conf["Jwt:SecretKey"]));
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //        var token = new JwtSecurityToken(
        //            issuer: conf["Jwt:Issuer"],
        //            audience: conf["Jwt:Audience"],
        //            claims: claims,
        //            expires: DateTime.UtcNow.AddSeconds(180),
        //            signingCredentials: creds
        //            );
        //        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        //        return Ok(new { message = "Sikeres Bejelentkezés", UName = user.username, userId = user.id, isOktato = oktato, token = jwtToken });
        //    }

        //    return Unauthorized("Helytelen Bejelentkezési adatok");
        //}
        //nem jó

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

            var uid = data.users.FirstOrDefault().id;
            if (uid == null)
            {
                await Console.Out.WriteLineAsync("---------nulla-------");
                user.id = 1;
            }
            else user.id = uid + 1;

            await Console.Out.WriteLineAsync($"--------username: {user.username}, id: {user.id}, name: {user.ToString}");
        }

    }
}
