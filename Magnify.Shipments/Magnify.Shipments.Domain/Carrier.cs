using System.Collections.ObjectModel;
using Magnify.Shipments.Framework.Extensions;

namespace Magnify.Shipments.Domain;

public class Carrier
{
    private readonly IList<Offer> _offers;

    private Carrier(string name)
    {
        Name = name;
        _offers = new List<Offer>();
    }

    public string Name { get; }

    public IReadOnlyCollection<Offer> Offers => new ReadOnlyCollection<Offer>(_offers);

    public static Carrier Create(string name)
    {
        name.ThrowWhenNullOrEmpty(nameof(name));
        return new Carrier(name);
    }

    internal void AddOffer(Offer offer)
    {
        _offers.Add(offer);
    }
}