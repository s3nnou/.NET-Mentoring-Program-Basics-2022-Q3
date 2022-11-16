namespace Reflection
{
    public interface IConfigurationComponentBase
    {
        U LoadSettings<T, U>(string propertyName);

        void SaveSettings<T>(string propertyName, object value);
    }

    public class ConfigurationComponentBase : IConfigurationComponentBase
    {
        private readonly IFileConfigurationProvider _fileConfigurationProvider;
        private readonly IConfigurationManagerConfigurationProvider _configurationManagerConfigurationProvider;

        public ConfigurationComponentBase(IFileConfigurationProvider fileConfigurationProvider,
            IConfigurationManagerConfigurationProvider configurationManagerConfigurationProvider)
        {
            _fileConfigurationProvider = fileConfigurationProvider;
            _configurationManagerConfigurationProvider = configurationManagerConfigurationProvider;
        }

        public U LoadSettings<T, U>(string propertyName)
        {
            var property = typeof(T).GetProperties().FirstOrDefault(i => i.Name == propertyName);

            var attribute =
                   Attribute.GetCustomAttribute(property, typeof(ConfigurationItem)) as ConfigurationItem;

            if (attribute != null)
            {
                try
                {
                    if(attribute.ProviderType == ProviderType.ConfigurationManager)
                    {
                        var value = _configurationManagerConfigurationProvider.LoadSettings(property.PropertyType, attribute);
                        return (U)value;
                    }
                    else
                    {
                        var value = _fileConfigurationProvider.LoadSettings(property.PropertyType, attribute);
                        return (U)value;
                    }
                }
                catch(ReflectionException ex)
                {
                    Console.WriteLine($"Error happend during setting reading. See error: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Unsupported property type. See error: {ex.Message}");
                }
            }

            return default(U);
        }

        public void SaveSettings<T>(string propertyName, object value)
        {
            var property = typeof(T).GetProperties().FirstOrDefault(i => i.Name == propertyName);
            var attribute =
                   Attribute.GetCustomAttribute(property, typeof(ConfigurationItem)) as ConfigurationItem;

            if (attribute != null)
            {
                try
                {
                    if (attribute.ProviderType == ProviderType.ConfigurationManager)
                    {
                        _configurationManagerConfigurationProvider.SaveSettings(value, attribute);
                    }
                    else
                    {
                        _fileConfigurationProvider.SaveSettings(value, attribute);
                    }
                }
                catch (ReflectionException ex)
                {
                    Console.WriteLine($"Error happend during setting reading. See error: {ex.Message}");
                }
            }
        }
    }
}
