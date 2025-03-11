namespace LMS.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public enum Status
{
    Active = 1,
    Inactive = 0
}


    public class Book
    {
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

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
    }
}