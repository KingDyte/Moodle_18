using Microsoft.AspNetCore.Authorization;
using Moodle_server2._0.Models;
using System.Security.Claims;

namespace Moodle_server2._0.Auth
{

    public class CanEnroll 
    {
        private readonly moodleDataContext data;

        public CanEnroll(moodleDataContext data)
        {
            this.data = data;
        }

        public bool CheckIfCanEnroll(string UName,string courseCode)
        {
            var user=data.users.FirstOrDefault(x=>x.username.Equals(UName));
            if (user == null)  return false;
            if (user.degree_id == data.degrees.FirstOrDefault(x => x.name.Equals("Oktató")).id) return true;
            else
            {
                var courseId=data.courses.FirstOrDefault(x=>x.code.Equals(courseCode)).id;
                var degreeId= user.degree_id;

                var apDegrees = data.approved_degrees.ToList();
                Console.WriteLine($"----------------degreeId: {degreeId},  courseId {courseId}");

                if (apDegrees.Exists(x=>x.degree_id==degreeId && x.course_id==courseId)) return true;
            }
            return false;
        }
    }
}