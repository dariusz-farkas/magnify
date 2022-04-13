using System;
using Magnify.Shipments.Domain.Common;
using Magnify.Shipments.Domain.Tests.Common.Builders;
using Magnify.Shipments.Framework.Exceptions;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.ShipmentTests;

[TestFixture]
public class CreateTests
{
    [Test]
    public void WhenShipperIsNull_ShouldThrowArgumentNullException()
    {
        // arrange
        Shipper shipper = null!;
        var pickAddress = AddressBuilder.Create().Build();
        var deliveryAddress = AddressBuilder.Create().Build();
        var budget = Budget.Create(1000, CurrencyCode.Usd, false);

        // act
        TestDelegate action = () => Shipment.Create(shipper, pickAddress, deliveryAddress, budget);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenPickAddressIsNull_ShouldThrowArgumentNullException()
    {
        // arrange
        var shipper = ShipperBuilder.Create().Build();
        Address pickAddress = null!;
        var deliveryAddress = AddressBuilder.Create().Build();
        var budget = Budget.Create(1000, CurrencyCode.Usd, false);


        // act
        TestDelegate action = () => Shipment.Create(shipper, pickAddress, deliveryAddress, budget);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenDestinationAddressIsNull_ShouldThrowArgumentNullException()
    {
        // arrange
        var shipper = ShipperBuilder.Create().Build();
        var pickAddress = AddressBuilder.Create().Build();

        Address deliveryAddress = null!;

        var budget = Budget.Create(1000, CurrencyCode.Usd, false);


        // act
        TestDelegate action = () => Shipment.Create(shipper, pickAddress, deliveryAddress, budget);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenBudgetIsNull_ShouldThrowArgumentNullException()
    {
        // arrange
        var shipper = ShipperBuilder.Create().Build();
        var pickAddress = AddressBuilder.Create().Build();
        var deliveryAddress = AddressBuilder.Create().Build();
        Budget budget = null!;


        // act
        TestDelegate action = () => Shipment.Create(shipper, pickAddress, deliveryAddress, budget);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenPickAndDestinationAddressesAreTheSame_ShouldThrowBusinessException()
    {
        // arrange
        var shipper = ShipperBuilder.Create().Build();
        var pickAddress = Address.Create("street", "NYC", "00", CountryCode.Usa);
        var deliveryAddress = Address.Create("street", "NYC", "00", CountryCode.Usa);
        Budget budget = Budget.Create(1000, CurrencyCode.Usd, false);

        // act
        TestDelegate action = () => Shipment.Create(shipper, pickAddress, deliveryAddress, budget);

        // assert
        Assert.Throws<BusinessException>(action);
    }

    [Test]
    public void WhenShipmentDataValid_ShouldCreateShipment()
    {
        // arrange
        var shipper = ShipperBuilder.Create().Build();
        var pickAddress = AddressBuilder.Create().Build();
        var deliveryAddress = AddressBuilder.Create().Build();
        Budget budget = Budget.Create(1000, CurrencyCode.Usd, false);

        // act
        var shipment = Shipment.Create(shipper, pickAddress, deliveryAddress, budget);

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(shipment, Is.Not.Null);
            Assert.That(shipment.Budget, Is.EqualTo(budget));
            Assert.That(shipment.Shipper, Is.EqualTo(shipper));
            Assert.That(shipment.PickAddress, Is.EqualTo(pickAddress));
            Assert.That(shipment.DestinationAddress, Is.EqualTo(deliveryAddress));
        });
    }
}