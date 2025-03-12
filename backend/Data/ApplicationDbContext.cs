using Microsoft.EntityFrameworkCore;
using LMS.Models;
using Microsoft.VisualBasic;

namespace LMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Librarian> Librarian { get; set; } = default!;
        public DbSet<Record> Record { get; set; } = default!;

    }
}