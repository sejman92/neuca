using AirlineBooking.Domain.Flights;

namespace AirlineBooking.Tests.Flights;

[TestFixture]
public class FlightScheduleTests
{
    [Test]
    public void Create_ValidParameters_ReturnsFlightScheduleWithCorrectProperties()
    {
        // Arrange
        var departureTime = new TimeSpan(10, 30, 0);
        var departureDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday };

        // Act
        var schedule = FlightSchedule.Create(departureTime, departureDays);

        // Assert
        Assert.AreEqual(departureTime, schedule.DepartureTime);
        Assert.AreEqual(3, schedule.DepartureDays.Count);
        Assert.Contains(DayOfWeek.Monday, schedule.DepartureDays);
        Assert.Contains(DayOfWeek.Wednesday, schedule.DepartureDays);
        Assert.Contains(DayOfWeek.Friday, schedule.DepartureDays);
    }

    [Test]
    public void Create_DuplicateDepartureDays_RemovesDuplicates()
    {
        // Arrange
        var departureTime = new TimeSpan(10, 30, 0);
        var departureDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Friday };

        // Act
        var schedule = FlightSchedule.Create(departureTime, departureDays);

        // Assert
        Assert.AreEqual(2, schedule.DepartureDays.Count);
        Assert.Contains(DayOfWeek.Monday, schedule.DepartureDays);
        Assert.Contains(DayOfWeek.Friday, schedule.DepartureDays);
    }

    [Test]
    public void Create_InvalidDepartureTime_ThrowsArgumentException()
    {
        // Arrange
        var invalidTime = new TimeSpan(25, 0, 0); // Invalid time
        var departureDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => FlightSchedule.Create(invalidTime, departureDays));
        Assert.AreEqual("DepartureTime must represent a valid time of day.", ex.Message);
    }

    [Test]
    public void Create_NegativeDepartureTime_ThrowsArgumentException()
    {
        // Arrange
        var invalidTime = new TimeSpan(-1, 0, 0); // Negative time
        var departureDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => FlightSchedule.Create(invalidTime, departureDays));
        Assert.AreEqual("DepartureTime must represent a valid time of day.", ex.Message);
    }

    [Test]
    public void Create_EmptyDepartureDays_ThrowsArgumentException()
    {
        // Arrange
        var departureTime = new TimeSpan(10, 30, 0);
        var departureDays = new List<DayOfWeek>();

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => FlightSchedule.Create(departureTime, departureDays));
        Assert.AreEqual("DepartureDays must have at least one element.", ex.Message);
    }
    
    [Test]
    public void Create_NullDepartureDays_ThrowsArgumentException()
    {
        // Arrange
        var departureTime = new TimeSpan(10, 30, 0);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => FlightSchedule.Create(departureTime, null));
        Assert.AreEqual("DepartureDays must have at least one element.", ex.Message);
    }
}