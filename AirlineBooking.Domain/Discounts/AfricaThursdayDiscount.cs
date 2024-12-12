using AirlineBooking.Domain.Tickets;

namespace AirlineBooking.Domain.Discounts;

public class AfricaThursdayDiscount: IDiscount
{
    private const string DestinationDiscount = "Africa";
    private const DayOfWeek DiscountDay = DayOfWeek.Thursday;
    
    public string Description { get; } = "Africa Thursday discount";
    public decimal Value { get; } = 5m;
    public bool IsApplicable(Ticket ticket)
    {
        return ticket.Departure.DayOfWeek == DiscountDay
               && ticket.Flight.Route.To.Contains(DestinationDiscount);
    }
}