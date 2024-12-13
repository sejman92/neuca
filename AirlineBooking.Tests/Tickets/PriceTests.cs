using AirlineBooking.Domain.Tickets;

namespace AirlineBooking.Tests.Tickets;

[TestFixture]
public class PriceTests
{
    [Test]
    public void Create_ValidPrice_ReturnsPriceWithCorrectValue()
    {
        // Arrange
        var value = 50m;

        // Act
        var price = Price.Create(value);

        // Assert
        Assert.AreEqual(value, price.Value);
    }

    [Test]
    public void Create_PriceBelowMinimum_ThrowsArgumentException()
    {
        // Arrange
        var value = 10m; // Below minimum price

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => Price.Create(value));
        Assert.AreEqual("Price cannot be below minimum value.", ex.Message);
    }

    [Test]
    public void CanApplyDiscount_ValidDiscount_ReturnsTrue()
    {
        // Arrange
        var price = Price.Create(50m);
        var discount = 20m;

        // Act
        var canApply = price.CanApplyDiscount(discount);

        // Assert
        Assert.IsTrue(canApply);
    }

    [Test]
    public void CanApplyDiscount_DiscountTooHigh_ReturnsFalse()
    {
        // Arrange
        var price = Price.Create(50m);
        var discount = 40m; // Would bring price below minimum

        // Act
        var canApply = price.CanApplyDiscount(discount);

        // Assert
        Assert.IsFalse(canApply);
    }

    [Test]
    public void ApplyDiscount_ValidDiscount_ReturnsPriceWithReducedValue()
    {
        // Arrange
        var price = Price.Create(50m);
        var discount = 20m;

        // Act
        var discountedPrice = price.ApplyDiscount(discount);

        // Assert
        Assert.AreEqual(30m, discountedPrice.Value);
    }

    [Test]
    public void ApplyDiscount_DiscountTooHigh_ThrowsArgumentException()
    {
        // Arrange
        var price = Price.Create(50m);
        var discount = 40m; // Would bring price below minimum

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => price.ApplyDiscount(discount));
        Assert.AreEqual("Price cannot be below minimum value.", ex.Message);
    }
}