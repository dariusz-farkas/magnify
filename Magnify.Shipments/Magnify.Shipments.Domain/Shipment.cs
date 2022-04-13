using System.Collections.ObjectModel;
using Magnify.Shipments.Domain.Common;
using Magnify.Shipments.Framework.Exceptions;

namespace Magnify.Shipments.Domain;

public sealed class Shipment
{
    private readonly Dictionary<Carrier, Offer> _offers;

    private Shipment(Shipper shipper, Address pickAddress, Address destinationAddress, Budget budget, AdditionalInfo? additionalInfo)
    {
        ShipmentId = ShipmentId.New();
        Shipper = shipper;
        PickAddress = pickAddress;
        DestinationAddress = destinationAddress;
        Budget = budget;
        AdditionalInfo = additionalInfo;
        _offers = new Dictionary<Carrier, Offer>();
    }

    public ShipmentId ShipmentId { get; set; }

    public Shipper Shipper { get; }

    public Address PickAddress { get; }

    public Address DestinationAddress { get; }

    public Budget Budget { get; }

    public AdditionalInfo? AdditionalInfo { get; }

    public DateTimeOffset? BookingTimestamp { get; private set; }

    public IReadOnlyDictionary<Carrier, Offer> Offers => new ReadOnlyDictionary<Carrier, Offer>(_offers);

    public bool IsActive => Offers.Values.All(x => x.State != OfferState.Accepted);

    public static Shipment Create(
        Shipper shipper, 
        Address pickAddress, 
        Address destinationAddress, 
        Budget budget,
        AdditionalInfo? additionalInfo = null)
    {
        if (shipper == null) throw new ArgumentNullException(nameof(shipper));
        if (pickAddress == null) throw new ArgumentNullException(nameof(pickAddress));
        if (destinationAddress == null) throw new ArgumentNullException(nameof(destinationAddress));
        if (budget == null) throw new ArgumentNullException(nameof(budget));

        if (pickAddress == destinationAddress)
        {
            throw new BusinessException("Pick and destination address cannot be the same.");
        }

        var shipment = new Shipment(shipper, pickAddress, destinationAddress, budget, additionalInfo);

        shipper.AddShipment(shipment);

        return shipment;
    }

    public void Book(Carrier carrier)
    {
        if (carrier == null)
        {
            throw new ArgumentNullException(nameof(carrier));
        }

        if (!IsActive)
        {
            throw new BusinessException("Shipment has been already booked.");
        }

        var offer = Offer.Create(this, carrier, Budget.Price);
        _offers.Add(carrier, offer);
        offer.Accept();
        BookingTimestamp = DateTimeOffset.Now;
    }

    public void Accept(Shipper user, OfferId offerId)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (user != this.Shipper)
        {
            throw new BusinessException("User is not the owner of the shipment.");
        }

        if (!IsActive)
        {
            throw new BusinessException("Shipment has been already booked.");
        }

        var offer = Offers.Values.SingleOrDefault(offer => offer.OfferId == offerId);

        if (offer == null)
        {
            throw new NotFoundException($"Offer {offerId} does not exists.");
        }

        offer.Accept();
        BookingTimestamp = DateTimeOffset.Now;
    }

    public void Reject(Shipper user, OfferId offerId)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (user != this.Shipper)
        {
            throw new BusinessException("User is not the owner of the shipment.");
        }

        if (!IsActive)
        {
            throw new BusinessException("Shipment has been already booked.");
        }

        var offer = Offers.Values.SingleOrDefault(offer => offer.OfferId == offerId);

        if (offer == null)
        {
            throw new NotFoundException($"Offer {offerId} does not exists.");
        }

        offer.Reject();
    }

    public void CreateCounterOffer(Carrier carrier, Price price)
    {
        if (carrier == null)
        {
            throw new ArgumentNullException(nameof(carrier));
        }

        if (price == null)
        {
            throw new ArgumentNullException(nameof(price));
        }

        if (!IsActive)
        {
            throw new BusinessException("Shipment has been already booked.");
        }

        if (!this.Budget.IsNegotiable)
        {
            throw new BusinessException("Budget is not negotiable.");
        }

        if (_offers.TryGetValue(carrier, out var offer))
        {
            if (offer.Price == price)
            {
                throw new BusinessException("Offer price is the same.");
            }

            if (offer.State == OfferState.Created)
            {
                offer.Terminate(carrier);
            }
        }

        offer = Offer.Create(this, carrier, price);

        this._offers[carrier] =  offer;
    }
}