using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HappyLittleHelpers.Extensions
{
    public static class ConditionalExtensions
    {
        public static bool IsIn<T>(this T item, params T[] lookup) => lookup.Any(a => a.Equals(item));
        public static bool IsIn<T>(this T item, IEnumerable<T> lookup) => lookup.Any(a => a.Equals(item));

        public static bool IsIn(this string item, StringComparison stringComparison = StringComparison.Ordinal,
            params string[] lookup) => lookup.Any(a => a.Equals(item, stringComparison));

        public static bool IsIn(this string item, IEnumerable<string> lookup,
            StringComparison stringComparison = StringComparison.Ordinal) =>
            lookup.Any(a => a.Equals(item, stringComparison));

    }
}