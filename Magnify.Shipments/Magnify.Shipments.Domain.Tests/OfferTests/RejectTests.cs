using Magnify.Shipments.Domain.Tests.Common.Builders;
using Magnify.Shipments.Framework.Exceptions;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.OfferTests;

[TestFixture]
public class RejectTests
{
    [Test]
    public void WhenOfferStateIsRejected_ShouldThrowBusinessException()
    {
        // arrange
        var offer = OfferBuilder.Create().Build();
        offer.Reject();

        // act
        TestDelegate action = () => offer.Reject();

        // assert
        Assert.Throws<BusinessException>(action, "Cannot reject an offer.");
    }

    [Test]
    public void WhenOfferStateIsAccepted_ShouldThrowBusinessException()
    {
        // arrange
        var offer = OfferBuilder.Create().Build();
        offer.Accept();

        // act
        TestDelegate action = () => offer.Reject();

        // assert
        Assert.Throws<BusinessException>(action, "Cannot reject an offer.");
    }

    [Test]
    public void WhenOfferStateIsTerminated_ShouldThrowBusinessException()
    {
        // arrange
        var offer = OfferBuilder.Create().Build();
        offer.Terminate(offer.Carrier);

        // act
        TestDelegate action = () => offer.Reject();

        // assert
        Assert.Throws<BusinessException>(action, "Cannot reject an offer.");
    }

    [Test]
    public void WhenSuccessful_ShouldReject()
    {
        // arrange
        var offer = OfferBuilder.Create().Build();

        // act
        offer.Reject();

        // assert
        Assert.That(offer.State, Is.EqualTo(OfferState.Rejected));
    }
}