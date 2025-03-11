// filepath: d:\code\xdas\LMS\Controllers\CoursesController.cs
using Microsoft.AspNetCore.Mvc;
using LMS.Data;
using LMS.Models;

[ApiController]
[Route("[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public CoursesController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public ActionResult<List<Course>> Get() => _context.Courses.ToList();

    [HttpGet("{id}")]
    public ActionResult<Course> GetById(int id) =>
        _context.Courses.Find(id) is Course c ? c : NotFound();

    [HttpPost]
    public IActionResult Create(Course course)
    {
        _context.Courses.Add(course);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Course updated)
    {
        var course = _context.Courses.Find(id);
        if (course is null) return NotFound();
        course.Name = updated.Name;
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