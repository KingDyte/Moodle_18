using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle18
{
    public class Approved_degreesModel
    {
        public long Id { get; set; }
        public long course_id { get; set; }
        public long degree_id { get; set; }

        public Approved_degreesModel(long a, long b, long c)
        {
            this.Id = a;
            this.course_id = b;
            this.degree_id = c;
        }
    }
}
