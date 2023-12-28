using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace JToolbox.Misc.Serializers
{
    public class SerializerJson : ISerializer
    {
        private readonly Formatting formatting;

        public SerializerJson(Formatting formatting = Formatting.Indented)
        {
            this.formatting = formatting;
        }

        public T Deserialize<T>(byte[] data) where T : class
        {
            using (var stream = new MemoryStream(data))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return JsonSerializer.Create().Deserialize(reader, typeof(T)) as T;
                }
            }
        }

        public T Deserialize<T>(FileInfo file)
        {
            var serialized = File.ReadAllText(file.FullName);
            return Deserialize<T>(serialized);
        }

        public T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public void Populate<T>(T @object, FileInfo file)
        {
            var serialized = File.ReadAllText(file.FullName);
            JsonConvert.PopulateObject(serialized, @object);
        }

        public void Populate<T>(T @object, string input)
        {
            JsonConvert.PopulateObject(input, @object);
        }

        public void Serialize<T>(T obj, FileInfo file)
        {
            var serialized = Serialize(obj);
            File.WriteAllText(file.FullName, serialized);
        }

        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }
    }
}