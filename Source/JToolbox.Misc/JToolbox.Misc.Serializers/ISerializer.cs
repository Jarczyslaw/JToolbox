namespace JToolbox.Misc.Serializers
{
    public interface ISerializer
    {
        T FromBytes<T>(byte[] data) where T : class;

        T FromFile<T>(string filePath);

        T FromString<T>(string input);

        void PopulateFromFile<T>(string filePath, T @object);

        void PopulateFromString<T>(string input, T @object);

        void ToFile<T>(T obj, string filePath);

        string ToString<T>(T val);
    }
}