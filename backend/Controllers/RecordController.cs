using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using LMS.Data;
using LMS.Models;
using LMS.Serializers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LMS.Controllers
{
    [Authorize]
    // [ApiController]
    [Route("[controller]")]
    public class RecordController : Controller // Changed to Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly int _defaultExpiryDays;

        public RecordController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            
            // Get default expiry days from environment
            if (!int.TryParse(configuration["book_expires_in_days"], out _defaultExpiryDays))
            {
                _defaultExpiryDays = 14; // Default to 14 days if not specified
            }
        }

        [HttpGet]
        public IActionResult Index(
            [FromQuery] int? bookId,
            [FromQuery] int? studentId,
            [FromQuery] DateTime? issueDate,
            [FromQuery] DateTime? returnDate,
            [FromQuery] DateTime? dueDate,
            [FromQuery] int? fine,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortBy = "created_on",
            [FromQuery] string? orderBy = "desc")
        {
            var query = _context.Record
                .Include(r => r.Book)
                .Include(r => r.Student)
                .AsQueryable();

            // Apply filters
            if (bookId.HasValue)
                query = query.Where(r => r.BookId == bookId.Value);
            if (studentId.HasValue)
                query = query.Where(r => r.StudentId == studentId.Value);
            if (issueDate.HasValue)
                query = query.Where(r => r.IssueDate.Date == issueDate.Value.Date);
            if (returnDate.HasValue)
                query = query.Where(r => r.ReturnDate.HasValue && r.ReturnDate.Value.Date == returnDate.Value.Date);
            if (dueDate.HasValue)
                query = query.Where(r => r.DueDate.Date == dueDate.Value.Date);
            if (fine.HasValue)
                query = query.Where(r => r.Fine == fine.Value);

            // Apply sorting
            switch (sortBy?.ToLower())
            {
                case "issue_date":
                    query = orderBy?.ToLower() == "asc" 
                        ? query.OrderBy(r => r.IssueDate) 
                        : query.OrderByDescending(r => r.IssueDate);
                    break;
                case "return_date":
                    query = orderBy?.ToLower() == "asc" 
                        ? query.OrderBy(r => r.ReturnDate) 
                        : query.OrderByDescending(r => r.ReturnDate);
                    break;
                case "due_date":
                    query = orderBy?.ToLower() == "asc" 
                        ? query.OrderBy(r => r.DueDate) 
                        : query.OrderByDescending(r => r.DueDate);
                    break;
                case "fine":
                    query = orderBy?.ToLower() == "asc" 
                        ? query.OrderBy(r => r.Fine) 
                        : query.OrderByDescending(r => r.Fine);
                    break;
                case "created_on":
                    query = orderBy?.ToLower() == "asc" 
                        ? query.OrderBy(r => r.CreatedOn) 
                        : query.OrderByDescending(r => r.CreatedOn);
                    break;
                case "updated_on":
                    query = orderBy?.ToLower() == "asc" 
                        ? query.OrderBy(r => r.UpdatedOn) 
                        : query.OrderByDescending(r => r.UpdatedOn);
                    break;
                default:
                    query = query.OrderByDescending(r => r.CreatedOn);
                    break;
            }

            // Apply pagination
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            
            // Return view with data
            var records = query.ToList();
            
            return View(records);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var record = await _context.Record
                .Include(r => r.Book)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (record == null) return NotFound();
            
            return View(record);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewBag.Books = _context.Books.Where(b => b.Stock > 0).ToList();
            ViewBag.Students = _context.Students.ToList();
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Record record)
        {
            ModelState.Remove("Book");
            ModelState.Remove("Student");

            if (ModelState.IsValid)
            {
                var book = await _context.Books.FindAsync(record.BookId);
                var student = await _context.Students.FindAsync(record.StudentId);

                if (book == null || student == null)
                {
                    ModelState.AddModelError("", "Invalid Book or Student.");
                }
                else if (book.Stock <= 0)
                {
                    ModelState.AddModelError("", "Book is out of stock.");
                }
                else
                {
                    record.IssueDate = DateTime.UtcNow;
                    record.DueDate = DateTime.UtcNow.AddDays(14);
                    record.ReturnDate = null;
                    record.Fine = 0;

                    book.Stock--;
                    _context.Update(book);
                    _context.Add(record);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            
            ViewBag.Books = _context.Books.Where(b => b.Stock > 0).ToList();
            ViewBag.Students = _context.Students.ToList();
            return View(record);
        }
        
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var record = await _context.Record.FindAsync(id);
            if (record == null) return NotFound();
            
            ViewBag.Books = _context.Books.ToList();
            ViewBag.Students = _context.Students.ToList();
            return View(record);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Record record)
        {
            if (id != record.Id) return NotFound();

            ModelState.Remove("Book");
            ModelState.Remove("Student");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Record.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Books = _context.Books.ToList();
            ViewBag.Students = _context.Students.ToList();
            return View(record);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _context.Record
                .Include(r => r.Book)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (record == null) return NotFound();

            return View(record);
        }

        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var record = await _context.Record.FindAsync(id);
            if (record != null)
            {
                if (record.ReturnDate == null)
                {
                    var book = await _context.Books.FindAsync(record.BookId);
                    if (book != null)
                    {
                        book.Stock++;
                        _context.Update(book);
                    }
                }
                
                _context.Record.Remove(record);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Return/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {
            var record = await _context.Record.Include(r => r.Book).FirstOrDefaultAsync(r => r.Id == id);
            if (record == null) return NotFound();
            
            if (record.ReturnDate == null)
            {
                record.ReturnDate = DateTime.UtcNow;
                
                if (record.ReturnDate > record.DueDate)
                {
                    var overdueDays = (record.ReturnDate.Value - record.DueDate).Days;
                    record.Fine = overdueDays * 10;
                }
                
                if (record.Book != null)
                {
                    record.Book.Stock++;
                    _context.Update(record.Book);
                }
                
                _context.Update(record);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
