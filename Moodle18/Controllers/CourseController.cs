using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle18.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        List<CourseModel> kurzusok = new List<CourseModel>()
        {
            new CourseModel(0, "xyz0", "Kurzus 0", 54631),
            new CourseModel(1, "xyz1", "Kurzus 1", 1768),
            new CourseModel(2, "xyz2", "Kurzus 2", 16),
            new CourseModel(3, "xyz3", "Kurzus 3", 8791),
            new CourseModel(4, "xyz4", "Kurzus 4", 1768),
            new CourseModel(5, "xyz5", "Kurzus 5", 541),
            new CourseModel(6, "xyz6", "Kurzus 6", 2341),
            new CourseModel(7, "xyz7", "Kurzus 7", 14325)
        };

        [HttpGet]
        public string Get()
        {
            return JsonSerializer.Serialize(kurzusok);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            int x = kurzusok.FindIndex(x => x.Id == id);
            if(x != -1)
            {
                return JsonSerializer.Serialize<CourseModel>(kurzusok[x]);
            } else
            {
                this.HttpContext.Response.StatusCode = 400;
                return "Hiba";
            }
        }
    }
}
