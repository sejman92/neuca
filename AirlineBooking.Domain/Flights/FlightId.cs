namespace AirlineBooking.Domain.Flights;

public class FlightId
{
    public string IataCode { get; }
    public int FlightNumber { get; }
    public string RandomCode { get; }

    public string FullId => $"{IataCode}{FlightNumber:D5}{RandomCode}";

    private FlightId(string iataCode, int flightNumber, string randomCode)
    {
        IataCode = iataCode;
        FlightNumber = flightNumber;
        RandomCode = randomCode;
    }

    public static FlightId Create(string iataCode, int flightNumber, string randomCode)
    {
        Validate(iataCode, flightNumber, randomCode);
        return new FlightId(iataCode.ToUpper(), flightNumber, randomCode.ToUpper());
    }

    private static void Validate(string iataCode, int flightNumber, string randomCode)
    {
        if (string.IsNullOrEmpty(iataCode) || iataCode.Length != 3)
            throw new ArgumentException("IATA code must be 3 letters.");
        if (flightNumber < 0 || flightNumber > 99999)
            throw new ArgumentException("Flight number must be between 00000 and 99999.");
        if (string.IsNullOrEmpty(randomCode) || randomCode.Length != 3)
            throw new ArgumentException("Random code must be 3 letters.");
    }
    
    public override bool Equals(object obj)
    {
        return obj is FlightId id && FullId == id.FullId;
    }

    public override int GetHashCode()
    {
        return FullId.GetHashCode();
    }
}