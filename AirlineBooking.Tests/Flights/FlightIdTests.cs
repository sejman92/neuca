using AirlineBooking.Domain.Flights;

namespace AirlineBooking.Tests.Flights;

[TestFixture]
public class FlightIdTests
{
    [TestCase("klm", 0, "abc")]
    [TestCase("Klm", 2, "Def")]
    [TestCase("KLm", 34, "GHI")]
    [TestCase("KlM", 123, "jKL")]
    [TestCase("KlM", 1444, "mnO")]
    [TestCase("klM", 99999, "uWu")]
    public void Create_ValidParameters_ReturnsFlightIdWithCorrectProperties(string iataCode, int flightNumber, string randomCode)
    {
        //Arrange
        var expectedFullId = $"{iataCode.ToUpper()}{flightNumber:D5}{randomCode.ToUpper()}";
        
        // Act
        var flightId = FlightId.Create(iataCode, flightNumber, randomCode);

        // Assert
        Assert.AreEqual(iataCode.ToUpper(), flightId.IataCode);
        Assert.AreEqual(flightNumber, flightId.FlightNumber);
        Assert.AreEqual(randomCode.ToUpper(), flightId.RandomCode);
        Assert.AreEqual(expectedFullId, flightId.FullId);
    }
    
    [TestCase(null)]
    [TestCase("")]
    [TestCase("a")]
    [TestCase("AB")]
    [TestCase("ABCD")]
    public void Create_InvalidIataCode_ThrowsArgumentException(string iataCode)
    {
        // Arrange
        var flightNumber = 12345;
        var randomCode = "XYZ";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => FlightId.Create(iataCode, flightNumber, randomCode));
        Assert.AreEqual("IATA code must be 3 letters.", ex.Message);
    }

    [TestCase(-1)]
    [TestCase(100000)]
    public void Create_InvalidFlightNumber_ThrowsArgumentException(int flightNumber)
    {
        // Arrange
        var iataCode = "ABC";
        var randomCode = "XYZ";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => FlightId.Create(iataCode, flightNumber, randomCode));
        Assert.AreEqual("Flight number must be between 00000 and 99999.", ex.Message);
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("a")]
    [TestCase("AB")]
    [TestCase("ABCD")]
    public void Create_InvalidRandomCode_ThrowsArgumentException(string randomCode)
    {
        // Arrange
        var iataCode = "ABC";
        var flightNumber = 12345;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => FlightId.Create(iataCode, flightNumber, randomCode));
        Assert.AreEqual("Random code must be 3 letters.", ex.Message);
    }

    [Test]
    public void Equals_SameFullId_ReturnsTrue()
    {
        // Arrange
        var flightId1 = FlightId.Create("ABC", 12345, "XYZ");
        var flightId2 = FlightId.Create("abc", 12345, "xyz"); // Case insensitive

        // Act & Assert
        Assert.IsTrue(flightId1.Equals(flightId2));
        Assert.IsTrue(flightId2.Equals(flightId1));
    }

    [Test]
    public void Equals_DifferentFullId_ReturnsFalse()
    {
        // Arrange
        var flightId1 = FlightId.Create("ABC", 12345, "XYZ");
        var flightId2 = FlightId.Create("DEF", 12345, "XYZ");

        // Act & Assert
        Assert.That(flightId1.Equals(flightId2), Is.True);
    }

    [Test]
    public void GetHashCode_SameFullId_ReturnsSameHashCode()
    {
        // Arrange
        var flightId1 = FlightId.Create("ABC", 12345, "XYZ");
        var flightId2 = FlightId.Create("abc", 12345, "xyz");

        // Act & Assert
        Assert.AreEqual(flightId1.GetHashCode(), flightId2.GetHashCode());
    }

    [Test]
    public void GetHashCode_DifferentFullId_ReturnsDifferentHashCode()
    {
        // Arrange
        var flightId1 = FlightId.Create("ABC", 12345, "XYZ");
        var flightId2 = FlightId.Create("DEF", 12345, "XYZ");

        // Act & Assert
        Assert.AreNotEqual(flightId1.GetHashCode(), flightId2.GetHashCode());
    }
}