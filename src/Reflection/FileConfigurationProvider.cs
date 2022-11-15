using Microsoft.Extensions.Configuration;

namespace Reflection
{
    public interface IFileConfigurationProvider : IConfigurationProvider
    {
    }

    public class FileConfigurationProvider : IFileConfigurationProvider
    {
        private readonly IConfiguration _configuration;

        public FileConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object LoadSettings(Type propertyType, ConfigurationItem configurationItemAttribute)
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

        public void SaveSettings(object value, ConfigurationItem configurationItemAttribute)
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
        }

        private string TryGetFilePath()
        {
            var filePath = _configuration.GetRequiredSection("Settings")["FilePath"];

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ReflectionException("Custom file is not specified");
            }

            return filePath;
        }
    }
}
