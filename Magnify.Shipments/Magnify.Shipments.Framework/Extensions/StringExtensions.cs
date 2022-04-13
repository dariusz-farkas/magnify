namespace Magnify.Shipments.Framework.Extensions;

public static class StringExtensions
{
    public static void ThrowWhenNullOrEmpty(this string? stringValue, string paramName)
    {
        if (string.IsNullOrEmpty(stringValue))
        {
            throw new ArgumentNullException(paramName);
        }
    }
}