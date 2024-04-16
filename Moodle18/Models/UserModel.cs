using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle18
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public long DegreeID { get; set; }

        public UserModel(long a, string b, string c, string d, long e)
        {
            this.Id = a;
            this.Username = b;
            this.Name = c;
            this.Password = d;
            this.DegreeID = e;
        }
    }
}
