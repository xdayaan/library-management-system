namespace LMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual ICollection<Student>? Students { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}
