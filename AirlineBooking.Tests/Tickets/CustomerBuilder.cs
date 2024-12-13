using AirlineBooking.Domain.Tickets;

namespace AirlineBooking.Tests.Tickets;

public class CustomerBuilder
{
    private DateTime _birthday = new DateTime(2000, 5, 25);
    private string _name = "John";

    public CustomerBuilder WithBirthday(DateTime birthday)
    {
        _birthday = birthday;
        return this;
    }

    public CustomerBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public Customer Build()
    {
        return new Customer(_birthday, _name);
    }
}