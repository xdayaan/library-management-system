namespace LMS.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Student
    {
        public int Id { get; set; }
        public int RollNumber { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public long Phone { get; set; }
        public string Address { get; set; } = default!;
        public int CourseId { get; set; }
        public string Photo { get; set; } = default!;
        public int Year { get; set; }
        public string Password { get; set; } = default!;

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
    }
}