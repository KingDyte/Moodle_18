using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle_server2._0.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        // Statikus felhasznalok
        List<UserModel> felhasznalok = new List<UserModel>()
        {
            new UserModel(1, "teszt@ors1.hu", "Ors1", "jelszo1", 0),
            new UserModel(2, "teszt@ors2.hu", "Ors2", "jelszo2", 1),
            new UserModel(3, "teszt@ors3.hu", "Ors3", "jelszo3", 2)
        };

        [HttpGet("{id}")]
        public string GetByID(int id)
        {
            int x = felhasznalok.FindIndex(x => x.Id == id);

            if (x != -1)
            {
                return JsonSerializer.Serialize<UserModel>(felhasznalok[x]);
            }
            else
            {
                this.HttpContext.Response.StatusCode = 400;
                return "Hiba";
            }
        }

        [HttpPost("login/{uName}/{pw}")]
        public string Login(string uName, string pw)
        {
            int x = felhasznalok.FindIndex(x => x.Username == uName);

            if (x != -1)
            {
                return JsonSerializer.Serialize<UserModel>(felhasznalok[x]);
                Console.WriteLine(felhasznalok[x].Name);
            }
            else
            {
                this.HttpContext.Response.StatusCode = 400;
                return "Nincs ilyen felhasznalo ezzel az emaillel!";
            }
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
