using Microsoft.AspNetCore.Mvc;
using Moodle_server2._0.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Authentication;
using System.Security.AccessControl;

namespace Moodle_server2._0.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly moodleDataContext data;
        private readonly IConfiguration conf;

        public AuthController(moodleDataContext data, IConfiguration conf)
        {
            this.data = data;
            this.conf = conf;
            AccesControlList.InitializeACL(data);
        }
    }
}
