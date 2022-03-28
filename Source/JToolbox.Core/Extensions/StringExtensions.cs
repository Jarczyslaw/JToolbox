using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace JToolbox.Core.Extensions
{
    public static class StringExtensions
    {
        public static string AsValidFileName(this string @string, string replaceChar = "_")
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return Regex.Replace(@string, invalidRegStr, replaceChar);
        }

        public static string ExtractBetween(this string @string, string from, string to)
        {
            if (@string == null)
            {
                return null;
            }

            var indexFrom = @string.IndexOf(from);
            if (indexFrom < 0)
            {
                return string.Empty;
            }
            indexFrom += from.Length;

            var indexTo = @string.LastIndexOf(to);
            if (indexTo < 0)
            {
                return string.Empty;
            }

            return @string.Substring(indexFrom, indexTo - indexFrom);
        }

        public static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return input[0].ToString().ToUpper() + input.Substring(1);
        }

        public static bool IgnoreCaseContains(this string val1, string val2)
        {
            if (val1 == null || val2 == null)
            {
                return false;
            }
            return val1.IndexOf(val2, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static bool IgnoreCaseEquals(this string val1, string val2)
        {
            if (val1 == null || val2 == null)
            {
                return false;
            }
            return val1.Equals(val2, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsNullOrEmpty(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }

        public static bool IsNullOrWhiteSpace(this string @this)
        {
            return string.IsNullOrWhiteSpace(@this);
        }

        public static bool IsValidFileName(this string @this)
        {
            return !string.IsNullOrEmpty(@this) && @this.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
        }

        public static string OnlyDigits(this string @this)
        {
            if (@this == null)
            {
                return null;
            }

            var result = string.Empty;
            for (int i = 0; i < @this.Length; i++)
            {
                if (char.IsDigit(@this[i]))
                {
                    result += @this[i];
                }
            }
            return result;
        }

        public static string SafeTrim(this string @this)
        {
            if (@this == null)
            {
                return null;
            }
            return @this.Trim();
        }

        public static string Truncate(this string value, int length)
        {
            if (value == null)
            {
                return null;
            }
            return value.Substring(0, Math.Min(value.Length, length));
        }

        public static string WithoutWhitespaces(this string @this)
        {
            if (@this == null)
            {
                return null;
            }
            return new string(@this.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }
    }
}