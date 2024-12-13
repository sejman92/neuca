using AirlineBooking.Domain.Flights;
using AirlineBooking.Domain.Tickets;

namespace AirlineBooking.Tests.Tickets;

public class FlightBuilder
{
    private FlightId _flightId = FlightId.Create("ABC", 123, "XYZ");
    private Route _route = new("Poland", "America");
    private Price _basePrice = Price.Create(100m);
    private FlightSchedule _flightSchedule = FlightSchedule.Create(TimeSpan.FromHours(8), [DayOfWeek.Monday, DayOfWeek.Friday]);

    public FlightBuilder WithFlightId(string ianaCode, int flightNumber, string randomCode)
    {
        _flightId = FlightId.Create(ianaCode, flightNumber, randomCode);
        return this;
    }

    public FlightBuilder WithRoute(string from, string to)
    {
        _route = new Route(from, to);
        return this;
    }
    
    public FlightBuilder WithBasePrice(decimal price)
    {
        _basePrice = Price.Create(price);
        return this;
    }

    public FlightBuilder WithFlightSchedule(TimeSpan departureTime, List<DayOfWeek> departureDays)
    {
        _flightSchedule = FlightSchedule.Create(departureTime, departureDays);
        return this;
    }
    
    public Flight Build()
    {
        return new Flight(_flightId, _route, _flightSchedule, _basePrice);
    }
}