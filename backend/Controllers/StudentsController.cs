using Microsoft.AspNetCore.Mvc;
using LMS.Data;
using LMS.Models;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
public IActionResult GetAll(
    [FromQuery] int? year, 
    [FromQuery] int? courseId,
    [FromQuery] string? name,
    [FromQuery] int? rollNumber,
    [FromQuery] long? phone,
    [FromQuery] string? email)
{
    var query = _context.Students.AsQueryable();
    
    // Apply filters
    if (year.HasValue)
        query = query.Where(s => s.Year == year.Value);
    
    if (courseId.HasValue)
        query = query.Where(s => s.CourseId == courseId.Value);
    
    if (!string.IsNullOrEmpty(name))
        query = query.Where(s => s.Address.Contains(name)); // No name field, assuming this is a search across other text fields
    
    if (rollNumber.HasValue)
        query = query.Where(s => s.RollNumber == rollNumber.Value);
    
    if (phone.HasValue)
        query = query.Where(s => s.Phone == phone.Value);
    
    if (!string.IsNullOrEmpty(email))
        query = query.Where(s => s.Email.Contains(email));
    
    return Ok(query.ToList());
}

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var student = _context.Students.Find(id);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpPost]
    public IActionResult Add(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Student updatedStudent)
    {
        var student = _context.Students.Find(id);
        if(student == null) return NotFound();
        student.RollNumber = updatedStudent.RollNumber;
        student.Email = updatedStudent.Email;
        student.Phone = updatedStudent.Phone;
        student.Address = updatedStudent.Address;
        student.CourseId = updatedStudent.CourseId;
        student.Photo = updatedStudent.Photo;
        student.Year = updatedStudent.Year;
        student.Password = updatedStudent.Password;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = _context.Students.Find(id);
        if(student == null) return NotFound();
        _context.Students.Remove(student);
        _context.SaveChanges();
        return NoContent();
    }
}