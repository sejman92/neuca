using AirlineBooking.Domain.Flights;
using AirlineBooking.Domain.Tickets;

namespace AirlineBooking;

public interface IFlightManagementService
{
    void AddFlight(string ianaCode, int flightNumber, string randomCode, string from, string to, TimeSpan departureTime, List<DayOfWeek> days, decimal basePrice);
    Flight GetByFlightId(FlightId flightId);
}

public class FlightManagementService : IFlightManagementService
{
    private readonly IFlightsRepository _flightRepository;

    public FlightManagementService(IFlightsRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public void AddFlight(string ianaCode, int flightNumber, string randomCode, string from, string to, TimeSpan departureTime, List<DayOfWeek> days, decimal basePrice)
    {
        var flightId = FlightId.Create(ianaCode, flightNumber, randomCode);
        var route = new Route(from, to);
        var schedule = FlightSchedule.Create(departureTime, days);
        var price = Price.Create(basePrice); 
        
        _flightRepository.AddFlight(new Flight(flightId, route, schedule, price));
    }

    public Flight GetByFlightId(FlightId flightId) => _flightRepository.GetByFlightId(flightId);
}