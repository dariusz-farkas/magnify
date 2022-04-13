using AutoFixture;
using Magnify.Shipments.Domain.Common;

namespace Magnify.Shipments.Domain.Tests.Common.Builders;

public sealed class ShipmentBuilder
{
    private static readonly Fixture Fixture = new Fixture();
    private Shipper _shipper;

    private Address _pickAddress;

    private Address _destinationAddress;

    private Budget _budget;

    private AdditionalInfo? _additionalInfo;

    public static ShipmentBuilder Create()
    {
        return new ShipmentBuilder();
    }

    private ShipmentBuilder()
    {
        _shipper = Fixture.Create<Shipper>();
        _pickAddress = AddressBuilder.Create().Build();
        _destinationAddress = AddressBuilder.Create().Build();
        _budget = Budget.Create(Fixture.Create<decimal>(), CurrencyCode.Usd, true);
        _additionalInfo = Fixture.Create<AdditionalInfo>();
    }

    public ShipmentBuilder Reset()
    {
        _shipper = Fixture.Create<Shipper>();
        _pickAddress = AddressBuilder.Create().Build();
        _destinationAddress = AddressBuilder.Create().Build();
        _budget = Budget.Create(Fixture.Create<decimal>(), CurrencyCode.Usd, true);
        _additionalInfo = Fixture.Create<AdditionalInfo>();

        return this;
    }

    public ShipmentBuilder WithPickAddress(Address address)
    {
        _pickAddress = address;
        return this;
    }

    public ShipmentBuilder WithDestinationAddress(Address address)
    {
        _destinationAddress = address;
        return this;
    }

    public ShipmentBuilder WithBudget(Budget budget)
    {
        _budget = budget;
        return this;
    }

    public ShipmentBuilder WithShipper(Shipper shipper)
    {
        _shipper = shipper;
        return this;
    }

    public Shipment Build()
    {
        var shipment = Shipment.Create(_shipper, _pickAddress, _destinationAddress, _budget, _additionalInfo);
        Reset();
        return shipment;
    }
}