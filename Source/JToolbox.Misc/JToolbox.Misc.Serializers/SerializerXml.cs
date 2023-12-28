using System;
using System.IO;
using System.Xml.Serialization;

namespace JToolbox.Misc.Serializers
{
    public class SerializerXml : ISerializer
    {
        public T Deserialize<T>(byte[] data) where T : class
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(FileInfo file)
        {
            var serialized = File.ReadAllText(file.FullName);
            return Deserialize<T>(serialized);
        }

        public T Deserialize<T>(string input)
        {
            var ser = new XmlSerializer(typeof(T));

            using (var sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public void Populate<T>(T @object, FileInfo file)
        {
            throw new NotImplementedException();
        }

        public void Populate<T>(T @object, string input)
        {
            throw new NotImplementedException();
        }

        public void Serialize<T>(T @object, FileInfo file)
        {
            Serialize(@object, file, null);
        }

        public void Serialize<T>(T @object, FileInfo file, XmlSerializerNamespaces namespaces)
        {
            var serialized = Serialize(@object, namespaces);
            File.WriteAllText(file.FullName, serialized);
        }

        public string Serialize<T>(T val)
        {
            return Serialize(val, (XmlSerializerNamespaces)null);
        }

        public string Serialize<T>(T val, XmlSerializerNamespaces namespaces)
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