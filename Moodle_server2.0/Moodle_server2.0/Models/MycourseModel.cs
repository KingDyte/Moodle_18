using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle_server2._0
{
    public class MycourseModel
    {
        public int Id { get; set; }
        public int user_id { get; set; }
        public int course_id { get; set; }

        public MycourseModel() { }
        public MycourseModel(int a, int b, int c)
        {
            this.Id = a;
            this.user_id = b;
            this.course_id = c;
        }
    }
}
