using AirlineBooking.Domain.Tickets;

namespace AirlineBooking.Persistence.Tickets;

public class TicketsRepository : ITicketsRepository
{
    private List<Ticket> _tickets = [];
    
    public void AddTicket(Ticket t)
    {
        _tickets.Add(t);
        Console.WriteLine($"Ticket: {t.Id}. Customer: {t.Customer.Name}. Flight: {t.Flight.FlightId.FullId}. ADDED");
    }
}