using System;
using Magnify.Shipments.Domain.Common;
using Magnify.Shipments.Domain.Tests.Common.Builders;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.Common.BudgetTests;

[TestFixture]
public class CreateTests
{
    [Test] public void WhenPriceIsNull_ShouldThrowArgumentNullException()
    {
        // arrange
        Price price = default!;

        // act
        TestDelegate action = () => Budget.Create(price, true);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenPriceIsValid_ShouldCreateBudget()
    {
        // arrange
        var price = PriceBuilder.Create().Build();

        // act
        var budget = Budget.Create(price, true);

        // assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(budget);
            Assert.That(budget.Price, Is.EqualTo(price));
        });
    }
}