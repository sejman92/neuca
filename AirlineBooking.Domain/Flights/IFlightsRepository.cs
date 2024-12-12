namespace AirlineBooking.Domain.Flights;

public interface IFlightsRepository
{
    void AddFlight(Flight flight);
    Flight GetByFlightId(FlightId flightId);
}