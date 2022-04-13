using System;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.OfferIdTests;

[TestFixture]
public class CreateTests
{
    [Test] public void WhenValueIsEmpty_ShouldThrowArgumentNullException()
    {
        // arrange
        var value = Guid.Empty;

        // act
        TestDelegate action = () => OfferId.Create(value);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenValidIsValid_ShouldCreateOfferId()
    {
        // arrange
        var value = Guid.NewGuid();

        // act
        var offerId = OfferId.Create(value);

        // assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(offerId);
            Assert.That(offerId.Value, Is.EqualTo(value));
        });
    }
}