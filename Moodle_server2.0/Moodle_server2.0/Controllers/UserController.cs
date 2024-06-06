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

namespace Moodle_server2._0.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly moodleDataContext data;
        //private readonly AuthController AuthC;
        public UserController(moodleDataContext d/*, AuthController authC*/)
        {
            data = d;
            //AuthC = authC;
        }
        [HttpGet("users/{CourseCode}")]
        public async Task<IActionResult> UserOnCourse(string CourseCode)
        {
            int courseId=data.courses.ToList().Where(x=>x.code==CourseCode).First().id;
            
            var allUser=data.users.ToList();
            var pList= new List<UserBack>();
            foreach(var e in data.mycourses)
            {
                var s=allUser.Find(x=>x.id==e.id);
                if (s == null) continue;

                UserBack back=new UserBack();
                back.Username = s!.username;
                back.Name = s!.name;
                back.Degree = data.degrees.FirstOrDefault(x => x.id == s.degree_id)!.name;
               
                pList.Add(back);
            }

            var pJson=JsonConvert.SerializeObject(pList);
            return Content(pJson);
        }

        [HttpPut("enroll/{courseCode}")]
        //[Authorize(Roles="student")]
        public async Task<IActionResult> EnrollStudent(string courseCode, [FromBody] string username)
        {
            int courseId=data.courses.ToList().Where(x=>x.code==courseCode).First().id;
            int userId=data.users.ToList().Where(x=>x.username==username).First().id;
            await Console.Out.WriteLineAsync( $"------- userId: {userId},   courseId: {courseId}-----");
            
            var sds=data.mycourses.FirstOrDefault(x=>x.id==userId && x.course_id==courseId);
            if (sds==null)
            {
                data.mycourses.Add(new MycourseModel(data.mycourses.Count()+1,userId,courseId));

                await Console.Out.WriteLineAsync($"-------id: {data.mycourses.Count()} userId: {userId},   courseId: {courseId}-----");
                await data.SaveChangesAsync();
                return Ok();
            }


            return BadRequest();
        }



        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
