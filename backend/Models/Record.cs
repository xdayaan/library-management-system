namespace LMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Record
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public DateTime IssueDate { get; set; }
        
        // Changed to nullable DateTime
        public DateTime? ReturnDate { get; set; }
        
        public required DateTime DueDate { get; set; }
        public int Fine { get; set; } = default!;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Student? Student { get; set; }
    }
}
