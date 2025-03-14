namespace LMS.Serializers
{
    public class StudentDto
        {
            public string Name { get; set; }
            public int Year { get; set; }
            public int RollNumber { get; set; }

            public int CourseId { get; set; }
            public long Phone { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public IFormFile Photo { get; set; }
            public string Password { get; set; }
            public string Gender { get; set; } = default!;
            public string FatherName { get; set; } = default!;
        }
}