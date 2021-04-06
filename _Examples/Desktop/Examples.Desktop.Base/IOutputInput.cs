using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examples.Desktop.Base
{
    public interface IOutputInput
    {
        void Write(string message);

        void WriteLine(string message);

        void PutLine();

        string Read(string label, string text = null, Func<string, string> validationRule = null);

        T SelectValue<T>(string label, List<T> values);

        Task Wait();

        void Clear();

        void StartTime();

        void StopTime();
    }
}