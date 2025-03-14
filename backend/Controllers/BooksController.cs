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
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
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
        public IActionResult GetAll(
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
            var total = query.Count();
            var skip = (page - 1) * pageSize;
            
            // Include records to check if book is currently issued
            var books = query.Skip(skip).Take(pageSize)
                .Include(b => b.Records)
                .ToList();
                
            // Select a simplified projection without circular references
            var data = books.Select(b => {
                var currentRecord = b.Records?.FirstOrDefault(r => 
                    r.ReturnDate > DateTime.UtcNow || 
                    r.DueDate > DateTime.UtcNow);
                    
                return new {
                    b.Id,
                    b.SerialNumber,
                    b.Name,
                    b.Author,
                    b.Publisher,
                    b.Subject,
                    b.CourseId,
                    b.PublishedDate,
                    b.Stock,
                    Status = (int)b.Status,
                    b.ISBN,
                    b.Edition,
                    b.Pages,
                    b.RackNumber,
                    b.CreatedOn,
                    b.UpdatedOn,
                    StudentId = currentRecord?.StudentId,
                    RecordId = currentRecord?.Id,
                    Course = b.Course != null ? new {
                        b.Course.Id,
                        b.Course.Name
                    } : null
                };
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
        public ActionResult GetById(int id)
        {
            var book = _context.Books
                .Include(b => b.Course)
                .Include(b => b.Records)
                    .ThenInclude(r => r.Student)
                .FirstOrDefault(b => b.Id == id);
                
            if (book == null) return NotFound();
            
            // Get current student if the book is on loan
            var currentRecord = book.Records?.FirstOrDefault(r => 
                r.ReturnDate > DateTime.UtcNow || 
                r.DueDate > DateTime.UtcNow);
                
            var bookResponse = new
            {
                book.Id,
                book.SerialNumber,
                book.Name,
                book.Author,
                book.Publisher,
                book.Subject,
                book.CourseId,
                book.PublishedDate,
                book.Stock,
                Status = (int)book.Status,
                book.ISBN,
                book.Edition,
                book.Pages,
                book.RackNumber,
                book.CreatedOn,
                book.UpdatedOn,
                StudentId = currentRecord?.StudentId,
                RecordId = currentRecord?.Id,
                Course = book.Course == null ? null : new {
                    book.Course.Id,
                    book.Course.Name
                }
            };
            
            var json = JsonSerializer.Serialize(bookResponse, _jsonOptions);
            return Content(json, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookDto bookDto)
        {
            var book = ToModel(bookDto);
            
            // Ensure timestamps are in UTC
            book.PublishedDate = DateTime.SpecifyKind(book.PublishedDate, DateTimeKind.Utc);
            book.CreatedOn = DateTime.UtcNow;
            book.UpdatedOn = DateTime.UtcNow;

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, ToDto(book));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookDto bookDto)
        {
            var book = _context.Books.Find(id);
            if (book is null) return NotFound();

            // Update model from DTO
            book.SerialNumber = bookDto.SerialNumber;
            book.Name = bookDto.Name;
            book.Author = bookDto.Author;
            book.Publisher = bookDto.Publisher;
            book.Subject = bookDto.Subject;
            book.CourseId = bookDto.CourseId;
            book.PublishedDate = DateTime.SpecifyKind(bookDto.PublishedDate, DateTimeKind.Utc);
            book.Stock = bookDto.Stock;
            book.Status = (Status)bookDto.Status;
            book.ISBN = bookDto.ISBN;
            book.Edition = bookDto.Edition;
            book.Pages = bookDto.Pages;
            book.RackNumber = bookDto.RackNumber;
            book.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public IActionResult ChangeStatus(int id, Status status)
        {
            var book = _context.Books.FirstOrDefault(c => c.Id == id);
            if (book == null) return NotFound();

            book.Status = status;
            book.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return NoContent();
        }

        // Helper methods for DTO conversion
        private BookDto ToDto(Book book)
        {
            return new BookDto
            {
                SerialNumber = book.SerialNumber,
                Name = book.Name,
                Author = book.Author,
                Publisher = book.Publisher,
                Subject = book.Subject,
                CourseId = book.CourseId,
                PublishedDate = book.PublishedDate,
                Stock = book.Stock,
                Status = (int)book.Status,
                ISBN = book.ISBN,
                Edition = book.Edition,
                Pages = book.Pages,
                RackNumber = book.RackNumber,
                Course = book.Course
            };
        }

        private Book ToModel(BookDto dto)
        {
            return new Book
            {
                SerialNumber = dto.SerialNumber,
                Name = dto.Name,
                Author = dto.Author,
                Publisher = dto.Publisher,
                Subject = dto.Subject,
                CourseId = dto.CourseId,
                PublishedDate = dto.PublishedDate,
                Stock = dto.Stock,
                Status = (Status)dto.Status,
                ISBN = dto.ISBN,
                Edition = dto.Edition,
                Pages = dto.Pages,
                RackNumber = dto.RackNumber
            };
        }
    }
}