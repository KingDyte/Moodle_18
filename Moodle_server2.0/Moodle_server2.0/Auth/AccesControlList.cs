using Moodle_server2._0.Models;
using System.Runtime.CompilerServices;

namespace Moodle_server2._0.Auth
{
    public class AccesControlList
    {
        private static Dictionary<string, string> accessControlList = new Dictionary<string, string>();
        private static bool initialized = false;



        public static void AddUser(string UName)
        {
            if (!accessControlList.ContainsKey(UName))
                accessControlList.Add(UName, "");
        }

        public static void AddPermission(string UName,string perm)
        {
            if (accessControlList.ContainsKey(UName))
                accessControlList[UName] = perm;

            else throw new Exception("User not found in Access Control List!");
        }

        public static bool HasPermission(string UName, string perm)
        {
            if(accessControlList.ContainsKey(UName))
                return accessControlList[UName] == perm;

            else throw new Exception("User not found in Access Contol List");
        }

        public static string GetPermission(string UName)
        {
            if (accessControlList[UName] != null)
                return accessControlList[UName];

            else throw new Exception("The User Has no permissions");
        }

        public static void InitializeACL(moodleDataContext data)
        {
            if (!initialized)
            {
                int teacherId = data.degrees.SingleOrDefault(x => x.name == "Oktató").id;
                //RoleEnum role;
                foreach (var u in data.users)
                {
                    if (u.degree_id == teacherId)
                    {
                        AccesControlList.AddUser(u.username);
                        AccesControlList.AddPermission(u.username, Roles.Teacher);
                    }
                    else
                    {
                        AccesControlList.AddUser(u.username);
                        AccesControlList.AddPermission(u.username, Roles.Student);
                    }
                    Console.WriteLine(u.username);
                }
                initialized = true;
            }
        }



    }
}
