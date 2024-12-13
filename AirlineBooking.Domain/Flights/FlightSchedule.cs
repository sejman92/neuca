namespace AirlineBooking.Domain.Flights;

public record FlightSchedule(TimeSpan DepartureTime, List<DayOfWeek> DepartureDays)
{
    public static FlightSchedule Create(TimeSpan departureTime, List<DayOfWeek> departureDays)
    {
        Validate(departureTime, departureDays);
        return new FlightSchedule(departureTime, departureDays.Distinct().ToList());
    }

    private static void Validate(TimeSpan departureTime, List<DayOfWeek> departureDays)
    {
        if (departureTime < TimeSpan.Zero || departureTime >= TimeSpan.FromDays(1))
        {
            throw new ArgumentException("DepartureTime must represent a valid time of day.");
        }

        if (departureDays == null || departureDays.Count == 0)
        {
            throw new ArgumentException("DepartureDays must have at least one element.");
        }
    }
}