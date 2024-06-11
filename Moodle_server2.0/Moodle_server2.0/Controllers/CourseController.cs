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

 
    }
}
