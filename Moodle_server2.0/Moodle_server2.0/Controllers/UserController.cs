using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Moodle_server2._0.Models;
using Moodle_server2._0.Auth;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.AspNetCore.Mvc;

namespace Moodle_server2._0.Controllers
{
    [Route("api/user")]
    [ApiController]
    //[Authorize]
    public class UserController : Controller
    {

        private readonly moodleDataContext data;
        //private readonly AuthController AuthC;
        public UserController(moodleDataContext d/*, AuthController authC*/)
        {
            data = d;
            //AuthC = authC;
        }
        [HttpGet("{CourseCode}/enrolled")]
        public async Task<IActionResult> UserOnCourse(string CourseCode)
        {
            int courseId = data.courses.ToList().Where(x => x.code == CourseCode).First().id;

            var allUser = data.users.ToList();
            var pList = new List<UserBack>();
            foreach (var e in data.mycourses)
            {
                var s = allUser.Find(x => x.id == e.id);
                if (s == null) continue;

                UserBack back = new UserBack();
                back.Username = s!.username;
                back.Name = s!.name;
                back.Degree = data.degrees.FirstOrDefault(x => x.id == s.degree_id)!.name;

                pList.Add(back);
            }

            var pJson = JsonConvert.SerializeObject(pList);
            return Content(pJson);
        }

        [HttpPut("enroll/{courseCode}")]
        public async Task<IActionResult> EnrollStudent(string courseCode, [FromBody] string username)
        {
            CanEnroll cr = new CanEnroll(data);
            if (cr.CheckIfCanEnroll(username, courseCode))
            {
                var userId = data.users.FirstOrDefault(x => x.username.Equals(username)).id;
                var courseId = data.courses.FirstOrDefault(x => x.code.Equals(courseCode)).id;
                var idx = data.mycourses.Count() + 1;
                data.mycourses.Add(new MycourseModel(idx, courseId, userId));
                data.SaveChanges();
                return Ok();
            }
            else

                return BadRequest();
        }

        [HttpGet("{username}/courses")]
        public async Task<IActionResult> ListMyCourses(string username)
        {
            var userId = data.users.FirstOrDefault(x=>x.username.Equals(username)).id;

            var back = new List<CourseBack>();

            foreach (var c in data.mycourses)
                if (c.user_id == userId)
                {
                    var course = data.courses.FirstOrDefault(x => x.id == c.course_id);

                    CourseBack courseBack = new CourseBack();
                    courseBack.Code = course.code;
                    courseBack.Name = course.name;
                    courseBack.Credit = course.credit;

                    back.Add(courseBack);
                }


            var cJson=JsonConvert.SerializeObject(back);
            return Content(cJson);
        }

        [HttpGet("{username}/events")]
        public async Task<IActionResult> NextEvent(string username)
        {
            int userId = data.users.FirstOrDefault(x=>x.username.Equals(username)).id;
            var usersCourses = data.mycourses.Where(x => x.user_id == userId).ToList();
           

            var back = new List<EventBack>();
            foreach (var u in usersCourses)
            {
                var ev = data.events.Where(x => x.course_id == u.course_id);
                foreach (var e in ev) 
                { 
                    EventBack eventBack = new EventBack();
                    eventBack.Name = e.name;
                    eventBack.Course = data.courses.FirstOrDefault(x => x.id == e.course_id).name;
                    eventBack.Description= e.description;
                    eventBack.Date = e.date;

                    back.Add(eventBack);
                }
            }

            //myCourse alapján megnézni kinek van az addot órája és annak megfelelő eventeket visszaadni EventBack ként
            var evsJson=JsonConvert.SerializeObject(back);
            return Content(evsJson);
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
