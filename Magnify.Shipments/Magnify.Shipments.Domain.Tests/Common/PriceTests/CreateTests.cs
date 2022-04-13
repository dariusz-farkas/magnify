using System;
using Magnify.Shipments.Domain.Common;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.Common.PriceTests;

[TestFixture]
public class CreateTests
{
    [Test] 
    public void WhenPriceIsLowerThanZero_ShouldThrowArgumentException()
    {
        // arrange
        decimal amount = -20;

        // act
        TestDelegate action = () => Price.Create(amount, CurrencyCode.Usd);

        // assert
        Assert.Throws<ArgumentException>(action, "Budget amount cannot be lower than 0.");
    }

    [Test]
    public void WhenCurrencyCodeIsNotSpecified_ShouldThrowArgumentException()
    {
        // arrange
        CurrencyCode currencyCode = CurrencyCode.None;

        // act
        TestDelegate action = () => Price.Create(1000, currencyCode);

        // assert
        Assert.Throws<ArgumentException>(action, "Currency code must be specified.");
    }

    [Test]
    public void WhenDataIsValid_ShouldCreatePrice()
    {
        // arrange
        CurrencyCode currencyCode = CurrencyCode.Usd;
        decimal amount = 1000;

        // act
        var budget = Price.Create(amount, currencyCode);

        // assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(budget);
            Assert.That(budget.Amount, Is.EqualTo(amount));
            Assert.That(budget.Currency, Is.EqualTo(currencyCode));
        });
    }
}