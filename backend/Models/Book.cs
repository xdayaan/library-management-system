namespace LMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    public enum Status
    {
        Active = 1,
        Inactive = 0
    }

    public class Book
    {
        [Key]
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public string Name { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string Publisher { get; set; } = default!;
        public string Subject { get; set; } = default!;
        public int CourseId { get; set; }
        public DateTime PublishedDate { get; set; }
        public int Stock { get; set; }
        public Status Status { get; set; }

        // Additional typical library fields
        public string ISBN { get; set; } = default!;
        public int Edition { get; set; }
        public int Pages { get; set; }
        public string RackNumber { get; set; } = default!;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }

        public virtual ICollection<Record>? Records { get; set; }
    }
}
