// filepath: d:\code\xdas\LMS\Backend\Controllers\BooksController.cs
using Microsoft.AspNetCore.Mvc;
using LMS.Data;
using LMS.Models;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public BooksController(ApplicationDbContext context) => _context = context;


    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Books);


    [HttpGet("{id}")]
    public ActionResult<Book> GetById(int id) =>
        _context.Books.Find(id) is Book c ? c : NotFound();

    [HttpPost]
    public async Task<IActionResult> Add(Book book)
    {
        book.PublishedDate = DateTime.SpecifyKind(book.PublishedDate, DateTimeKind.Utc);
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }


    [HttpPut("{id}")]
    public IActionResult Update(int id, Book updated)
    {
        var book = _context.Books.Find(id);
        if (book is null) return NotFound();
        book.Name = updated.Name;
        _context.SaveChanges();
        return NoContent();
    }

    // [HttpDelete("{id}")]
    // public IActionResult Delete(int id)
    // {
    //     var book = _context.Books.Find(id);
    //     if (book is null) return NotFound();
    //     _context.Books.Remove(book);
    //     _context.SaveChanges();
    //     return NoContent();
    // }

    
    [HttpPatch("{id}/status")]
    public IActionResult ChangeStatus(int id, Status status)
    {
        var book = _context.Books.FirstOrDefault(c => c.Id == id);
        if (book == null) return NotFound();
        book.Status = status;
        _context.SaveChanges();
        return NoContent();
    }
}