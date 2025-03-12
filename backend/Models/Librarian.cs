using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LMS.Models
{
    public class Librarian
    {
        [Key]
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; } 
        public required string Name { get; set;}

    }
}
