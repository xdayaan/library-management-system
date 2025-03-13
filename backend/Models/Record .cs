namespace LMS.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    public class Record
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Book_Id")]
        public int Book_Id { get; set; } = default!;
        public DateTime Issue_Date { get; set; }
        public required DateTime Return_Date { get; set; }
        public required DateTime Due_Date { get; set; }
        public int Fine { get; set; } = default!;
    }
}
