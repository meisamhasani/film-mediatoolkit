using System;
using System.Collections.Generic;

namespace MediaToolkit.Util
{
    public static class Extensions
    {
        private const int BUFF_SIZE = 16 * 1024;

        internal static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrEmpty(value) || value.Trim()
                .Length == 0;
        }

        internal static string Remove(this Enum enumerable, string text)
        {
            return enumerable.ToString()
                .Replace(text, "");
        }

        internal static string ToLower(this Enum enumerable)
        {
            return enumerable.ToString()
                .ToLowerInvariant();
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (action == null) throw new ArgumentNullException("action");

            foreach (T t in collection) action(t);
        }
    }
}