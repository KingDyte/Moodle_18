using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle_server2._0
{
    public class EventsModel
    {
        public int Id { get; set; }
        public int course_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public EventsModel() { }
        public EventsModel(int a, int b, string c, string d)
        {
            this.Id = a;
            this.course_id = b;
            this.name = c;
            this.description = d;
        }
    }
}
