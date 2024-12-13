using AirlineBooking.Domain;
using AirlineBooking.Domain.Discounts;
using AirlineBooking.Domain.Flights;
using AirlineBooking.Domain.Tickets;
using Moq;

namespace AirlineBooking.Tests.Tickets;

[TestFixture]
public class TicketTests
{
    [Test]
    public void Create_ValidParameters_ReturnsTicketWithCorrectProperties()
    {
        // Arrange
        var flight = new FlightBuilder().Build();
        var departure = new DateTime(2024, 12, 15);
        var customer = new CustomerBuilder().Build();
        var tenant = Tenant.A;
        var discounts = new List<IDiscount>();

        // Act
        var ticket = Ticket.Create(flight, customer, departure, discounts, tenant);

        // Assert
        Assert.AreEqual(flight, ticket.Flight);
        Assert.AreEqual(customer, ticket.Customer);
        Assert.AreEqual(departure, ticket.Departure);
        Assert.AreEqual(tenant, ticket.Tenant);
        Assert.AreEqual(100m, ticket.Price.Value);
    }

    [Test]
    public void Create_FlightNotAvailableOnDate_ThrowsArgumentException()
    {
        // Arrange
        var flight = new FlightBuilder()
            .WithFlightSchedule(TimeSpan.FromHours(5), [DayOfWeek.Monday, DayOfWeek.Wednesday])
            .Build();
        
        var departure = new DateTime(2024, 12, 17); // A Tuesday
        var customer = new CustomerBuilder().Build();
        var tenant = Tenant.A;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => Ticket.Create(flight, customer, departure, new List<IDiscount>(), tenant));
        Assert.AreEqual("Flight is not available for 17/12/2024.", ex.Message);
    }

    [Test]
    public void Create_ApplicableDiscounts_AppliesDiscountsCorrectly_AndStoreAppliedDiscounts()
    {
        // Arrange
        var flight = new FlightBuilder()
            .WithRoute("Europe", "Africa")
            .WithFlightSchedule(TimeSpan.FromHours(3), [DayOfWeek.Thursday])
            .WithBasePrice(85m)
            .Build();
        
        var departure = new DateTime(2024, 12, 12); // Thursday
        var customer = new CustomerBuilder().Build();
        var tenant = Tenant.A;

        var discount = new AfricaThursdayDiscount();

        var discounts = new List<IDiscount> { discount };

        // Act
        var ticket = Ticket.Create(flight, customer, departure, discounts, tenant);

        // Assert
        Assert.AreEqual(80m, ticket.Price.Value);
        Assert.Contains(discount, ticket.AppliedDiscounts);
    }
    
    [Test]
    public void Create_ApplicableDiscounts_AppliesDiscountsCorrectly_AndNotStoreAppliedDiscounts()
    {
        // Arrange
        var flight = new FlightBuilder()
            .WithRoute("Europe", "Africa")
            .WithFlightSchedule(TimeSpan.FromHours(3), [DayOfWeek.Thursday])
            .WithBasePrice(85m)
            .Build();
        
        var departure = new DateTime(2024, 12, 12); // Thursday
        var customer = new CustomerBuilder().Build();
        var tenant = Tenant.B;

        var discount = new AfricaThursdayDiscount();

        var discounts = new List<IDiscount> { discount };

        // Act
        var ticket = Ticket.Create(flight, customer, departure, discounts, tenant);

        // Assert
        Assert.AreEqual(80m, ticket.Price.Value);
        Assert.That(ticket.AppliedDiscounts, Is.Null);
    }

    [Test]
    public void Create_DiscountNotApplicable_DoesNotApplyDiscount()
    {
        // Arrange
        var flight = new FlightBuilder()
            .WithRoute("Europe", "Asua")
            .WithFlightSchedule(TimeSpan.FromHours(3), [DayOfWeek.Thursday])
            .WithBasePrice(100m)
            .Build();
        
        var departure = new DateTime(2024, 12, 12); // Thursday
        var customer = new CustomerBuilder().Build();
        var tenant = Tenant.B;

        var discount = new AfricaThursdayDiscount();

        var discounts = new List<IDiscount> { discount };

        // Act
        var ticket = Ticket.Create(flight, customer, departure, discounts, tenant);

        // Assert
        Assert.AreEqual(100m, ticket.Price.Value);
        Assert.IsEmpty(ticket.AppliedDiscounts);
    }

    [Test]
    public void Create_DiscountBreachesMinimumPrice_DoesNotApplyDiscount()
    {
        // Arrange
        var flight = new FlightBuilder()
            .WithRoute("Europe", "Africa")
            .WithFlightSchedule(TimeSpan.FromHours(3), [DayOfWeek.Thursday])
            .WithBasePrice(22m)
            .Build();
        
        var departure = new DateTime(2024, 12, 12); // Thursday
        var customer = new CustomerBuilder().Build();
        var tenant = Tenant.A;

        var discount = new AfricaThursdayDiscount();

        var discounts = new List<IDiscount> { discount };

        // Act
        var ticket = Ticket.Create(flight, customer, departure, discounts, tenant);

        // Assert
        Assert.AreEqual(22m, ticket.Price.Value);
        Assert.IsEmpty(ticket.AppliedDiscounts);
    }
}