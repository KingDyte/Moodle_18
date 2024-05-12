using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle_server2._0
{
    public class MycourseModel
    {
        public long Id { get; set; }
        public long user_id { get; set; }
        public long course_id { get; set; }

        public MycourseModel(long a, long b, long c)
        {
            this.Id = a;
            this.user_id = b;
            this.course_id = c;
        }
    }
}
