using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle_server2._0
{
    public class EventsModel
    {
        public int id { get; set; }
        public int course_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public EventsModel() { }
        public EventsModel(int a, int b, string c, string d, DateTime date)
        {
            this.id = a;
            this.course_id = b;
            this.name = c;
            this.description = d;
            this.date = date;
        }
    }
}
