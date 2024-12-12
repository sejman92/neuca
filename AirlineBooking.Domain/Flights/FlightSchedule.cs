namespace AirlineBooking.Domain.Flights;

public record FlightSchedule(TimeSpan DepartureTime, List<DayOfWeek> DepartureDays)
{
   
}