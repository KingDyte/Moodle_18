using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Moodle_server2._0.Models
{
    public class moodleDataContext : DbContext
    {
        public moodleDataContext() { }
        public moodleDataContext(DbContextOptions options) : base(options) { }


        public DbSet<UserModel> users { get; set; }
        public DbSet<MycourseModel> mycourses { get; set; }
        public DbSet<EventsModel> events { get; set; }
        public DbSet<DegreesModel> degrees { get; set; }
        public DbSet<CourseModel> courses { get; set; }
        public DbSet<Approved_degreesModel> approved_degrees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Moodle_db.db");
        }
    }
}
