namespace Magnify.Shipments.Domain.Common;

public record Budget
{
    private Budget(Price price, bool isNegotiable)
    {
        Price = price;
        IsNegotiable = isNegotiable;
    }

    public Price Price { get; }

    public bool IsNegotiable { get; }

    public static Budget Create(decimal amount, CurrencyCode currencyCode, bool isNegotiable)
    {
        Price price = Price.Create(amount, currencyCode);

        return new Budget(price, isNegotiable);
    }

    public static Budget Create(Price price, bool isNegotiable)
    {
        if (price == null)
        {
            throw new ArgumentNullException(nameof(price));
        }

        return new Budget(price, isNegotiable);
    }

    public override string ToString()
    {
        return $"{Price.Amount} {Price.Currency}";
    }
}