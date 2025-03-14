using Microsoft.AspNetCore.Mvc;
using LMS.Data;
using LMS.Models;
using LMS.Utils;
using LMS.Serializers;
using LMS.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

[Authorize]
[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly JsonSerializerOptions _jsonOptions;

    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
        _jsonOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };
    }

    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] int? year,
        [FromQuery] int? courseId,
        [FromQuery] string? name,
        [FromQuery] int? rollNumber,
        [FromQuery] long? phone,
        [FromQuery] string? email,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortBy = "created_on",
        [FromQuery] string? orderBy = "desc")
    {
        var query = _context.Students
            .Include(s => s.Course)
            .Include(s => s.Records)
            .AsQueryable();

        // Apply filters
        if (year.HasValue)
            query = query.Where(s => s.Year == year.Value);
        if (courseId.HasValue)
            query = query.Where(s => s.CourseId == courseId.Value);
        if (!string.IsNullOrEmpty(name))
            query = query.Where(s => s.Name.Contains(name));
        if (rollNumber.HasValue)
            query = query.Where(s => s.RollNumber == rollNumber.Value);
        if (phone.HasValue)
            query = query.Where(s => s.Phone == phone.Value);
        if (!string.IsNullOrEmpty(email))
            query = query.Where(s => s.Email.Contains(email));

        // Sorting
        switch (sortBy?.ToLower())
        {
            case "name":
                query = orderBy?.ToLower() == "asc"
                    ? query.OrderBy(s => s.Name)
                    : query.OrderByDescending(s => s.Name);
                break;
            case "created_on":
                query = orderBy?.ToLower() == "asc"
                    ? query.OrderBy(s => s.CreatedOn)
                    : query.OrderByDescending(s => s.CreatedOn);
                break;
            case "updated_on":
                query = orderBy?.ToLower() == "asc"
                    ? query.OrderBy(s => s.UpdatedOn)
                    : query.OrderByDescending(s => s.UpdatedOn);
                break;
            case "year":
                query = orderBy?.ToLower() == "asc"
                    ? query.OrderBy(s => s.Year)
                    : query.OrderByDescending(s => s.Year);
                break;
            default:
                query = query.OrderByDescending(s => s.CreatedOn);
                break;
        }

        // Pagination
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        var skip = (page - 1) * pageSize;
        var total = query.Count();
        
        var data = query.Skip(skip).Take(pageSize)
            .Select(s => new {
                s.Id,
                s.RollNumber,
                s.Name,
                s.Email,
                s.Phone,
                s.Address,
                s.CourseId,
                s.Photo,
                s.Year,
                s.Gender,
                s.FatherName,
                s.CreatedOn,
                s.UpdatedOn,
                TotalRecords = s.Records != null ? s.Records.Count : 0,
                RecordIds = s.Records != null ? s.Records.Select(r => r.Id).ToArray() : Array.Empty<int>(),
                Course = s.Course != null ? new {
                    s.Course.Id,
                    s.Course.Name
                } : null
            })
            .ToList();

        var result = new
        {
            TotalItems = total,
            Page = page,
            PageSize = pageSize,
            Items = data
        };

        var json = JsonSerializer.Serialize(result, _jsonOptions);
        return Content(json, "application/json");
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var student = _context.Students
            .Include(s => s.Course)
            .Include(s => s.Records)
            .FirstOrDefault(s => s.Id == id);
        if (student == null) return NotFound();
        
        // Create a response with the student's details and summary info
        var studentResponse = new
        {
            student.Id,
            student.RollNumber,
            student.Name,
            student.Email,
            student.Phone,
            student.Address,
            student.CourseId,
            student.Photo,
            student.Year,
            student.Gender,
            student.FatherName,
            student.CreatedOn,
            student.UpdatedOn,
            TotalRecords = student.Records?.Count ?? 0,
            RecordIds = student.Records?.Select(r => r.Id).ToArray() ?? Array.Empty<int>(),
            Course = student.Course == null ? null : new
            {
                student.Course.Id,
                student.Course.Name,
                student.Course.CreatedOn,
                student.Course.UpdatedOn
            }
        };
        
        var json = JsonSerializer.Serialize(studentResponse, _jsonOptions);
        return Content(json, "application/json");
    }

    [HttpPost]
    public IActionResult Add([FromForm] StudentDto studentDto)
    {
        var student = new Student
        {
            Name = studentDto.Name,
            Year = studentDto.Year,
            RollNumber = studentDto.RollNumber,
            Phone = studentDto.Phone,
            Email = studentDto.Email,
            CourseId = studentDto.CourseId,
            Address = studentDto.Address,
            UpdatedOn = DateTime.UtcNow,
            CreatedOn = DateTime.UtcNow,
            FatherName = studentDto.FatherName,
            Gender = studentDto.Gender,
            Password = PasswordHandler.Encrypt(studentDto.Password),
            Photo = ""
        };

        if (studentDto.Photo != null)
        {
            var s3Url = UploadToS3.UploadFile(studentDto.Photo);
            student.Photo = s3Url;
        }

        _context.Students.Add(student);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromForm] StudentDto studentDto)
    {
        var student = _context.Students.Find(id);
        if (student == null) return NotFound();

        student.Name = studentDto.Name;
        student.Year = studentDto.Year;
        student.RollNumber = studentDto.RollNumber;
        student.Phone = studentDto.Phone;
        student.Email = studentDto.Email;
        student.Address = studentDto.Address;
        student.CourseId = studentDto.CourseId;
        student.FatherName = studentDto.FatherName;
        student.UpdatedOn = DateTime.UtcNow;
        student.Gender = studentDto.Gender;
        student.Password = PasswordHandler.Encrypt(studentDto.Password);

        if (studentDto.Photo != null)
        {
            var fileName = Path.GetFileName(studentDto.Photo.FileName);
            var savePath = Path.Combine("wwwroot", "uploads", fileName);
            using var stream = new FileStream(savePath, FileMode.Create);
            studentDto.Photo.CopyTo(stream);
            student.Photo = $"/uploads/{fileName}";
        }

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = _context.Students.Find(id);
        if (student == null) return NotFound();
        _context.Students.Remove(student);
        _context.SaveChanges();
        return NoContent();
    }
}