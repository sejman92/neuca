using AirlineBooking.Domain.Flights;
using AirlineBooking.Domain.Tickets;

namespace AirlineBooking.Domain.Discounts;

public interface IDiscount
{
    string Description { get; }
    decimal Value { get; }
    bool IsApplicable(Ticket ticket);
}


