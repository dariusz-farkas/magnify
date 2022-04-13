namespace Magnify.Shipments.Domain.Common;

public sealed class Price
{
    private Price(decimal amount, CurrencyCode currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; }

    public CurrencyCode Currency { get; }

    public static Price Create(decimal amount, CurrencyCode currencyCode)
    {
        if (currencyCode == CurrencyCode.None)
        {
            throw new ArgumentException("Currency code must be specified.", nameof(currencyCode));
        }

        if (amount < 0)
        {
            throw new ArgumentException("Budget amount cannot be lower than 0.");
        }

        return new Price(amount, currencyCode);
    }
}