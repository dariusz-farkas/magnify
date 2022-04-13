using System;
using Magnify.Shipments.Domain.Common;
using Magnify.Shipments.Domain.Tests.Common.Builders;
using Magnify.Shipments.Framework.Exceptions;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.ShipmentTests;

[TestFixture]
public class AcceptTests
{
    [Test]
    public void WhenShipperIsNull_ShouldThrowArgumentNullException()
    {
        // arrange
        var shipment = ShipmentBuilder.Create().Build();
        OfferId offerId = OfferId.New();

        Shipper shipper = null!;

        // act
        TestDelegate action = () => shipment.Accept(shipper, offerId);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenShipperIsDifferentThanShipmentOwner_ShouldThrowBusinessException()
    {
        // arrange
        Shipper shipper = ShipperBuilder.Create().Build();
        var shipment = ShipmentBuilder.Create().Build();
        OfferId offerId = OfferId.New();

        // act
        TestDelegate action = () => shipment.Accept(shipper, offerId);

        // assert
        Assert.Throws<BusinessException>(action, "User is not the owner of the shipment");
    }

    [Test]
    public void WhenShipmentIsAlreadyBooked_ShouldThrowBusinessException()
    {
        // arrange
        Shipper shipper = ShipperBuilder.Create().Build();
        Carrier carrier = CarrierBuilder.Create().Build();

        var shipment = ShipmentBuilder.Create().Build();
        shipment.Book(carrier);

        OfferId offerId = OfferId.New();

        // act
        TestDelegate action = () => shipment.Accept(shipper, offerId);

        // assert
        Assert.Throws<BusinessException>(action, "Shipment has been already booked");
    }

    [Test]
    public void WhenOfferDoesNotExists_ShouldThrowNotFoundException()
    {
        // arrange
        Shipper shipper = ShipperBuilder.Create().Build();

        var shipment = ShipmentBuilder
            .Create()
            .WithShipper(shipper)
            .Build();

        OfferId offerId = OfferId.New();

        // act
        TestDelegate action = () => shipment.Accept(shipper, offerId);

        // assert
        Assert.Throws<NotFoundException>(action, $"Offer {offerId} does not exists.");
    }

    [Test]
    public void WhenOfferExists_ShouldAcceptOffer()
    {
        // arrange
        Shipper shipper = ShipperBuilder.Create().Build();
        Carrier carrier = CarrierBuilder.Create().Build();

        var shipment = ShipmentBuilder
            .Create()
            .WithShipper(shipper)
            .WithBudget(Budget.Create(1000, CurrencyCode.Usd, true))
            .Build();

        shipment.CreateCounterOffer(carrier, Price.Create(3000, CurrencyCode.Usd));

        var offer = shipment.Offers[carrier];

        // act
        shipment.Accept(shipper, offer.OfferId);

        // assert
        Assert.That(offer.State, Is.EqualTo(OfferState.Accepted)); 
    }

    [Test]
    public void WhenOfferExists_ShouldMarkShipmentAsInactive()
    {
        // arrange
        var shipper = ShipperBuilder.Create().Build();
        var carrier = CarrierBuilder.Create().Build();

        var shipment = ShipmentBuilder
            .Create()
            .WithShipper(shipper)
            .WithBudget(Budget.Create(1000, CurrencyCode.Usd, true))
            .Build();

        shipment.CreateCounterOffer(carrier, Price.Create(3000, CurrencyCode.Usd));

        var offer = shipment.Offers[carrier];

        // act
        shipment.Accept(shipper, offer.OfferId);

        // assert
        Assert.That(shipment.IsActive, Is.False); 
    }
}