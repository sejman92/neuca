using AirlineBooking.Domain;
using AirlineBooking.Domain.Discounts;
using AirlineBooking.Domain.Flights;
using AirlineBooking.Domain.Tickets;

namespace AirlineBooking;

public interface IBookingService
{
    void BookFlight(Flight flight, Customer customer, DateTime departureDay, Tenant tenant);
}

public class BookingService : IBookingService
{
    private readonly ITicketsRepository _ticketsRepository;
    private readonly List<IDiscount> ActiveDiscounts = [];

    private readonly Tenant _tenant;
    
    public BookingService(ITicketsRepository ticketsRepository)
    {
        _ticketsRepository = ticketsRepository;
        ActiveDiscounts.Add(new AfricaThursdayDiscount());
        ActiveDiscounts.Add(new BirthdayDiscount());
    }

    public void BookFlight(Flight flight, Customer customer, DateTime departureDay, Tenant tenant)
    {
        var ticket = Ticket.Create(flight, customer, departureDay, ActiveDiscounts, tenant);
        
        _ticketsRepository.AddTicket(ticket);
    }
}