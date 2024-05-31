using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle_server2._0
{
    public class DegreesModel
    {
        public int Id { get; set; }
        public string name { get; set; }

        public DegreesModel() { }  
        public DegreesModel(int a, string b)
        {
            this.Id = a;
            this.name = b;
        }
    }
}
