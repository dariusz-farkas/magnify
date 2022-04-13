using System;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.ShipperTests;

[TestFixture]
public class CreateTests
{
    [Test]
    public void WhenNameIsEmpty_ShouldThrowArgumentException()
    {
        // arrange
        var name = string.Empty;

        // act
        TestDelegate action = () => Shipper.Create(name);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }
}