using System;
using Magnify.Shipments.Domain.Common;
using Magnify.Shipments.Domain.Tests.Common.Builders;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.OfferTests;

[TestFixture]
public class CreateTests
{
    [Test]
    public void WhenShipmentIsNull_ShouldThrowArgumentNullException()
    {
        // arrange
        Shipment shipment = default!;
        var carrier = CarrierBuilder.Create().Build();
        var price = PriceBuilder.Create().Build();

        // act
        TestDelegate action = () => Offer.Create(shipment, carrier, price);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenCarrierIsNull_ShouldThrowArgumentNullException()
    {
        // arrange
        var shipment = ShipmentBuilder.Create().Build();
        Carrier carrier = default!;
        var price = PriceBuilder.Create().Build();

        // act
        TestDelegate action = () => Offer.Create(shipment, carrier, price);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenPriceIsNull_ShouldThrowArgumentNullException()
    {
        // arrange
        var shipment = ShipmentBuilder.Create().Build();
        var carrier = CarrierBuilder.Create().Build();
        Price price = default!;

        // act
        TestDelegate action = () => Offer.Create(shipment, carrier, price);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenSuccessful_ShouldReturnOffer()
    {
        // arrange
        var shipment = ShipmentBuilder.Create().Build();
        var carrier = CarrierBuilder.Create().Build();
        var price = PriceBuilder.Create().Build();

        // act
        var offer = Offer.Create(shipment, carrier, price);

        // assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(offer);
            Assert.That(offer.Carrier, Is.EqualTo(carrier));
            Assert.That(offer.Price, Is.EqualTo(price));
            Assert.That(offer.Shipment.ShipmentId, Is.EqualTo(shipment.ShipmentId));
        });
    }
}