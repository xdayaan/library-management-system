using System;

namespace LMS.Serializers
{
    public class RecordSummaryDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int StudentId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Fine { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        
        // Include essential book information without circular references
        public string? BookName { get; set; }
        public int BookSerialNumber { get; set; }
        
        // Include essential student information without circular references
        public string? StudentName { get; set; }
        public int StudentRollNumber { get; set; }
    }
}
