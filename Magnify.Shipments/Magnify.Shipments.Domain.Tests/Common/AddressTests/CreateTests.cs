using System;
using Magnify.Shipments.Domain.Common;
using NUnit.Framework;

namespace Magnify.Shipments.Domain.Tests.Common.AddressTests;

[TestFixture]
public class CreateTests
{
    [Test]
    public void WhenStreetIsEmpty_ShouldThrowArgumentNullException()
    {
        // arrange
        string street = default!;
        var city = "city";
        var postalCode = "code";
        var countryCode = CountryCode.Usa;

        // act
        TestDelegate action = () => Address.Create(street, city, postalCode, countryCode);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenCityIsEmpty_ShouldThrowArgumentNullException()
    {
        // arrange
        var street = "street";
        string city = default!;
        var postalCode = "code";
        var countryCode = CountryCode.Usa;

        // act
        TestDelegate action = () => Address.Create(street, city, postalCode, countryCode);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenPostalCodeIsEmpty_ShouldThrowArgumentNullException()
    {
        // arrange
        var street = "street";
        var city = "city";
        string postalCode = default!;
        var countryCode = CountryCode.Usa;

        // act
        TestDelegate action = () => Address.Create(street, city, postalCode, countryCode);

        // assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void WhenCountryCodeNotSpecified_ShouldThrowArgumentException()
    {
        // arrange
        var street = "street";
        var city = "city";
        var postalCode = "code";
        var countryCode = CountryCode.None;

        // act
        TestDelegate action = () => Address.Create(street, city, postalCode, countryCode);

        // assert
        Assert.Throws<ArgumentException>(action, "CountryCode must be specified.");
    }

    [Test]
    public void WhenDataIsValid_ShouldCreateAddress()
    {
        // arrange
        var street = "street";
        var city = "city";
        var postalCode = "code";
        var countryCode = CountryCode.Usa;

        // act
        var address = Address.Create(street, city, postalCode, countryCode);

        // assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(address);
            Assert.That(address.Street, Is.EqualTo(street));
            Assert.That(address.City, Is.EqualTo(city));
            Assert.That(address.PostalCode, Is.EqualTo(postalCode));
            Assert.That(address.CountryCode, Is.EqualTo(countryCode));
        });
    }
}