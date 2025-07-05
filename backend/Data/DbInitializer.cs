using LMS.Models;
using LMS.Utils;

namespace LMS.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if any librarian exists
            if (context.Librarian.Any())
            {
                return;   // DB has been seeded
            }

            var librarians = new Librarian[]
            {
                new Librarian
                {
                    Name = "Admin Librarian",
                    Username = "admin",
                    Password = PasswordHandler.Encrypt("admin123"), // Encrypt the password
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow
                }
            };

            foreach (var l in librarians)
            {
                context.Librarian.Add(l);
            }
            context.SaveChanges();
        }
    }
}
