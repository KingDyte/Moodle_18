using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle_server2._0
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int DegreeID { get; set; }

        public UserModel() { }
        public UserModel(int a, string b, string c, string d, int e)
        {
            this.Id = a;
            this.Username = b;
            this.Name = c;
            this.Password = d;
            this.DegreeID = e;
        }
    }
}
