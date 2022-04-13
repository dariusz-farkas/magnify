using Magnify.Shipments.Domain.Common;

namespace Magnify.Shipments.Domain.Tests.Common.Builders;

public sealed class OfferBuilder
{
    private Shipment _shipment;
    private Carrier _carrier;
    private Price _price;

    public static OfferBuilder Create()
    {
        return new OfferBuilder();
    }

    private OfferBuilder()
    {
        _shipment = ShipmentBuilder.Create().Build();
        _carrier = CarrierBuilder.Create().Build();
        _price = PriceBuilder.Create().Build();
    }

    public OfferBuilder Reset()
    {
        _shipment = ShipmentBuilder.Create().Build();
        _carrier = CarrierBuilder.Create().Build();
        _price = PriceBuilder.Create().Build();

        return this;
    }
    public Offer Build()
    {
        var shipment = Offer.Create(_shipment, _carrier, _price);
        Reset();
        return shipment;
    }
}