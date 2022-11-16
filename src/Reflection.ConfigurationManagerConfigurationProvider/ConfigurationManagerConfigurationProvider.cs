using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Reflection.Models;

namespace Reflection.ConfigurationManagerConfigurationProvider
{
    public class ConfigurationManagerConfigurationProvider : IConfigurationManagerConfigurationProvider
    {
        private readonly IConfiguration _configuration;

        public ConfigurationManagerConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object LoadSettings(Type propertyType, ConfigurationItem configurationItemAttribute)
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

        public void SaveSettings(object value, ConfigurationItem configurationItemAttribute)
        {
            var settingsPath = _configuration["SettingsPath"];

            var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

            Console.WriteLine(filePath);
            var json = File.ReadAllText(filePath);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj[configurationItemAttribute.SettingName] = value.ToString();

            var output = JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, output);
            File.WriteAllText(settingsPath, output);        }
    }
}