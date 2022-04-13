using AutoFixture;
using Magnify.Shipments.Domain.Common;

namespace Magnify.Shipments.Domain.Tests.Common.Builders;

public sealed class PriceBuilder
{
    private static readonly Fixture Fixture;
    private decimal _amount;
    private CurrencyCode _currencyCode;

    static PriceBuilder()
    {
        Fixture = new Fixture();
        Fixture.Customizations.Add(new RandomNumericSequenceGenerator(1000, 10000));
    }

    public static PriceBuilder Create()
    {
        return new PriceBuilder();
    }

    private PriceBuilder()
    {
        _amount = Fixture.Create<decimal>();
        _currencyCode = CurrencyCode.Usd;
    }

    public PriceBuilder Reset()
    {
        _amount = Fixture.Create<decimal>();
        _currencyCode = CurrencyCode.Usd;

        return this;
    }

    public PriceBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public PriceBuilder WithCurrencyCode(CurrencyCode currencyCode)
    {
        _currencyCode = currencyCode;
        return this;
    }

    public Price Build()
    {
        var price = Price.Create(_amount, _currencyCode);
        Reset();
        return price;
    }
}