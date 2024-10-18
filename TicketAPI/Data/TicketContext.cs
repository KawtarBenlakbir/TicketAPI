using Microsoft.EntityFrameworkCore;

public class TicketContext : DbContext
{
    public TicketContext(DbContextOptions<TicketContext> options) : base(options) { }

    public DbSet<Ticket> Tickets { get; set; }
}
