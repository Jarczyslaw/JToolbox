using System.IO;
using System.Xml.Serialization;

namespace JToolbox.Misc.Serializers
{
    public class SerializerXml : ISerializer
    {
        public T FromBytes<T>(byte[] data) where T : class
        {
            throw new System.NotImplementedException();
        }

        public T FromFile<T>(string filePath)
        {
            var serialized = File.ReadAllText(filePath);
            return FromString<T>(serialized);
        }

        public T FromString<T>(string input)
        {
            var ser = new XmlSerializer(typeof(T));

            using (var sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public void ToFile<T>(T obj, string filePath)
        {
            ToFile(obj, filePath, null);
        }

        public void ToFile<T>(T obj, string filePath, XmlSerializerNamespaces namespaces)
        {
            var serialized = ToString(obj, namespaces);
            File.WriteAllText(filePath, serialized);
        }

        public string ToString<T>(T val)
        {
            return ToString(val, null);
        }

        public string ToString<T>(T val, XmlSerializerNamespaces namespaces)
        {
            var s = new XmlSerializer(typeof(T));
            using (var writer = new Utf8StringWriter())
            {
                s.Serialize(writer, val, namespaces);
                return writer.ToString();
            }
        }
    }
}