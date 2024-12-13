namespace AirlineBooking.Domain.Tickets;

public class Price
{
    private const decimal MinPrice = 20;
    
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
        if (Value - discountAmount < MinPrice)
        {
            throw new ArgumentException("Price cannot be below minimum value.");
        }
        
        return new Price(Value - discountAmount);
    }
}