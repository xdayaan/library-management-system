namespace LMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    public class Student
    {
        [Key]
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
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Gender { get; set; } = default!;
        public string FatherName { get; set; } = default!;

        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }

        public virtual ICollection<Record>? Records { get; set; }
    }
}
