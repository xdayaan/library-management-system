using Microsoft.AspNetCore.Mvc;
using LMS.Data;
using LMS.Models;
using LMS.Utils;
using LMS.Serializers;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("[controller]")]



public class LibrarianController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LibrarianController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authorize]
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

    // [Authorize]
    [HttpPost]
    public IActionResult Add(Librarian librarian)
    {
        librarian.Password = PasswordHandler.Encrypt(librarian.Password);
        _context.Librarian.Add(librarian);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = librarian.Id }, librarian);
    }
    
    // Allow anonymous access to login endpoint
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginDto request) 
    {
        var librarian = _context.Librarian.FirstOrDefault(l => l.Username == request.Username);
        if (librarian == null) 
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        try 
        {
            string decryptedPassword = PasswordHandler.Decrypt(librarian.Password);

            if (request.Password != decryptedPassword)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
        }
        catch (Exception ex) when (ex is FormatException || ex is CryptographicException)
        {
            return BadRequest(new { message = "Authentication error. Please contact support." });
        }

        var response = new
        {
            Id = librarian.Id,
            Username = librarian.Username,
            Name = librarian.Name,
            Token = JwtHelper.GenerateToken(librarian)
        };

        return Ok(response);
    }

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Update(int id, Librarian updatedLibrarian)
    {
        var librarian = _context.Librarian.Find(id);
        if(librarian == null) return NotFound();
        librarian.Username = updatedLibrarian.Username;
        librarian.Name = updatedLibrarian.Name;
        librarian.Password = PasswordHandler.Encrypt(updatedLibrarian.Password);
        _context.SaveChanges();
        return NoContent();
    }
    
    [Authorize]
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