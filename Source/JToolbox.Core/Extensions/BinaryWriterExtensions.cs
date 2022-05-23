using System;
using System.Collections.Generic;
using System.IO;

namespace JToolbox.Core.Extensions
{
    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter writer, DateTime dateTime)
        {
            writer.Write(dateTime.Ticks);
        }

        public static void Write<T>(this BinaryWriter writer, List<T> list, Action<T> writerAction)
        {
            writer.Write(list.Count);
            foreach (var item in list)
            {
                writerAction(item);
            }
        }

        public static void Write<TKey, TValue>(this BinaryWriter writer, Dictionary<TKey, TValue> dictionary, Action<TKey> keyWriterAction, Action<TValue> valueWriterAction)
        {
            writer.Write(dictionary.Count);
            foreach (var pair in dictionary)
            {
                keyWriterAction(pair.Key);
                valueWriterAction(pair.Value);
            }
        }
    }
}