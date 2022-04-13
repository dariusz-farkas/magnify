using System;
using Magnify.Shipments.Domain.Common;
using Magnify.Shipments.Domain.Tests.Common.Builders;
using Magnify.Shipments.Framework.Exceptions;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.ShipmentTests;

[TestFixture]
public class CreateCounterOfferTests
{
    [Test]
    public void WhenCarrierIsNull_ShouldThrowArgumentNullException()
    {
        // arrange
        var shipment = ShipmentBuilder.Create().Build();

        Carrier carrier = null!;

        // act
        TestDelegate action = () => shipment.CreateCounterOffer(carrier, Price.Create(100, CurrencyCode.Usd));

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenPriceIsNull_ShouldThrowArgumentNullException()
    {
        // arrange
        var shipment = ShipmentBuilder.Create().Build();

        Carrier carrier = CarrierBuilder.Create().Build();

        // act
        TestDelegate action = () => shipment.CreateCounterOffer(carrier, null!);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }
    
    [Test]
    public void WhenShipmentIsAlreadyBooked_ShouldThrowBusinessException()
    {
        // arrange
        Carrier carrier = CarrierBuilder.Create().Build();

        var shipment = ShipmentBuilder.Create().Build();
        shipment.Book(carrier);

        // act
        TestDelegate action = () => shipment.CreateCounterOffer(carrier, Price.Create(100, CurrencyCode.Usd));

        // assert
        Assert.Throws<BusinessException>(action, "Shipment has been already booked.");
    }

    [Test]
    public void WhenBudgetIsNotNegotiable_ShouldThrowBusinessException()
    {
        // arrange
        Carrier carrier = CarrierBuilder.Create().Build();

        var shipment = ShipmentBuilder
            .Create()
            .WithBudget(Budget.Create(1000, CurrencyCode.Usd, false))
            .Build();

        // act
        TestDelegate action = () => shipment.CreateCounterOffer(carrier, Price.Create(9900, CurrencyCode.Usd));

        // assert
        Assert.Throws<BusinessException>(action, "Budget is not negotiable.");
    }

    [Test]
    public void WhenPreviousOfferExitedForCarrierWithSamePrice_ShouldThrowBusinessException()
    {
        // arrange
        Carrier carrier = CarrierBuilder.Create().Build();
        var offerPrice = Price.Create(3000, CurrencyCode.Usd);

        var shipment = ShipmentBuilder
            .Create()
            .WithBudget(Budget.Create(Price.Create(1000, CurrencyCode.Usd), true))
            .Build();

        shipment.CreateCounterOffer(carrier, offerPrice);

        // act
        TestDelegate action = () => shipment.CreateCounterOffer(carrier, offerPrice);

        // assert
        Assert.Throws<BusinessException>(action, "Offer price is the same.");
    }

    [Test]
    public void WhenPreviousOfferExitedForCarrierWithDifferentPrice_ShouldTerminatePreviousOffer()
    {
        // arrange
        Carrier carrier = CarrierBuilder.Create().Build();

        var shipment = ShipmentBuilder
            .Create()
            .WithBudget(Budget.Create(1000, CurrencyCode.Usd, true))
            .Build();

        shipment.CreateCounterOffer(carrier, Price.Create(3000, CurrencyCode.Usd));

        var offer = shipment.Offers[carrier];

        // act
        shipment.CreateCounterOffer(carrier, Price.Create(5000, CurrencyCode.Usd));

        // assert
        Assert.That(offer.State, Is.EqualTo(OfferState.Terminated)); 
    }

    [Test]
    public void WhenCounterOfferIsValid_ShouldCreateNewOffer()
    {
        // arrange
        Carrier carrier = CarrierBuilder.Create().Build();

        var shipment = ShipmentBuilder
            .Create()
            .WithBudget(Budget.Create(1000, CurrencyCode.Usd, true))
            .Build();
        var price = Price.Create(5000, CurrencyCode.Usd);

        // act
        shipment.CreateCounterOffer(carrier, price);

        // assert
        var offer = shipment.Offers[carrier];
        Assert.Multiple(() =>
        {
            Assert.NotNull(offer);
            Assert.That(offer.State, Is.EqualTo(OfferState.Created));
            Assert.That(offer.Price, Is.EqualTo(price));
            Assert.That(offer.Carrier, Is.EqualTo(carrier));
        });
    }
}