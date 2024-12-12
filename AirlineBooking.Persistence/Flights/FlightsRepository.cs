using AirlineBooking.Domain.Flights;

namespace AirlineBooking.Persistence.Flights;

public class FlightsRepository : IFlightsRepository
{
    private List<Flight> _flights = [];
    
    public void AddFlight(Flight flight)
    {
        _flights.Add(flight);
        Console.WriteLine($"Flight {flight.FlightId.FullId} has been added.");
    }

    public Flight GetByFlightId(FlightId flightId)
    {
        var flight = _flights.SingleOrDefault(x => x.FlightId.Equals(flightId));

        if (flight != null) return flight;
        
        Console.WriteLine($"Flight {flightId.FullId} not found.");
        throw new Exception($"Flight {flightId.FullId} not found.\"");
    }
}