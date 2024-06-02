using System.Security.Cryptography;
using System.Text;

namespace Moodle_server2._0.Controllers
{
    public class PasswordHandler
    {
        public static bool VerifyPassword(string UName,string Hash,string pw)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(UName+pw);
            byte[] hashOut;
            using(SHA256 sha256 = SHA256.Create()) 
            {
                hashOut=sha256.ComputeHash(bytes);
            }
            string hash=Convert.ToBase64String(hashOut);
            if(Hash==hash) return true;
            else return false;
        }
    }
}
