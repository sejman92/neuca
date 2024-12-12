using AirlineBooking.Domain.Flights;

namespace AirlineBooking.Persistence.Flight;

public class FlightsRepository : IFlightsRepository
{
    private List<Domain.Flights.Flight> _flights = [];
    
    public void AddFlight(Domain.Flights.Flight flight)
    {
        _flights.Add(flight);
        Console.WriteLine($"Flight {flight.FlightId.FullId} has been added.");
    }

    public Domain.Flights.Flight GetByFlightId(FlightId flightId)
    {
        var flight = _flights.SingleOrDefault(x => x.FlightId.Equals(flightId));

        if (flight != null) return flight;
        
        Console.WriteLine($"Flight {flightId.FullId} not found.");
        throw new Exception($"Flight {flightId.FullId} not found.\"");
    }
}