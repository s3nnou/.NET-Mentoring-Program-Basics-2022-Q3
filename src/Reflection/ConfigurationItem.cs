using System.Runtime.CompilerServices;

namespace Reflection
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigurationItem : Attribute
    {
        public ConfigurationItem(ProviderType providerType, [CallerMemberName] string settingName = null)
        {
            ProviderType = providerType;
            SettingName = settingName;
        }

        public ProviderType ProviderType { get; }

        public string SettingName { get; }
    }
}
