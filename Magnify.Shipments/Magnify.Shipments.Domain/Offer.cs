using Magnify.Shipments.Domain.Common;
using Magnify.Shipments.Framework.Exceptions;

namespace Magnify.Shipments.Domain;

public class Offer
{
    private Offer(Shipment shipment, Carrier carrier, Price price)
    {
        OfferId = OfferId.Create(Guid.NewGuid());
        Shipment = shipment;
        Carrier = carrier;
        Price = price;
        State = OfferState.Created;
    }

    public OfferId OfferId { get; }

    public Shipment Shipment { get; }

    public Carrier Carrier { get; }

    public Price Price { get; }

    public OfferState State { get; private set; }

    internal static Offer Create(Shipment shipment, Carrier carrier, Price price)
    {
        if (shipment == null)
        {
            throw new ArgumentNullException(nameof(shipment));
        }

        if (carrier == null)
        {
            throw new ArgumentNullException(nameof(carrier));
        }

        if (price == null)
        {
            throw new ArgumentNullException(nameof(price));
        }

        var offer = new Offer(shipment, carrier, price);
        carrier.AddOffer(offer);

        return offer;
    }

    internal void Accept()
    {
        if (State != OfferState.Created)
        {
            throw new BusinessException("Cannot accept an offer.");
        }

        State = OfferState.Accepted;
    }

    internal void Reject()
    {
        if (State != OfferState.Created)
        {
            throw new BusinessException("Cannot reject an offer.");
        }

        State = OfferState.Rejected;
    }

    internal void Terminate(Carrier user)
    {
        if (user != Carrier)
        {
            throw new BusinessException("User is not the carrier of the offer.");
        }

        if (State != OfferState.Created)
        {
            throw new BusinessException("Cannot terminate an offer.");
        }

        State = OfferState.Terminated;
    }
}