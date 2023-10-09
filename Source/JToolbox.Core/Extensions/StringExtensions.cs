using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public static List<int> IndexesOf(this string @this, string value)
        {
            var result = new List<int>();

            if (string.IsNullOrEmpty(@this) || string.IsNullOrEmpty(value)) { return result; }

            var startIndex = 0;
            while (true)
            {
                int index = @this.IndexOf(value, startIndex);
                if (index < 0) { return result; }

                result.Add(index);
                startIndex = index + value.Length;

                if (startIndex >= @this.Length) { return result; }
            }
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

        public static string RemoveTaggedContent(this string @string, string startTag, string endTag, List<string> removedContent = null)
        {
            if (string.IsNullOrEmpty(@string)
                || string.IsNullOrEmpty(startTag)
                || string.IsNullOrEmpty(endTag)) { return @string; }

            while (@string.Length > 0)
            {
                int startIndex = @string.IndexOf(startTag);
                if (startIndex < 0) { break; }

                int endIndex = @string.IndexOf(endTag, startIndex + startTag.Length);
                if (endIndex < 0) { break; }

                if (removedContent != null)
                {
                    int startIndexWithLength = startIndex + startTag.Length;
                    string contentToRemove = @string.Substring(startIndexWithLength, endIndex - startIndexWithLength);

                    if (!string.IsNullOrEmpty(contentToRemove))
                    {
                        removedContent.Add(contentToRemove);
                    }
                }

                endIndex += endTag.Length;

                @string = @string.Remove(startIndex, endIndex - startIndex);
            }

            return @string;
        }

        public static string ReplaceMany(this string @this, Dictionary<string, string> toReplace)
        {
            if (string.IsNullOrEmpty(@this)) { return @this; }

            var sb = new StringBuilder(@this);
            foreach (KeyValuePair<string, string> pair in toReplace)
            {
                sb.Replace(pair.Key, pair.Value);
            }
            return sb.ToString();
        }

        public static string SafeTrim(this string @this)
        {
            if (@this == null)
            {
                return null;
            }
            return @this.Trim();
        }

        public static string SmartReplaceMany(this string @this, Dictionary<string, string> toReplace)
        {
            if (string.IsNullOrEmpty(@this) || toReplace == null || toReplace.Count == 0) { return @this; }

            var replaceEntries = new List<ReplaceEntry>();
            foreach (KeyValuePair<string, string> pair in toReplace)
            {
                List<int> indexes = @this.IndexesOf(pair.Key);
                if (indexes.Count == 0) { continue; }

                foreach (int index in indexes)
                {
                    var replaceEntry = new ReplaceEntry
                    {
                        Index = index,
                        OldValue = pair.Key,
                        NewValue = pair.Value
                    };

                    if (!replaceEntries.Any(x => x.IsOverlapping(replaceEntry)))
                    {
                        replaceEntries.Add(replaceEntry);
                    }
                }
            }

            if (replaceEntries.Count == 0) { return @this; }

            List<ReplaceEntry> orderedReplaceEntries = replaceEntries.OrderBy(x => x.Index)
                .ToList();

            var sb = new StringBuilder(@this);
            for (int i = 0; i < orderedReplaceEntries.Count; i++)
            {
                ReplaceEntry replaceEntry = orderedReplaceEntries[i];
                sb.Remove(replaceEntry.Index, replaceEntry.OldValue.Length);
                sb.Insert(replaceEntry.Index, replaceEntry.NewValue);

                for (int j = i + 1; j < orderedReplaceEntries.Count; j++)
                {
                    orderedReplaceEntries[j].Index += replaceEntry.LengthDifference;
                }
            }
            return sb.ToString();
        }

        public static Stream ToStream(this string @this)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(@this);
            writer.Flush();
            stream.Position = 0;
            return stream;
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

        private class ReplaceEntry
        {
            public int Index { get; set; }

            public int LengthDifference => NewValue.Length - OldValue.Length;

            public string NewValue { get; set; }

            public string OldValue { get; set; }

            public bool IsOverlapping(ReplaceEntry other)
            {
                int start = Index;
                int end = Index + OldValue.Length - 1;

                int otherStart = other.Index;
                int otherEnd = other.Index + other.OldValue.Length - 1;

                return end >= otherStart && start <= otherEnd;
            }
        }
    }
}