using System;

namespace MediaToolkit.Util
{
    public static class Extensions
    {
        internal static bool IsNullOrWhiteSpace(this string value)
            => string.IsNullOrEmpty(value) || value.Trim().Length == 0;

        internal static string Remove(this Enum enumerable, string text)
            => enumerable.ToString().Replace(text, "");

        internal static string ToLower(this Enum enumerable)
            => enumerable.ToString().ToLowerInvariant();
    }
}