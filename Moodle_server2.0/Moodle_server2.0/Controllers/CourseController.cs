using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Moodle_server2._0.Models;



namespace Moodle_server2._0.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly moodleDataContext data;
        public CourseController(moodleDataContext d)
        {
            data = d;
        }

        [HttpGet]
        public async Task<ActionResult> GetCourses()
        {

            var coursesList=data.courses.ToList();
            var coursesJson= JsonConvert.SerializeObject(coursesList);


            return Content(coursesJson);
        }

        [HttpGet("{courseId}")]
        public string GetCourseById(int courseId)
        {
            var course = data.courses.Where(x => x.id == courseId);
            return JsonConvert.SerializeObject(course);
        }

        [HttpGet("dep/{depName}")]
        public string ListCoursesByDep(string depName)
        {
            var courses = data.courses.Where(x => x.department == depName);
            return JsonConvert.SerializeObject(courses);
        }

 







        //[HttpGet("{courseId}/events")]
        //public string GetEventsForId(int courseId)
        //{
        //    List<EventsModel> sorted = eventek.Where(y => y.course_id == courseId).ToList();

        //    return JsonSerializer.Serialize(sorted);
        //}

        //[HttpGet("events/{userId}")]
        //public string GetEventsForUserId(int userId)
        //{
        //    List<MycourseModel> sort = kapcsoloT.Where(x => x.user_id == userId).ToList();
        //    List<EventsModel> sorted = new List<EventsModel>();

        //    foreach (var item in sort)
        //    {
        //        sorted.Add(eventek.Find(x => x.course_id == item.course_id));
        //    }

        //    return JsonSerializer.Serialize(sorted);
        //}

        //[HttpGet("{courseId}/degrees")]
        //public string GetDegrees(int courseId)
        //{
        //    List<Approved_degreesModel> sorted = apprDegrees.Where(x => x.course_id == courseId).ToList();

        //    return JsonSerializer.Serialize(sorted);
        //}

        //[HttpGet("{degreeId}/degree")]
        //public string GetDegreeById(int degreeId)
        //{
        //    DegreesModel d = degrees.Find(x => x.Id == degreeId);

        //    return JsonSerializer.Serialize(d);
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
