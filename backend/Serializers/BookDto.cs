namespace LMS.Serializers
{
    using LMS.Models;
    using System.Collections.Generic;
    
    public class BookDto
    {
        public int SerialNumber { get; set; }
        public string Name { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string Publisher { get; set; } = default!;
        public string Subject { get; set; } = default!;
        public int CourseId { get; set; }
        public DateTime PublishedDate { get; set; }
        public int Stock { get; set; }
        public int Status { get; set; }
        public string ISBN { get; set; } = default!;
        public int Edition { get; set; }
        public int Pages { get; set; }
        public string RackNumber { get; set; } = default!;
        public Course? Course { get; set; }
        public ICollection<Record>? Records { get; set; }
    }
}