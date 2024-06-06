using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Moodle_server2._0.Models;
using Moodle_server2._0.Auth;

namespace Moodle_server2._0.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        // Statikus felhasznalok
        //List<UserModel> felhasznalok = new List<UserModel>()
        //{
        //    new UserModel(1, "teszt@ors1.hu", "Ors1", "jelszo1", 0),
        //    new UserModel(2, "teszt@ors2.hu", "Ors2", "jelszo2", 1),
        //    new UserModel(3, "teszt@ors3.hu", "Ors3", "jelszo3", 2)
        //};

        private readonly moodleDataContext data;
        private readonly AuthController AuthC;
        public UserController(moodleDataContext d, AuthController authC)
        {
            data = d;
            AuthC = authC;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUser user)
        {
            await AuthC.AddUser(user);
            return Ok();
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
