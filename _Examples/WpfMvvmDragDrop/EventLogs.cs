using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WpfMvvmDragDrop
{
    public static class EventLogs
    {
        public static Action<string> AddEvent;
        private static int counter = 1;

        public static void Add(string log)
        {
            AddEvent?.Invoke($"{counter}. {log}");
            counter++;
        }

        public static void AddWithClassName(string log)
        {
            var mth = new StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            Add($"{cls}: {log}");
        }
    }
}