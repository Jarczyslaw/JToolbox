namespace Examples.Desktop.Base
{
    public interface IOutputInput
    {
        void Write(string message);

        void WriteLine(string message);

        string Read(string label, string text = null);

        void Wait(string message);

        void Clear();
    }
}