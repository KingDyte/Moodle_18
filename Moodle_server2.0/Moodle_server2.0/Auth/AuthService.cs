using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moodle_server2._0.Models;
using System.Security.Cryptography;

namespace Moodle_server2._0.Auth
{
    public interface IAuthService
    {
        Task AddUser(AddUser addUser);
        Task<IActionResult> Login(LoginModel Login);
        public Task<UserModel?> GetUser();
    }
    public class AuthService : IAuthService
    {
        private readonly IConfiguration conf;
        private readonly moodleDataContext data;
        private readonly IHttpContextAccessor HCA;

        public AuthService(IConfiguration conf, moodleDataContext data, IHttpContextAccessor hCA)
        {
            this.conf = conf;
            this.data = data;
            HCA = hCA;
            AccesControlList.InitializeACL(data);
        }

        public async Task<IActionResult> Login(LoginModel login)
        {
            return null;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

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
        public Task<UserModel?> GetUser() 
        {
            return data.users!.FirstOrDefaultAsync();
        }

    }
}
