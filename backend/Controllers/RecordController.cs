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
    [ApiController]
    [Route("[controller]")]
    public class RecordController : ControllerBase
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
        public IActionResult GetAll(
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
            var total = query.Count();
            var skip = (page - 1) * pageSize;
            
            // Map to RecordSummaryDto to avoid circular references
            var data = query.Skip(skip).Take(pageSize)
                .Select(r => new RecordSummaryDto
                {
                    Id = r.Id,
                    BookId = r.BookId,
                    StudentId = r.StudentId,
                    IssueDate = r.IssueDate,
                    ReturnDate = r.ReturnDate,
                    DueDate = r.DueDate,
                    Fine = r.Fine,
                    CreatedOn = r.CreatedOn,
                    UpdatedOn = r.UpdatedOn,
                    BookName = r.Book.Name,
                    BookSerialNumber = r.Book.SerialNumber,
                    StudentName = r.Student.Name,
                    StudentRollNumber = r.Student.RollNumber
                }).ToList();

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
        public async Task<IActionResult> Get(int id)
        {
            var record = await _context.Record
                .Include(r => r.Book)
                    .ThenInclude(b => b.Course)
                .Include(r => r.Student)
                    .ThenInclude(s => s.Course)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (record == null)
                return NotFound();
            
            // Create a detailed response with complete book and student details
            var detailedRecord = new
            {
                record.Id,
                record.BookId,
                record.StudentId,
                record.IssueDate,
                record.ReturnDate,
                record.DueDate,
                record.Fine,
                record.CreatedOn,
                record.UpdatedOn,
                Book = record.Book == null ? null : new
                {
                    record.Book.Id,
                    record.Book.SerialNumber,
                    record.Book.Name,
                    record.Book.Author,
                    record.Book.Publisher,
                    record.Book.Subject,
                    record.Book.CourseId,
                    record.Book.PublishedDate,
                    record.Book.Stock,
                    Status = (int)record.Book.Status,
                    record.Book.ISBN,
                    record.Book.Edition,
                    record.Book.Pages,
                    record.Book.RackNumber,
                    record.Book.CreatedOn,
                    record.Book.UpdatedOn,
                    Course = record.Book.Course == null ? null : new
                    {
                        record.Book.Course.Id,
                        record.Book.Course.Name
                    }
                },
                Student = record.Student == null ? null : new
                {
                    record.Student.Id,
                    record.Student.RollNumber,
                    record.Student.Name,
                    record.Student.Email,
                    record.Student.Phone,
                    record.Student.Address,
                    record.Student.CourseId,
                    record.Student.Photo,
                    record.Student.Year,
                    record.Student.Gender,
                    record.Student.FatherName,
                    record.Student.CreatedOn,
                    record.Student.UpdatedOn,
                    Course = record.Student.Course == null ? null : new
                    {
                        record.Student.Course.Id,
                        record.Student.Course.Name
                    }
                }
            };
            
            var json = JsonSerializer.Serialize(detailedRecord, _jsonOptions);
            return Content(json, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JsonElement requestBody)
        {
            try
            {
                // Extract values from JSON manually to handle empty strings for dates
                if (!requestBody.TryGetProperty("bookId", out var bookIdElement) || !bookIdElement.TryGetInt32(out var bookId))
                {
                    return BadRequest(new { message = "Invalid or missing bookId" });
                }

                if (!requestBody.TryGetProperty("studentId", out var studentIdElement) || !studentIdElement.TryGetInt32(out var studentId))
                {
                    return BadRequest(new { message = "Invalid or missing studentId" });
                }

                // Parse issueDate with fallback
                DateTime issueDate;
                if (requestBody.TryGetProperty("issueDate", out var issueDateElement))
                {
                    if (issueDateElement.ValueKind == JsonValueKind.String)
                    {
                        var dateString = issueDateElement.GetString();
                        if (string.IsNullOrEmpty(dateString))
                        {
                            issueDate = DateTime.UtcNow;
                        }
                        else if (!DateTime.TryParse(dateString, out issueDate))
                        {
                            issueDate = DateTime.UtcNow;
                        }
                    }
                    else if (issueDateElement.TryGetDateTime(out var parsedDate))
                    {
                        issueDate = parsedDate;
                    }
                    else
                    {
                        issueDate = DateTime.UtcNow;
                    }
                }
                else
                {
                    issueDate = DateTime.UtcNow;
                }

                // Parse returnDate with proper null handling
                DateTime? returnDate = null;
                if (requestBody.TryGetProperty("returnDate", out var returnDateElement))
                {
                    if (returnDateElement.ValueKind == JsonValueKind.String)
                    {
                        var dateString = returnDateElement.GetString();
                        if (!string.IsNullOrEmpty(dateString))
                        {
                            if (DateTime.TryParse(dateString, out var parsedReturnDate))
                            {
                                returnDate = parsedReturnDate;
                            }
                        }
                    }
                    else if (returnDateElement.ValueKind == JsonValueKind.Null)
                    {
                        returnDate = null;
                    }
                    else if (returnDateElement.TryGetDateTime(out var parsedReturnDate))
                    {
                        returnDate = parsedReturnDate;
                    }
                }

                // Parse dueDate with fallback to default expiry days
                DateTime dueDate;
                if (requestBody.TryGetProperty("dueDate", out var dueDateElement))
                {
                    if (dueDateElement.ValueKind == JsonValueKind.String)
                    {
                        var dateString = dueDateElement.GetString();
                        if (string.IsNullOrEmpty(dateString))
                        {
                            dueDate = DateTime.UtcNow.AddDays(_defaultExpiryDays);
                        }
                        else if (!DateTime.TryParse(dateString, out dueDate))
                        {
                            dueDate = DateTime.UtcNow.AddDays(_defaultExpiryDays);
                        }
                    }
                    else if (dueDateElement.TryGetDateTime(out var parsedDate))
                    {
                        dueDate = parsedDate;
                    }
                    else
                    {
                        dueDate = DateTime.UtcNow.AddDays(_defaultExpiryDays);
                    }
                }
                else
                {
                    dueDate = DateTime.UtcNow.AddDays(_defaultExpiryDays);
                }

                // Parse fine with fallback
                int fine = 0;
                if (requestBody.TryGetProperty("fine", out var fineElement))
                {
                    fineElement.TryGetInt32(out fine);
                }

                // Verify that the referenced Book exists
                var book = await _context.Books.FindAsync(bookId);
                if (book == null)
                    return BadRequest(new { message = "The specified book does not exist" });

                // Verify that the referenced Student exists
                var student = await _context.Students.FindAsync(studentId);
                if (student == null)
                    return BadRequest(new { message = "The specified student does not exist" });

                // Check if book is available (has sufficient stock)
                if (book.Stock <= 0 || book.Status == Status.Inactive)
                    return BadRequest(new { message = "The book is not available for issuing" });

                var record = new Record
                {
                    BookId = bookId,
                    StudentId = studentId,
                    IssueDate = issueDate.ToUniversalTime(),
                    ReturnDate = returnDate?.ToUniversalTime(), // Now properly handling null
                    DueDate = dueDate.ToUniversalTime(),
                    Fine = fine,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow
                };

                // Decrement book stock
                book.Stock--;
                if (book.Stock == 0)
                    book.Status = Status.Inactive;

                _context.Record.Add(record);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = record.Id }, record);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Failed to process request: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JsonElement requestBody)
        {
            try
            {
                var record = await _context.Record.FindAsync(id);
                if (record == null)
                    return NotFound();

                // Extract values from JSON manually to handle empty strings for dates
                if (!requestBody.TryGetProperty("bookId", out var bookIdElement) || !bookIdElement.TryGetInt32(out var bookId))
                {
                    return BadRequest(new { message = "Invalid or missing bookId" });
                }

                if (!requestBody.TryGetProperty("studentId", out var studentIdElement) || !studentIdElement.TryGetInt32(out var studentId))
                {
                    return BadRequest(new { message = "Invalid or missing studentId" });
                }

                // Handle book changes if needed
                if (record.BookId != bookId)
                {
                    // Return stock to original book
                    var originalBook = await _context.Books.FindAsync(record.BookId);
                    if (originalBook != null)
                    {
                        originalBook.Stock++;
                        if (originalBook.Status == Status.Inactive && originalBook.Stock > 0)
                            originalBook.Status = Status.Active;
                    }

                    // Reduce stock of new book
                    var newBook = await _context.Books.FindAsync(bookId);
                    if (newBook == null)
                        return BadRequest(new { message = "The specified book does not exist" });
                    
                    if (newBook.Stock <= 0)
                        return BadRequest(new { message = "The new book is not available for issuing" });
                    
                    newBook.Stock--;
                    if (newBook.Stock == 0)
                        newBook.Status = Status.Inactive;
                }

                // Verify that the referenced Student exists
                if (record.StudentId != studentId)
                {
                    var student = await _context.Students.FindAsync(studentId);
                    if (student == null)
                        return BadRequest(new { message = "The specified student does not exist" });
                }

                // Parse issueDate with fallback
                DateTime issueDate;
                if (requestBody.TryGetProperty("issueDate", out var issueDateElement) && 
                    issueDateElement.ValueKind != JsonValueKind.Null &&
                    issueDateElement.ValueKind != JsonValueKind.String && 
                    issueDateElement.GetString() != "")
                {
                    if (!issueDateElement.TryGetDateTime(out issueDate))
                    {
                        issueDate = record.IssueDate; // Keep existing value if parsing fails
                    }
                }
                else
                {
                    issueDate = record.IssueDate;
                }

                // Parse returnDate - now properly handling null
                DateTime? returnDate = null;
                if (requestBody.TryGetProperty("returnDate", out var returnDateElement))
                {
                    if (returnDateElement.ValueKind == JsonValueKind.String)
                    {
                        var dateString = returnDateElement.GetString();
                        if (!string.IsNullOrEmpty(dateString))
                        {
                            if (DateTime.TryParse(dateString, out var parsedReturnDate))
                            {
                                returnDate = parsedReturnDate;
                            }
                        }
                    }
                    else if (returnDateElement.ValueKind == JsonValueKind.Null)
                    {
                        returnDate = null;
                    }
                    else if (returnDateElement.TryGetDateTime(out var parsedReturnDate))
                    {
                        returnDate = parsedReturnDate;
                    }
                }
                else if (record.ReturnDate.HasValue) // Check if existing ReturnDate has a value
                {
                    returnDate = record.ReturnDate;
                }

                // Parse dueDate with fallback to default expiry days
                DateTime dueDate;
                if (requestBody.TryGetProperty("dueDate", out var dueDateElement) &&
                    dueDateElement.ValueKind != JsonValueKind.Null &&
                    dueDateElement.ValueKind != JsonValueKind.String &&
                    dueDateElement.GetString() != "")
                {
                    if (!dueDateElement.TryGetDateTime(out dueDate))
                    {
                        dueDate = record.DueDate; // Keep existing value if parsing fails
                    }
                }
                else
                {
                    dueDate = record.DueDate;
                }

                // Parse fine with fallback
                int fine = record.Fine;
                if (requestBody.TryGetProperty("fine", out var fineElement))
                {
                    fineElement.TryGetInt32(out fine);
                }

                record.BookId = bookId;
                record.StudentId = studentId;
                record.IssueDate = issueDate.ToUniversalTime();
                record.ReturnDate = returnDate?.ToUniversalTime(); // Now properly handling null
                record.DueDate = dueDate.ToUniversalTime();
                record.Fine = fine;
                record.UpdatedOn = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Failed to process request: {ex.Message}" });
            }
        }

        // New endpoint for reissuing a book
        [HttpPost("{id}/reissue")]
        public async Task<IActionResult> Reissue(int id, [FromQuery] int? daysToExtend = null)
        {
            var record = await _context.Record.FindAsync(id);
            if (record == null)
                return NotFound();

            // Check if the book has been returned already
            if (record.ReturnDate.HasValue)
            {
                return BadRequest(new { message = "Cannot reissue a book that has already been returned" });
            }

            // Use the provided days or default to the environment setting
            int extendDays = daysToExtend ?? _defaultExpiryDays;
            
            // Calculate new due date - either from the current date or from the old due date
            DateTime currentDueDate = record.DueDate;
            record.DueDate = DateTime.UtcNow > currentDueDate 
                ? DateTime.UtcNow.AddDays(extendDays)
                : currentDueDate.AddDays(extendDays);
            
            record.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            
            var result = new
            {
                RecordId = record.Id,
                PreviousDueDate = currentDueDate,
                NewDueDate = record.DueDate,
                DaysExtended = extendDays
            };
            
            var json = JsonSerializer.Serialize(result, _jsonOptions);
            return Content(json, "application/json");
        }

        // New endpoint for returning a book
        [HttpPost("{id}/return")]
        public async Task<IActionResult> ReturnBook(int id, [FromQuery] int? fine = null)
        {
            var record = await _context.Record
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.Id == id);
                
            if (record == null)
                return NotFound();

            // Check if the book is already returned
            if (record.ReturnDate.HasValue)
                return BadRequest(new { message = "This book has already been returned" });

            // Mark the book as returned with today's date
            record.ReturnDate = DateTime.UtcNow;
            
            // Calculate fine if not provided and due date has passed
            if (!fine.HasValue && DateTime.UtcNow > record.DueDate)
            {
                // Calculate days overdue
                var daysOverdue = (int)(DateTime.UtcNow - record.DueDate).TotalDays;
                if (daysOverdue < 0) daysOverdue = 0;
                
                // Assuming fine is $1 per day overdue
                record.Fine = daysOverdue;
            }
            else if (fine.HasValue)
            {
                // Use provided fine value
                record.Fine = fine.Value;
            }
            
            record.UpdatedOn = DateTime.UtcNow;
            
            // Return the book to stock
            if (record.Book != null)
            {
                record.Book.Stock++;
                if (record.Book.Status == Status.Inactive && record.Book.Stock > 0)
                    record.Book.Status = Status.Active;
            }

            await _context.SaveChangesAsync();
            
            var result = new
            {
                RecordId = record.Id,
                BookId = record.BookId,
                StudentId = record.StudentId,
                ReturnDate = record.ReturnDate,
                DueDate = record.DueDate,
                Fine = record.Fine,
                DaysOverdue = record.ReturnDate.HasValue && record.DueDate < record.ReturnDate.Value ? 
                    (int)(record.ReturnDate.Value - record.DueDate).TotalDays : 0
            };
            
            var json = JsonSerializer.Serialize(result, _jsonOptions);
            return Content(json, "application/json");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _context.Record.FindAsync(id);
            if (record == null)
                return NotFound();

            // Return the book to stock
            var book = await _context.Books.FindAsync(record.BookId);
            if (book != null)
            {
                book.Stock++;
                if (book.Status == Status.Inactive && book.Stock > 0)
                    book.Status = Status.Active;
            }

            _context.Record.Remove(record);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
