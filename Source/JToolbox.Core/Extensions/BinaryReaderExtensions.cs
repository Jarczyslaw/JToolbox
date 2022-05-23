using System;
using System.Collections.Generic;
using System.IO;

namespace JToolbox.Core.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static DateTime ReadDateTime(this BinaryReader reader)
        {
            return new DateTime(reader.ReadInt64());
        }

        public static Dictionary<TKey, TValue> ReadDictionary<TKey, TValue>(this BinaryReader reader, Func<TKey> keyReaderFunc, Func<TValue> valueReaderFunc)
        {
            var dictSize = reader.ReadInt32();
            var result = new Dictionary<TKey, TValue>();
            for (int i = 0; i < dictSize; i++)
            {
                var key = keyReaderFunc();
                result.Add(key, valueReaderFunc());
            }
            return result;
        }

        public static List<T> ReadList<T>(this BinaryReader reader, Func<T> readerFunc)
        {
            var listSize = reader.ReadInt32();
            var result = new List<T>();
            for (int i = 0; i < listSize; i++)
            {
                result.Add(readerFunc());
            }
            return result;
        }
    }
}