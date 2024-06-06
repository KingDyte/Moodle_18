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
    [Authorize]
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
        [Authorize(Roles=Roles.Student)]
        public async Task<IActionResult> EnrollStudent(string courseCode, [FromBody] string username)
        {
            int courseId = data.courses.FirstOrDefault(x => x.code == courseCode).id;
            int userId = data.users.FirstOrDefault(x => x.username == username).id; 
            
            //try 
            //{
            //    courseId = data.courses.FirstOrDefault(x => x.code == courseCode).id;
            //    userId = data.users.FirstOrDefault(x => x.username == username).id;
            //}
            //catch (Exception e)
            //{
            //    return BadRequest("tárgy vagy személy nem létezik");
            //}

            int CheckIfExist=data.mycourses.Count(x=>x.user_id==userId && x.course_id==courseId);

            if (CheckIfExist==0)
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
