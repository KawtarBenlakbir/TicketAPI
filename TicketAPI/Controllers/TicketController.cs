using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase
{
    private readonly TicketContext _context;

    public TicketController(TicketContext context)
    {
        _context = context;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets(int page = 1, int pageSize = 10)
    {
        return await _context.Tickets
                             .Skip((page - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync();
    }

    // Get by id

    [HttpGet("{id}")]
    public async Task<ActionResult<Ticket>> GetTicket(int id)
    {
       var ticket = await _context.Tickets.FindAsync(id);

       if (ticket == null)
       {
         return NotFound();
       }

       return ticket;
    }
    // ADD
    [HttpPost("create")]
    public async Task<ActionResult<Ticket>> CreateTicket(Ticket ticket)
    {
       _context.Tickets.Add(ticket);
       await _context.SaveChangesAsync();

       return CreatedAtAction(nameof(GetTicket), new { id = ticket.TicketId }, ticket);
    }
   //Update
   [HttpPut("update/{id}")]
   public async Task<IActionResult> UpdateTicket(int id, Ticket ticket)
   {
      if (id != ticket.TicketId)
      {
         return BadRequest();
      }

      _context.Entry(ticket).State = EntityState.Modified;

      try
      {
         await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
          if (!TicketExists(id))
          {
             return NotFound();
          }
          throw;
      }

      return NoContent();
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
      var ticket = await _context.Tickets.FindAsync(id);
      if (ticket == null)
      {
         return NotFound();
       }

      _context.Tickets.Remove(ticket);
      await _context.SaveChangesAsync();

      return NoContent();
    }
   private bool TicketExists(int id)
   {
      return _context.Tickets.Any(t => t.TicketId == id);
   }

}
