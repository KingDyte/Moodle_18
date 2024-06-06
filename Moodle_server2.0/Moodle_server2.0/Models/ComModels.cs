namespace Moodle_server2._0.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class AddCourse
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Department { get; set; }
        public int Credit { get; set; }
    }
    public class AddEvent
    {
        public string Username { get; set; }
        public string CourseName { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public DateTime Date { get; set; }
    }

    public class AddUser
    {
        public string Username { get; set;}
        public string Name { get;set; }
        public string Password { get; set; }
        public int DegreeId { get; set; }
    }
    public class UserBack
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
    }
}
