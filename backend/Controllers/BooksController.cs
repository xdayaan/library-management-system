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
    public class BooksController : Controller // Change Base Class to Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly JsonSerializerOptions _jsonOptions;
        
        public BooksController(ApplicationDbContext context)
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
            [FromQuery] int? serialNumber,
            [FromQuery] string? name,
            [FromQuery] string? author,
            [FromQuery] string? publisher,
            [FromQuery] string? subject,
            [FromQuery] int? courseId,
            [FromQuery] DateTime? publishedDate,
            [FromQuery] int? stock,
            [FromQuery] Status? status,
            [FromQuery] string? isbn,
            [FromQuery] int? edition,
            [FromQuery] int? pages,
            [FromQuery] string? rackNumber,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortBy = "created_on",
            [FromQuery] string? orderBy = "desc")
        {
            var query = _context.Books
                .Include(b => b.Course)
                .AsQueryable();

            // Filters
            if (serialNumber.HasValue)
                query = query.Where(b => b.SerialNumber == serialNumber.Value);
            if (!string.IsNullOrEmpty(name))
                query = query.Where(b => b.Name.Contains(name));
            if (!string.IsNullOrEmpty(author))
                query = query.Where(b => b.Author.Contains(author));
            if (!string.IsNullOrEmpty(publisher))
                query = query.Where(b => b.Publisher.Contains(publisher));
            if (!string.IsNullOrEmpty(subject))
                query = query.Where(b => b.Subject.Contains(subject));
            if (courseId.HasValue)
                query = query.Where(b => b.CourseId == courseId.Value);
            if (publishedDate.HasValue)
                query = query.Where(b => b.PublishedDate.Date == publishedDate.Value.Date);
            if (stock.HasValue)
                query = query.Where(b => b.Stock == stock.Value);
            if (status.HasValue)
                query = query.Where(b => b.Status == status.Value);
            if (!string.IsNullOrEmpty(isbn))
                query = query.Where(b => b.ISBN.Contains(isbn));
            if (edition.HasValue)
                query = query.Where(b => b.Edition == edition.Value);
            if (pages.HasValue)
                query = query.Where(b => b.Pages == pages.Value);
            if (!string.IsNullOrEmpty(rackNumber))
                query = query.Where(b => b.RackNumber.Contains(rackNumber));

            // Sorting
            switch (sortBy?.ToLower())
            {
                case "serial_number":
                    query = orderBy?.ToLower() == "asc"
                        ? query.OrderBy(b => b.SerialNumber)
                        : query.OrderByDescending(b => b.SerialNumber);
                    break;
                case "name":
                    query = orderBy?.ToLower() == "asc"
                        ? query.OrderBy(b => b.Name)
                        : query.OrderByDescending(b => b.Name);
                    break;
                case "created_on":
                    query = orderBy?.ToLower() == "asc"
                        ? query.OrderBy(b => b.CreatedOn)
                        : query.OrderByDescending(b => b.CreatedOn);
                    break;
                case "updated_on":
                    query = orderBy?.ToLower() == "asc"
                        ? query.OrderBy(b => b.UpdatedOn)
                        : query.OrderByDescending(b => b.UpdatedOn);
                    break;
                default:
                    // Fallback to CreatedOn desc
                    query = query.OrderByDescending(b => b.CreatedOn);
                    break;
            }

            // Pagination
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            
            // For MVC view, we'll just return the list for now to match the view model
            // In a real app, you'd use a ViewModel with pagination info
            var books = query.Include(b => b.Records).ToList();
            
            return View(books);
        }

        // ... Index method remains unchanged ...

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .Include(b => b.Course)
                .Include(b => b.Records)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (book == null) return NotFound();
            
            return View(book);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            // Remove navigation properties from validation
            ModelState.Remove("Course");
            ModelState.Remove("Records");
            
            if (ModelState.IsValid)
            {
                book.CreatedOn = DateTime.UtcNow;
                book.UpdatedOn = DateTime.UtcNow;
                book.PublishedDate = DateTime.SpecifyKind(book.PublishedDate, DateTimeKind.Utc);
                
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Courses = _context.Courses.ToList();
            return View(book);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            
            ViewBag.Courses = _context.Courses.ToList();
            return View(book);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id) return NotFound();

            // Remove navigation properties from validation
            ModelState.Remove("Course");
            ModelState.Remove("Records");

            if (ModelState.IsValid)
            {
                try
                {
                    book.UpdatedOn = DateTime.UtcNow;
                    book.PublishedDate = DateTime.SpecifyKind(book.PublishedDate, DateTimeKind.Utc);
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Books.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Courses = _context.Courses.ToList();
            return View(book);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books
                .Include(b => b.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (book == null) return NotFound();

            return View(book);
        }

        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
