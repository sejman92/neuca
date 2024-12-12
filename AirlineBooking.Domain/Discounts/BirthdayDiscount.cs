using AirlineBooking.Domain.Tickets;

namespace AirlineBooking.Domain.Discounts;

public class BirthdayDiscount : IDiscount
{
    public string Description { get; } = "Customer Birthday discount";
    public decimal Value { get; } = 5m;
    public bool IsApplicable(Ticket ticket)
    {
        return ticket.Departure.Date == ticket.Customer.Birthday.Date;
    }
}