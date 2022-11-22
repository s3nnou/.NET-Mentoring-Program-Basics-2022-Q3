namespace Reflection
{
    public interface IConfigurationProvider
    {
        public object LoadSettings(Type propertyType, ConfigurationItem configurationItemAttribute);

        public void SaveSettings(object value, ConfigurationItem configurationItemAttribute);
    }
}
