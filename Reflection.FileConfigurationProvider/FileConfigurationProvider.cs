
using Microsoft.Extensions.Configuration;
using Reflection.Models;

namespace Reflection.FileConfigurationProvider
{
    public class FileConfigurationProvider : IFileConfigurationProvider
    {
        private readonly IConfiguration _configuration;

        public FileConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object LoadSettings(Type propertyType, ConfigurationItem configurationItemAttribute)
        {
            if (Guard.IsAvailableForStorage(propertyType))
            {
                var path = TryGetFilePath();
                string[] lines = File.ReadAllLines(path);
                var dict = lines.Select(line => line.Split('=')).ToDictionary(split => split[0], split => split[1]);
                var config = dict[configurationItemAttribute.SettingName];
                if (!string.IsNullOrEmpty(config))
                {
                    return config.TryConvertToPropertyType(propertyType);
                }

                throw new ReflectionException(string.Format("Setting {0} is not specified", configurationItemAttribute.SettingName));
            }

            throw new ArgumentException("Provided property is not string, int, float or TimeSpan");
        }

        public void SaveSettings(object value, ConfigurationItem configurationItemAttribute)
        {
            if (Guard.IsAvailableForStorage(value))
            {
                var path = TryGetFilePath();
                string[] lines = File.ReadAllLines(path);
                var dict = lines.Select(line => line.Split('=')).ToDictionary(split => split[0], split => split[1]);

                if (dict.ContainsKey(configurationItemAttribute.SettingName))
                {
                    dict[configurationItemAttribute.SettingName] = value.ToString();

                    var newLines = new List<string>();

                    foreach (var pair in dict)
                    {
                        newLines.Add($"{pair.Key}={pair.Value}");
                    }

                    File.WriteAllLines(path, newLines);
                }

                return;
            }

            throw new ArgumentException("Provided property is not string, int, float or TimeSpan");
        }

        private string TryGetFilePath()
        {
            var filePath = _configuration["FilePath"];

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ReflectionException("Custom file is not specified");
            }

            return filePath;
        }
    }
}