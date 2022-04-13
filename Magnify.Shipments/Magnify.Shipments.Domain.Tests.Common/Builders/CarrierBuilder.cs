using AutoFixture;

namespace Magnify.Shipments.Domain.Tests.Common.Builders;

public sealed class CarrierBuilder
{
    private static readonly Fixture Fixture = new Fixture();
    private string _name;

    public static CarrierBuilder Create()
    {
        return new CarrierBuilder();
    }

    private CarrierBuilder()
    {
        _name = Fixture.Create<string>();
    }

    public CarrierBuilder Reset()
    {
        _name = Fixture.Create<string>();

        return this;
    }

    public CarrierBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public Carrier Build()
    {
        var carrier = Carrier.Create(_name);
        Reset();
        return carrier;
    }
}