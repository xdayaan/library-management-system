using Microsoft.AspNetCore.Mvc;
using LMS.Data;
using LMS.Models;
using LMS.Serializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LMS.Controllers
{
    [Authorize]
    // [ApiController]
    [Route("[controller]")]
    public class CoursesController : Controller
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
        public IActionResult Index(
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
            
            // Retrieve courses with related entities
            var courses = query.ToList();
            
            return View(courses);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Books)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (course == null) return NotFound();
            
            return View(course);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            // Remove navigation properties from validation
            ModelState.Remove("Books");
            ModelState.Remove("Students");

            if (ModelState.IsValid)
            {
                course.CreatedOn = DateTime.UtcNow;
                course.UpdatedOn = DateTime.UtcNow;
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();
            
            return View(course);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.Id) return NotFound();

            // Remove navigation properties from validation
            ModelState.Remove("Books");
            ModelState.Remove("Students");

            if (ModelState.IsValid)
            {
                try
                {
                    course.UpdatedOn = DateTime.UtcNow;
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Courses.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
