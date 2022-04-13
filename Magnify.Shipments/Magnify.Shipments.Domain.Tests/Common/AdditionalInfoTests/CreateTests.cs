using System;
using Magnify.Shipments.Domain.Common;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.Common.AdditionalInfoTests;

[TestFixture]
public class CreateTests
{
    [Test]
    public void WhenTextIsEmpty_ShouldThrowArgumentException()
    {
        // arrange
        var text = string.Empty;

        // act
        TestDelegate action = () => AdditionalInfo.Create(text);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenTextIsValid_ShouldCreateAdditionalInfo()
    {
        // arrange
        var text = "text";

        // act
        var additionalInfo = AdditionalInfo.Create(text);

        // assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(additionalInfo);
            Assert.That(additionalInfo.Text, Is.EqualTo(text));
        });
    }
}