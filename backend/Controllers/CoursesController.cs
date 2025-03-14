using Microsoft.AspNetCore.Mvc;
using LMS.Data;
using LMS.Models;
using LMS.Serializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

[Authorize] 
[ApiController]
[Route("[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly JsonSerializerOptions _jsonOptions;

    public CoursesController(ApplicationDbContext context) 
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
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortBy = "name",
        [FromQuery] string? orderBy = "asc")
    {
        var query = _context.Courses
            .Include(c => c.Books)
            .Include(c => c.Students)
            .AsQueryable();

        // Sorting
        switch (sortBy?.ToLower())
        {
            case "name":
                query = orderBy?.ToLower() == "asc"
                    ? query.OrderBy(c => c.Name)
                    : query.OrderByDescending(c => c.Name);
                break;
            case "created_on":
                query = orderBy?.ToLower() == "asc"
                    ? query.OrderBy(c => c.CreatedOn)
                    : query.OrderByDescending(c => c.CreatedOn);
                break;
            case "updated_on":
                query = orderBy?.ToLower() == "asc"
                    ? query.OrderBy(c => c.UpdatedOn)
                    : query.OrderByDescending(c => c.UpdatedOn);
                break;
            default:
                query = query.OrderBy(c => c.Name);
                break;
        }

        // Pagination
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        var total = query.Count();
        var skip = (page - 1) * pageSize;
        
        // Retrieve courses with related entities
        var courses = query.Skip(skip).Take(pageSize).ToList();
        
        // Calculate totals after retrieving data to avoid translation issues
        var data = new List<object>();
        
        foreach (var course in courses)
        {
            // Get total records separately to avoid translation errors
            var totalRecords = _context.Record
                .Include(r => r.Student)
                .Count(r => r.Student != null && r.Student.CourseId == course.Id);
                
            data.Add(new {
                course.Id,
                course.Name,
                course.CreatedOn,
                course.UpdatedOn,
                TotalBooks = course.Books?.Count ?? 0,
                TotalStudents = course.Students?.Count ?? 0,
                TotalRecords = totalRecords
            });
        }

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
    public IActionResult GetById(int id)
    {
        var course = _context.Courses
            .Include(c => c.Books)
            .Include(c => c.Students)
            .FirstOrDefault(c => c.Id == id);
            
        if (course == null) return NotFound();
        
        // Get total records separately to avoid translation errors
        var totalRecords = _context.Record
            .Include(r => r.Student)
            .Count(r => r.Student != null && r.Student.CourseId == course.Id);
            
        var courseResponse = new
        {
            course.Id,
            course.Name,
            course.CreatedOn,
            course.UpdatedOn,
            TotalBooks = course.Books?.Count ?? 0,
            TotalStudents = course.Students?.Count ?? 0,
            TotalRecords = totalRecords
        };
        
        var json = JsonSerializer.Serialize(courseResponse, _jsonOptions);
        return Content(json, "application/json");
    }

    [HttpPost]
    public IActionResult Create([FromBody] CourseDto courseDto)
    {
        var course = new Course
        {
            Name = courseDto.Name,
            CreatedOn = DateTime.UtcNow,
            UpdatedOn = DateTime.UtcNow
        };

        _context.Courses.Add(course);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] CourseDto updatedDto)
    {
        var course = _context.Courses.Find(id);
        if (course is null) return NotFound();

        course.Name = updatedDto.Name;
        course.UpdatedOn = DateTime.UtcNow;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var course = _context.Courses.Find(id);
        if (course is null) return NotFound();
        
        _context.Courses.Remove(course);
        _context.SaveChanges();
        return NoContent();
    }
}