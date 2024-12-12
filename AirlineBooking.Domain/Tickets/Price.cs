namespace AirlineBooking.Domain.Tickets;

public class Price
{
    private const decimal MinPrice = 20;
    private const decimal DefaultPrice = 20;
    
    public decimal Value { get; private set; }

    private Price(decimal value)
    {
        Value = value;
    }

    public static Price Create(decimal value)
    {
        Validate(value);
        return new Price(value);
    }

    public static Price CreateDefault()
    {
        return new Price(30m);
    }

    private static void Validate(decimal value)
    {
        if (value < MinPrice)
            throw new ArgumentException("Price cannot be below minimum value.");
    }
    
    public bool CanApplyDiscount(decimal discountAmount)
    {
        return Value - discountAmount >= MinPrice;
    }

    public Price ApplyDiscount(decimal discountAmount)
    {
        return new Price(Value - discountAmount);
    }
}