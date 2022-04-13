using Magnify.Shipments.Domain.Tests.Common.Builders;
using Magnify.Shipments.Framework.Exceptions;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.OfferTests;

[TestFixture]
public class AcceptTests
{
    [Test]
    public void WhenOfferStateIsRejected_ShouldThrowBusinessException()
    {
        // arrange
        var offer = OfferBuilder.Create().Build();
        offer.Reject();

        // act
        TestDelegate action = () => offer.Accept();

        // assert
        Assert.Throws<BusinessException>(action, "Cannot accept an offer.");
    }

    [Test]
    public void WhenOfferStateIsAccepted_ShouldThrowBusinessException()
    {
        // arrange
        var offer = OfferBuilder.Create().Build();
        offer.Accept();

        // act
        TestDelegate action = () => offer.Accept();

        // assert
        Assert.Throws<BusinessException>(action, "Cannot accept an offer.");
    }

    [Test]
    public void WhenOfferStateIsTerminated_ShouldThrowBusinessException()
    {
        // arrange
        var offer = OfferBuilder.Create().Build();
        offer.Terminate(offer.Carrier);

        // act
        TestDelegate action = () => offer.Accept();

        // assert
        Assert.Throws<BusinessException>(action, "Cannot accept an offer.");
    }

    [Test]
    public void WhenSuccessful_ShouldAccept()
    {
        // arrange
        var offer = OfferBuilder.Create().Build();

        // act
        offer.Accept();

        // assert
        Assert.That(offer.State, Is.EqualTo(OfferState.Accepted));
    }
}