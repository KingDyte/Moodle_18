using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle_server2._0
{
    public class Approved_degreesModel
    {
        public int Id { get; set; }
        public int course_id { get; set; }
        public int degree_id { get; set; }

        public Approved_degreesModel() { }  
        public Approved_degreesModel(int a, int b, int c)
        {
            this.Id = a;
            this.course_id = b;
            this.degree_id = c;
        }
    }
}
