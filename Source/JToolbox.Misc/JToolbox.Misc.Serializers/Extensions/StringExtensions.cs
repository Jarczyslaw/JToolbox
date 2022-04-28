namespace JToolbox.Misc.Serializers.Extensions
{
    public static class StringExtensions
    {
        private static readonly SerializerJson serializerJson = new SerializerJson();
        private static readonly SerializerXml serializerXml = new SerializerXml();

        public static T FromJson<T>(this string @this)
        {
            return serializerJson.FromString<T>(@this);
        }

        public static T FromXml<T>(this string @this)
        {
            return serializerXml.FromString<T>(@this);
        }
    }
}