using System;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.CarrierTests;

[TestFixture]
public class CreateTests
{
    [Test]
    public void WhenNameIsEmpty_ShouldThrowArgumentException()
    {
        // arrange
        var name = string.Empty;

        // act
        TestDelegate action = () => Carrier.Create(name);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }
}