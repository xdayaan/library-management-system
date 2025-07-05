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
// [ApiController]
[Route("[controller]")]
public class StudentsController : Controller
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
    public IActionResult Index(
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
        
        // Return view with data
        var students = query.ToList();

        return View(students);
    }

    // ... Index method remains unchanged ...

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var student = await _context.Students
            .Include(s => s.Course)
            .Include(s => s.Records)
            .FirstOrDefaultAsync(m => m.Id == id);
            
        if (student == null) return NotFound();
        
        return View(student);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        ViewBag.Courses = _context.Courses.ToList();
        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student, IFormFile? photoFile)
    {
        ModelState.Remove("Course");
        ModelState.Remove("Records");
        ModelState.Remove("Photo"); // Photo is handled by photoFile

        if (ModelState.IsValid)
        {
            if (photoFile != null && photoFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);
                    
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photoFile.CopyToAsync(stream);
                }
                student.Photo = "/uploads/" + fileName;
            }
            else
            {
                student.Photo = "https://ui-avatars.com/api/?name=" + Uri.EscapeDataString(student.Name ?? "Student") + "&background=random";
            }
            
            // Encrypt password
            if (!string.IsNullOrEmpty(student.Password))
            {
                student.Password = PasswordHandler.Encrypt(student.Password);
            }
            
            student.CreatedOn = DateTime.UtcNow;
            student.UpdatedOn = DateTime.UtcNow;
            
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Courses = _context.Courses.ToList();
        return View(student);
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        
        // Decrypt password for display or leave it encrypted? 
        // Usually we don't display password. Let's leave it blank in view or keeps it as is.
        // student.Password = PasswordHandler.Decrypt(student.Password); 
        
        ViewBag.Courses = _context.Courses.ToList();
        return View(student);
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student, IFormFile? photoFile)
    {
        if (id != student.Id) return NotFound();

        ModelState.Remove("Course");
        ModelState.Remove("Records");
        ModelState.Remove("Photo");

        if (ModelState.IsValid)
        {
            try
            {
                var existingStudent = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
                if (existingStudent == null) return NotFound();

                // Handle file upload
                if (photoFile != null && photoFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);
                        
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photoFile.CopyToAsync(stream);
                    }
                    student.Photo = "/uploads/" + fileName;
                }
                else
                {
                    student.Photo = existingStudent.Photo; // Keep existing photo
                }

                // Handle password
                if (!string.IsNullOrEmpty(student.Password) && student.Password != existingStudent.Password)
                {
                    // If password changed (and is not the encrypted string), encrypt it
                    // Simple check: if it looks like it's already encrypted (long string), maybe don't re-encrypt?
                    // Better approach: assume if user types something, it's a new password.
                    // But if form sends back the hidden encrypted password, we need to know.
                    // For now, let's assume if it is different from existing, it's a new plain text password.
                    student.Password = PasswordHandler.Encrypt(student.Password);
                }
                else
                {
                    student.Password = existingStudent.Password;
                }

                student.UpdatedOn = DateTime.UtcNow;
                student.CreatedOn = existingStudent.CreatedOn; // Preserve creation date
                
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Students.Any(e => e.Id == id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Courses = _context.Courses.ToList();
        return View(student);
    }

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students
            .Include(s => s.Course)
            .FirstOrDefaultAsync(m => m.Id == id);
            
        if (student == null) return NotFound();

        return View(student);
    }

    [HttpPost("Delete/{id}"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
