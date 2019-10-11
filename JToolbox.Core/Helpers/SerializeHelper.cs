using System.IO;
using System.Xml.Serialization;

namespace JToolbox.Core.Helpers
{
    public static class SerializeHelper
    {
        public static string ToXml<T>(T val)
        {
            var s = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                s.Serialize(writer, val);
                return writer.ToString();
            }
        }

        public static T FromXml<T>(string input)
        {
            var ser = new XmlSerializer(typeof(T));

            using (var sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }
    }
}