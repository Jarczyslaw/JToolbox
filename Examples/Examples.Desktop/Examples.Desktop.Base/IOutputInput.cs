using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examples.Desktop.Base
{
    public interface IOutputInput
    {
        void Clear();

        void PutLine();

        string Read(string label, string text = null, Func<string, string> validationRule = null);

        string SelectDirectory(string message);

        T SelectValue<T>(string label, List<T> values);

        void StartTime();

        void StopTime();

        Task Wait();

        void Write(string message);

        void WriteLine(string message);
    }
}