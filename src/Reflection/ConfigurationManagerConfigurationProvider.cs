using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Reflection
{
    public interface IConfigurationManagerConfigurationProvider : IConfigurationProvider
    {
    }

    public class ConfigurationManagerConfigurationProvider : IConfigurationManagerConfigurationProvider
    {
        private const string AppSettingsPath = "..\\..\\..\\appsettings.json";
        private readonly IConfiguration _configuration;

        public ConfigurationManagerConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object LoadSettings(Type propertyType, ConfigurationItem configurationItemAttribute)
        {
            if (Guard.IsAvailableForStorage(propertyType))
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                var json = File.ReadAllText(filePath);

                if (!string.IsNullOrEmpty(json))
                {
                    dynamic settings = JsonConvert.DeserializeObject(json);
                    string setting = settings[configurationItemAttribute.SettingName].Value;
                    return setting.TryConvertToPropertyType(propertyType);
                }

                throw new ReflectionException(string.Format("Setting {0} is not specified", configurationItemAttribute.SettingName));
            }

            throw new ArgumentException("Provided property is not string, int, float or TimeSpan");
        }

        public void SaveSettings(object value, ConfigurationItem configurationItemAttribute)
        {
            if (Guard.IsAvailableForStorage(value))
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                var json = File.ReadAllText(filePath);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);
                jsonObj[configurationItemAttribute.SettingName] = value.ToString();

                var output = JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
                File.WriteAllText(AppSettingsPath, output);
                return;
            }

            throw new ArgumentException("Provided property is not string, int, float or TimeSpan");
        }
    }
}
