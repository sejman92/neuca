using AirlineBooking.Domain.Discounts;
using AirlineBooking.Domain.Flights;

namespace AirlineBooking.Domain.Tickets;

public class Ticket
{
    public Guid Id { get; private set; }
    public Flight Flight { get; private set; }
    public Customer Customer { get; private set; }
    public DateTime Departure { get; private set; }
    public Price Price { get; private set; }
    public List<IDiscount> AppliedDiscounts { get; private set; }
    public Tenant Tenant { get; private set; }
    
    
    private Ticket(Flight flight, Customer customer, DateTime departure, Tenant tenant)
    {
        Id = Guid.NewGuid();
        Flight = flight;
        Customer = customer;
        Departure = departure;
        Price = flight.BasePrice;
        Tenant = tenant;
    }

    public static Ticket Create(Flight flight, Customer customer, DateTime departure, IEnumerable<IDiscount> activeDiscounts, Tenant tenant)
    {
        Validate(flight, departure);
        var ticket = new Ticket(flight, customer, departure, tenant);
        ticket.ApplyDiscounts(activeDiscounts);
        return ticket;
    }

    private void ApplyDiscounts(IEnumerable<IDiscount> activeDiscounts)
    {
        foreach (var discount in activeDiscounts)
        {
            if (discount.IsApplicable(this))
            {
                if (Price.CanApplyDiscount(discount.Value))
                {
                    ApplyDiscount(discount);
                }
            }
        }
    }

    private void ApplyDiscount(IDiscount discount)
    {
        Price.ApplyDiscount(discount.Value);
        Console.WriteLine($"Discount: {discount.Description} applied.");
        if (IsTenantA())
        {
            AppliedDiscounts.Add(discount);
            Console.WriteLine($"Discount: {discount.Description} has been registered.");
        }
    }

    private bool IsTenantA() => Tenant == Tenant.A;
    
    private static void Validate(Flight flight, DateTime departure)
    {
        if (flight.Schedule.DepartureDays.Contains(departure.Date.DayOfWeek))
        {
            throw new ArgumentException($"Flight is not available for {departure:dd/MM/yyyy}.");
        }
    }
}