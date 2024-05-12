using Microsoft.EntityFrameworkCore;

namespace Moodle_server2._0.Models
{
    public class moodleData: DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<MycourseModel> Mycourses { get; set; }
        public DbSet<EventsModel> Events { get; set; }
        public DbSet<DegreesModel> Degrees { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<Approved_degreesModel> Approved_degrees { get; set; }

        public moodleData(DbContextOptions options):base(options)
        {

        }
    }
}
