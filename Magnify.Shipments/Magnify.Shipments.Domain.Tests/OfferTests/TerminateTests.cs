using Magnify.Shipments.Domain.Tests.Common.Builders;
using Magnify.Shipments.Framework.Exceptions;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.OfferTests;

[TestFixture]
public class TerminateTests
{
    [Test]
    public void WhenOfferStateIsRejected_ShouldThrowBusinessException()
    {
        // arrange
        var offer = OfferBuilder.Create().Build();
        offer.Reject();

        // act
        TestDelegate action = () => offer.Terminate(offer.Carrier);

        // assert
        Assert.Throws<BusinessException>(action, "Cannot terminate an offer.");
    }

    [Test]
    public void WhenOfferStateIsAccepted_ShouldThrowBusinessException()
    {
        // arrange
        var offer = OfferBuilder.Create().Build();
        offer.Accept();

        // act
        TestDelegate action = () => offer.Terminate(offer.Carrier);

        // assert
        Assert.Throws<BusinessException>(action, "Cannot terminate an offer.");
    }

    [Test]
    public void WhenOfferStateIsTerminated_ShouldThrowBusinessException()
    {
        // arrange
        var offer = OfferBuilder.Create().Build();
        offer.Terminate(offer.Carrier);

        // act
        TestDelegate action = () => offer.Terminate(offer.Carrier);

        // assert
        Assert.Throws<BusinessException>(action, "Cannot terminate an offer.");
    }

    [Test]
    public void WhenUserIsNotTheOfferOwner_ShouldThrowBusinessException()
    {
        // arrange
        var offer = OfferBuilder.Create().Build();
        var carrier = CarrierBuilder.Create().Build();

        // act
        TestDelegate action = () => offer.Terminate(carrier);

        // assert
        Assert.Throws<BusinessException>(action, "User is not the carrier of the offer.");
    }

    [Test]
    public void WhenSuccessful_ShouldTerminate()
    {
        // arrange
        var offer = OfferBuilder.Create().Build();

        // act
        offer.Terminate(offer.Carrier);

        // assert
        Assert.That(offer.State, Is.EqualTo(OfferState.Terminated));
    }
}