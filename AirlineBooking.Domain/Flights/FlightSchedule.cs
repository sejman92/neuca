namespace AirlineBooking.Domain.Flights;

public record FlightSchedule(TimeSpan DepartureTime, List<DayOfWeek> DepartureDays)
{
   public static FlightSchedule Create(TimeSpan departureTime, List<DayOfWeek> departureDays) =>
      new (departureTime, departureDays.Distinct().ToList());
}