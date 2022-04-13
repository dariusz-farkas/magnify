using System.Collections.ObjectModel;
using Magnify.Shipments.Framework.Extensions;

namespace Magnify.Shipments.Domain;

public class Shipper
{
    private readonly IList<Shipment> _shipments;

    private Shipper(string name)
    {
        Name = name;
        _shipments = new List<Shipment>();
    }

    public string Name { get; }

    public IReadOnlyCollection<Shipment> Shipments => new ReadOnlyCollection<Shipment>(_shipments);

    public static Shipper Create(string name)
    {
        name.ThrowWhenNullOrEmpty(nameof(name));

        return new Shipper(name);
    }

    internal void AddShipment(Shipment shipment)
    {
        _shipments.Add(shipment);
    }
}