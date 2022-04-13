using AutoFixture;

namespace Magnify.Shipments.Domain.Tests.Common.Builders;

public sealed class ShipperBuilder
{
    private static readonly Fixture Fixture = new Fixture();
    private string _name;

    public static ShipperBuilder Create()
    {
        return new ShipperBuilder();
    }

    private ShipperBuilder()
    {
        _name = Fixture.Create<string>();
    }

    public ShipperBuilder Reset()
    {
        _name = Fixture.Create<string>();

        return this;
    }

    public ShipperBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public Shipper Build()
    {
        var shipper = Shipper.Create(_name);
        Reset();
        return shipper;
    }
}