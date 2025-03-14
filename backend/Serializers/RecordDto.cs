using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.Serializers
{
    public class RecordDto
    {
        [Required]
        public int BookId { get; set; }
        
        [Required]
        public int StudentId { get; set; }
        
        [Required]
        public DateTime IssueDate { get; set; }
        
        // Return date is now optional
        public DateTime? ReturnDate { get; set; }
        
        [Required]
        public DateTime DueDate { get; set; }
        
        public int Fine { get; set; } = 0;
    }
}
