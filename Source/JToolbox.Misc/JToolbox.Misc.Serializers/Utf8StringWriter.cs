using System.IO;
using System.Text;

namespace JToolbox.Misc.Serializers
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}