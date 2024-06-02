using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle_server2._0
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int credit { get; set; }
        public string department { get; set; }

        public CourseModel() { }
        public CourseModel(int a, string b, string c, int d, string department)
        {
            this.Id = a;
            this.code = b;
            this.name = c;
            this.credit = d;
            this.department = department;
        }
    }
}
