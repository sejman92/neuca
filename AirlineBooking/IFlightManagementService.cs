using AirlineBooking.Domain.Flights;

namespace AirlineBooking;

public interface IFlightManagementService
{
    void AddFlight(string ianaCode, int flightNumber, string randomCode, string from, string to, TimeSpan departureTime, List<DayOfWeek> days);
    Flight GetByFlightId(FlightId flightId);
}

public class FlightManagementService : IFlightManagementService
{
    private readonly IFlightsRepository _flightRepository;

    public FlightManagementService(IFlightsRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public void AddFlight(string ianaCode, int flightNumber, string randomCode, string from, string to, TimeSpan departureTime, List<DayOfWeek> days)
    {
        var flightId = FlightId.Create(ianaCode, flightNumber, randomCode);
        var route = new Route(from, to);
        var schedule = new FlightSchedule(departureTime, days);
        _flightRepository.AddFlight(new Flight(flightId, route, schedule));
    }

    public Flight GetByFlightId(FlightId flightId) => _flightRepository.GetByFlightId(flightId);
}