namespace Magnify.Shipments.Domain;

public record OfferId
{
    private OfferId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static OfferId Create(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentNullException(nameof(value), "Shipper id cannot be empty");
        }

        return new OfferId(value);
    }

    public static implicit operator Guid(OfferId self) => self.Value;

    public static OfferId New()
    {
        return Create(Guid.NewGuid());
    }
}