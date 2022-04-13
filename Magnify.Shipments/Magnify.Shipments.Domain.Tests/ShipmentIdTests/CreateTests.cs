using System;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.ShipmentIdTests;

[TestFixture]
public class CreateTests
{
    [Test]
    public void WhenValueIsEmpty_ShouldThrowArgumentNullException()
    {
        // arrange
        var value = Guid.Empty;

        // act
        TestDelegate action = () => ShipmentId.Create(value);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenValidIsValid_ShouldCreateOfferId()
    {
        // arrange
        var value = Guid.NewGuid();

        // act
        var shipmentId = ShipmentId.Create(value);

        // assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(shipmentId);
            Assert.That(shipmentId.Value, Is.EqualTo(value));
        });
    }
}