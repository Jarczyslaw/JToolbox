namespace JToolbox.Misc.Serializers
{
    public interface ISerializer
    {
        T FromBytes<T>(byte[] data) where T : class;

        T FromFile<T>(string filePath);

        T FromString<T>(string input);

        void ToFile<T>(T obj, string filePath);

        string ToString<T>(T val);
    }
}