using System.IO;

namespace JToolbox.Misc.Serializers
{
    public interface ISerializer
    {
        T Deserialize<T>(byte[] data) where T : class;

        T Deserialize<T>(FileInfo file);

        T Deserialize<T>(string input);

        void Populate<T>(T @object, FileInfo file);

        void Populate<T>(T @object, string input);

        void Serialize<T>(T obj, FileInfo file);

        string Serialize<T>(T val);
    }
}