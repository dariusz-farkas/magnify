using System;
using Magnify.Shipments.Domain.Tests.Common.Builders;
using Magnify.Shipments.Framework.Exceptions;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.ShipmentTests;

[TestFixture]
public class BookTests
{
    [Test]
    public void WhenCarrierIsNull_ShouldThrowArgumentNullException()
    {
        // arrange
        var shipment = ShipmentBuilder.Create().Build();

        Carrier carrier = null!;

        // act
        TestDelegate action = () => shipment.Book(carrier);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenShipmentIsAlreadyBooked_ShouldThrowBusinessException()
    {
        // arrange
        var shipment = ShipmentBuilder.Create().Build();
        var carrier = CarrierBuilder.Create().Build();
        shipment.Book(carrier);

        // act
        TestDelegate action = () => shipment.Book(carrier);

        // assert
        Assert.Throws<BusinessException>(action);
    }
}