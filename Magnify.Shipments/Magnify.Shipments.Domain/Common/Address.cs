using Magnify.Shipments.Framework.Extensions;

namespace Magnify.Shipments.Domain.Common;

public record Address
{
    private Address(string street, string city, string postalCode, CountryCode countryCode)
    {
        Street = street;
        City = city;
        PostalCode = postalCode;
        CountryCode = countryCode;
    }

    public static Address Create(string street, string city, string postalCode, CountryCode countryCode)
    {
        street.ThrowWhenNullOrEmpty(nameof(street));
        city.ThrowWhenNullOrEmpty(nameof(city));
        postalCode.ThrowWhenNullOrEmpty(nameof(postalCode));

        if (countryCode == CountryCode.None)
        {
            throw new ArgumentException("CountryCode must be specified.", nameof(countryCode));
        }

        return new Address(street, city, postalCode, countryCode);
    }

    public override string ToString() => string.Join(Environment.NewLine, Street, City, PostalCode, CountryCode);

    public string Street { get; }

    public string City { get; }

    public string PostalCode { get; }

    public CountryCode CountryCode { get; }
}