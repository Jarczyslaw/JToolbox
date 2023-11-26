using System.Collections.Generic;
using System.Dynamic;

namespace JToolbox.Core.Extensions
{
    public static class ExpandoObjectExtensions
    {
        public static void AddProperty(this ExpandoObject @this, string propertyName, object propertyValue)
        {
            var expandoDict = @this as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
            {
                expandoDict[propertyName] = propertyValue;
            }
            else
            {
                expandoDict.Add(propertyName, propertyValue);
            }
        }
    }
}