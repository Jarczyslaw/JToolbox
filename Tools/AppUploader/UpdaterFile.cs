using JToolbox.Misc.Serializers;
using System.Xml.Serialization;

namespace AppUploader
{
    [XmlRoot(elementName: "item")]
    public class UpdaterFile
    {
        [XmlElement(ElementName = "mandatory")]
        public string Mandatory { get; set; }

        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "version")]
        public string Version { get; set; }

        public void Serialize(string path)
        {
            var serializer = new SerializerXml();
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            serializer.ToFile(this, path, namespaces);
        }
    }
}