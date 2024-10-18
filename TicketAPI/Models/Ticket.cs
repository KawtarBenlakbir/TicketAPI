public class Ticket
{
    public int TicketId { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } // "Open" or "Closed"
    public DateTime DateCreated { get; set; }
}
