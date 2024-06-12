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
            var courseBack=new List<CourseBack>();
            foreach (var c in coursesList)
            {
                CourseBack b=new CourseBack();
                b.Code=c.code;
                b.Name=c.name;
                b.Credit=c.credit;

                courseBack.Add(b);
            }
            var coursesJson= JsonConvert.SerializeObject(courseBack);


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
            var courseBack = new List<CourseBack>();
            foreach (var c in courses)
            {
                CourseBack b = new CourseBack();
                b.Code = c.code;
                b.Name = c.name;
                b.Credit = c.credit;

                courseBack.Add(b);
            }

            return JsonConvert.SerializeObject(courseBack);
        }

 
    }
}
