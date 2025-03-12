using Microsoft.AspNetCore.Mvc;
using LMS.Data;
using LMS.Models;

[ApiController]
[Route("[controller]")]
public class LibrarianController : ControllerBase{
    private readonly ApplicationDbContext _context;

    public LibrarianController(ApplicationDbContext context)
    {
        _context = context;
    }
    private readonly object? hashedPassword;

    [HttpGet("{id}")]
    public ActionResult<Librarian> GetById(int id)
    {
       var librarian = _context.Librarian.Find(id);
    if (librarian == null)
    {
        return NotFound();
    }
    return librarian;
    }

    [HttpPost("add")]
    public IActionResult Add(Librarian librarian)
    {
        _context.Librarian.Add(librarian);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = librarian.Id }, librarian);
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, Librarian updatedLibrarian)
    {
        var librarian = _context.Librarian.Find(id);
        if(librarian == null) return NotFound();
        librarian.Username = updatedLibrarian.Username;
        librarian.Name = updatedLibrarian.Name;
        librarian.Password = updatedLibrarian.Password;
        _context.SaveChanges();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var librarian = _context.Librarian.Find(id);
        if(librarian == null) return NotFound();
        _context.Librarian.Remove(librarian);
        _context.SaveChanges();
        return NoContent();
    }
}