using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JToolbox.Misc.Configuration
{
    public abstract class BaseConfiguration
    {
        private readonly Lazy<IConfigurationRoot> configurationRoot;

        protected BaseConfiguration()
        {
            configurationRoot = new Lazy<IConfigurationRoot>(GetConfigurationRoot, true);
        }

        protected IConfigurationRoot ConfigurationRoot => configurationRoot.Value;

        protected abstract string GetConfigurationFilePath { get; }

        protected bool GetBool(string section, bool defaultValue = default)
            => GetValue(section, x => bool.TryParse(x, out bool result) ? result : defaultValue);

        protected IConfigurationRoot GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile(GetConfigurationFilePath)
                .Build();
        }

        protected Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string section)
        {
            return ConfigurationRoot
                .GetSection(section)
                .Get<Dictionary<TKey, TValue>>();
        }

        protected double GetDouble(string section, double defaultValue = default)
            => GetValue(section, x => double.TryParse(x, out double result) ? result : defaultValue);

        protected int GetInt(string section, int defaultValue = default)
            => GetValue(section, x => int.TryParse(x, out int result) ? result : defaultValue);

        protected List<T> GetList<T>(string section)
        {
            return ConfigurationRoot
                .GetSection(section)
                .Get<List<T>>();
        }

        protected string GetValue(string section) => ConfigurationRoot.GetSection(section).Value;

        protected T GetValue<T>(string section, Func<string, T> valueFunc) => valueFunc(GetValue(section));

        protected void SetValue(Action<dynamic> callback)
        {
            var json = File.ReadAllText(GetConfigurationFilePath);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            callback(jsonObj);
            var newJson = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(GetConfigurationFilePath, newJson);
        }
    }
}