using Microsoft.AspNetCore.Mvc;
using LMS.Data;
using LMS.Models;
using LMS.Utils;
using LMS.Serializers;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LMS.Controllers
{
    public class LibrarianController : Controller
    {
    private readonly ApplicationDbContext _context;

    public LibrarianController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var librarians = _context.Librarian.ToList();
        return View(librarians);
    }

    // [Authorize]
    [HttpGet("{id}")]
    public ActionResult<Librarian> Details(int id)
    {
        var librarian = _context.Librarian.Find(id);
        if (librarian == null)
        {
            return NotFound();
        }
        return View(librarian);
    }

    // ... Index and Details methods remain unchanged ...

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Librarian librarian)
    {
        if (ModelState.IsValid)
        {
            librarian.Password = PasswordHandler.Encrypt(librarian.Password);
            _context.Librarian.Add(librarian);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(librarian);
    }
    
    [AllowAnonymous]
    [HttpGet("/login")]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("/login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto request) 
    {
        var librarian = _context.Librarian.FirstOrDefault(l => l.Username == request.Username);
        if (librarian == null) 
        {
            ViewBag.Error = "Invalid username or password";
            return View(request);
        }

        try 
        {
            string decryptedPassword = PasswordHandler.Decrypt(librarian.Password);

            if (request.Password != decryptedPassword)
            {
                ViewBag.Error = "Invalid username or password";
                return View(request);
            }
        }
        catch (Exception ex) when (ex is FormatException || ex is CryptographicException)
        {
            ViewBag.Error = "Authentication error. Please contact support.";
            return View(request);
        }

        // Authentication successful - create claims identity
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, librarian.Username),
            new Claim(ClaimTypes.NameIdentifier, librarian.Id.ToString()),
            new Claim("Name", librarian.Name)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddHours(8)
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("/logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Librarian");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var librarian = _context.Librarian.Find(id);
        if (librarian == null) return NotFound();
        return View(librarian);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Librarian updatedLibrarian)
    {
        if (id != updatedLibrarian.Id) return NotFound();

        if (ModelState.IsValid)
        {
            var librarian = _context.Librarian.Find(id);
            if(librarian == null) return NotFound();
            
            librarian.Username = updatedLibrarian.Username;
            librarian.Name = updatedLibrarian.Name;
            
            if (!string.IsNullOrEmpty(updatedLibrarian.Password) && updatedLibrarian.Password != librarian.Password)
            {
                 librarian.Password = PasswordHandler.Encrypt(updatedLibrarian.Password);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(updatedLibrarian);
    }
    
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var librarian = _context.Librarian.Find(id);
        if (librarian == null) return NotFound();
        return View(librarian);
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var librarian = _context.Librarian.Find(id);
        if(librarian == null) return NotFound();
        _context.Librarian.Remove(librarian);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
}