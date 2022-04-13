using Magnify.Shipments.Framework.Extensions;

namespace Magnify.Shipments.Domain.Common;

public record AdditionalInfo
{
    private AdditionalInfo(string text)
    {
        Text = text;
    }

    public string Text { get; }

    public static AdditionalInfo Create(string text)
    {
        text.ThrowWhenNullOrEmpty(nameof(text));

        return new AdditionalInfo(text);
    }
}