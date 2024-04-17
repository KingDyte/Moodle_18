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
            new CourseModel(3, "xyz3", "Kurzus 3", 8791)
        };
        
        List<MycourseModel> kapcsoloT = new List<MycourseModel>()
        {
            new MycourseModel(0, 1, 0),
            new MycourseModel(0, 2, 1),
            new MycourseModel(0, 1, 2),
            new MycourseModel(0, 3, 3),
        };
        
        List<EventsModel> eventek = new List<EventsModel>()
        {
            new EventsModel(1, 0, "esemeny 1", "leiras 1"),
            new EventsModel(1, 0, "esemeny 1", "leiras 1"),
            new EventsModel(1, 1, "esemeny 2", "leiras 2"),
            new EventsModel(1, 1, "esemeny 2", "leiras 2"),
            new EventsModel(1, 2, "esemeny 3", "leiras 3"),
            new EventsModel(1, 2, "esemeny 3", "leiras 3"),
            new EventsModel(1, 3, "esemeny 4", "leiras 4"),
            new EventsModel(1, 3, "esemeny 4", "leiras 4"),
        };

        List<DegreesModel> degrees = new List<DegreesModel>()
        {
            new DegreesModel(1, "Egyes"),
            new DegreesModel(2, "Kettes"),
            new DegreesModel(3, "Harmas"),
            new DegreesModel(4, "Negyes"),
            new DegreesModel(5, "Otos"),
        };

        List<Approved_degreesModel> apprDegrees = new List<Approved_degreesModel>()
        {
            new Approved_degreesModel(1, 0, 3),
            new Approved_degreesModel(2, 1, 4),
            new Approved_degreesModel(3, 2, 5),
            new Approved_degreesModel(4, 3, 1),
        };

        [HttpGet]
        public string Get()
        {
            return JsonSerializer.Serialize(kurzusok);
        }

        [HttpGet("{courseId}")]
        public string Get(int courseId)
        {
            int x = kurzusok.FindIndex(x => x.Id == courseId);
            if(x != -1)
            {
                return JsonSerializer.Serialize<CourseModel>(kurzusok[x]);
            } else
            {
                this.HttpContext.Response.StatusCode = 400;
                return "{'msg':'Hiba'}";
            }
        }

        [HttpGet("{courseId}/events")]
        public string GetEventsForId(int courseId)
        {
            List<EventsModel> sorted = eventek.Where(y => y.course_id == courseId).ToList();

            return JsonSerializer.Serialize(sorted);
        }
    
        [HttpGet("events/{userId}")]
        public string GetEventsForUserId(int userId)
        {
            List<MycourseModel> sort = kapcsoloT.Where(x => x.user_id == userId).ToList();
            List<EventsModel> sorted = new List<EventsModel>();

            foreach (var item in sort)
            {
                sorted.Add(eventek.Find(x => x.course_id == item.course_id));
            }

            return JsonSerializer.Serialize(sorted);
        }
    
        [HttpGet("{courseId}/degrees")]
        public string GetDegrees(int courseId)
        {
            List<Approved_degreesModel> sorted = apprDegrees.Where(x => x.course_id == courseId).ToList();

            return JsonSerializer.Serialize(sorted);
        }
    
        [HttpGet("{degreeId}/degree")]
        public string GetDegreeById(int degreeId)
        {
            DegreesModel d = degrees.Find(x => x.Id == degreeId);

            return JsonSerializer.Serialize(d);
        }
    }
}
