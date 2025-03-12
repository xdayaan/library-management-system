using Microsoft.EntityFrameworkCore;
using LMS.Models;

namespace LMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Book> Books { get; set; } = default!;
    }
}