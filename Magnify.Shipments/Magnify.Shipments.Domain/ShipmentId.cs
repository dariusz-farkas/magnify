namespace Magnify.Shipments.Domain;

public record ShipmentId
{
    private ShipmentId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ShipmentId Create(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentNullException(nameof(value), "Shipper id cannot be empty");
        }

        return new ShipmentId(value);
    }

    public static implicit operator Guid(ShipmentId self) => self.Value;

    public static ShipmentId New()
    {
        return Create(Guid.NewGuid());
    }
}