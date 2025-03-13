using Microsoft.AspNetCore.Mvc;
using LMS.Data;
using LMS.Models;

[ApiController]
[Route("[controller]")]
public class RecordController : ControllerBase{
    private readonly ApplicationDbContext _context;    
    public RecordController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var Record = _context.Record.Find(id);
        if (Record == null) return NotFound("Record not found");
        return Ok(Record);
    }
    
    [HttpPost("add")]
    public IActionResult Add(Record record) 
    {
        _context.Record.Add(record); 
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = record.Id }, record);
    }
    
    [HttpPost("renew")]
    public IActionResult Renew(int id, DateTime newReturnDate)
    {
        var record = _context.Record.Find(id);
        if (record == null)
        {
            return NotFound("Record not found.");
        }
        record.Return_Date = newReturnDate;
        _context.SaveChanges();
        return Ok("Record renewed successfully!");
    }
}