using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace JToolbox.Misc.Serializers
{
    public class SerializerJson : ISerializer
    {
        public T FromBytes<T>(byte[] data) where T : class
        {
            using (var stream = new MemoryStream(data))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return JsonSerializer.Create().Deserialize(reader, typeof(T)) as T;
                }
            }
        }

        public T FromFile<T>(string filePath)
        {
            var serialized = File.ReadAllText(filePath);
            return FromString<T>(serialized);
        }

        public T FromString<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public void PopulateFromFile<T>(string filePath, T @object)
        {
            var serialized = File.ReadAllText(filePath);
            JsonConvert.PopulateObject(serialized, @object);
        }

        public void PopulateFromString<T>(string input, T @object)
        {
            JsonConvert.PopulateObject(input, @object);
        }

        public void ToFile<T>(T obj, string filePath)
        {
            var serialized = ToString(obj);
            File.WriteAllText(filePath, serialized);
        }

        public string ToString<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}