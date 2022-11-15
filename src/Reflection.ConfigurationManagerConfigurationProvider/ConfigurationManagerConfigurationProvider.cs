using Microsoft.Extensions.Configuration;
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
            var config = _configuration[configurationItemAttribute.SettingName];

            if (!string.IsNullOrEmpty(config))
            {
                return config.TryConvertToPropertyType(propertyType);
            }

            throw new ReflectionException(string.Format("Setting {0} is not specified", configurationItemAttribute.SettingName));
        }

        public void SaveSettings(object value, ConfigurationItem configurationItemAttribute)
        {
            _configuration[configurationItemAttribute.SettingName] = value.ToString();
        }
    }
}