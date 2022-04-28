namespace JToolbox.Misc.Serializers.Extensions
{
    public static class ObjectExtensions
    {
        private static readonly SerializerJson serializerJson = new SerializerJson();
        private static readonly SerializerXml serializerXml = new SerializerXml();

        public static string ToJson(this object @this)
        {
            if (@this == null) { return null; }

            return serializerJson.ToString(@this);
        }

        public static string ToXml(this object @this)
        {
            if (@this == null) { return null; }

            return serializerXml.ToString(@this);
        }
    }
}