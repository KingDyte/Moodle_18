using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle_server2._0
{
    public class UserModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public int degree_id { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }

        public UserModel() { }
        public UserModel(int a, string b, string c, int e, byte[] ph, byte[]ps)
        {
            this.id = a;
            this.username = b;
            this.name = c;
            this.degree_id = e;
            this.passwordHash = ph;
            this.passwordSalt = ps;
        }
    }
}
