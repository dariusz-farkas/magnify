using AutoFixture;
using Magnify.Shipments.Domain.Common;

namespace Magnify.Shipments.Domain.Tests.Common.Builders;

public sealed class AddressBuilder
{
    private static readonly Fixture Fixture = new Fixture();
    private string _street;
    private string _city;
    private string _postalCode;
    private CountryCode _countryCode;

    public static AddressBuilder Create()
    {
        return new AddressBuilder();
    }

    private AddressBuilder()
    {
        _street = Fixture.Create<string>();
        _city = Fixture.Create<string>();
        _postalCode = Fixture.Create<string>();
        _countryCode = CountryCode.Usa;
    }

    public AddressBuilder Reset()
    {
        _street = Fixture.Create<string>();
        _city = Fixture.Create<string>();
        _postalCode = Fixture.Create<string>();
        _countryCode = CountryCode.Usa;

        return this;
    }

    public AddressBuilder WithStreet(string street)
    {
        _street = street;
        return this;
    }

    public AddressBuilder WithCity(string city)
    {
        _street = city;
        return this;
    }

    public AddressBuilder WithPostalCode(string postalCode)
    {
        _postalCode = postalCode;
        return this;
    }

    public AddressBuilder WithCountryCode(CountryCode countryCode)
    {
        _countryCode = countryCode;
        return this;
    }

    public Address Build()
    {
        var address = Address.Create(_street, _city, _postalCode, _countryCode);
        Reset();
        return address;
    }
}