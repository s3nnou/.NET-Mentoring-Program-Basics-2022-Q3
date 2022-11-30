using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Models
{
    public interface IConfigurationProvider
    {
        public object LoadSettings(Type propertyType, ConfigurationItem configurationItemAttribute);

        public void SaveSettings(object value, ConfigurationItem configurationItemAttribute);
    }

    public interface IFileConfigurationProvider : IConfigurationProvider
    {
    }

    public interface IConfigurationManagerConfigurationProvider : IConfigurationProvider
    {
    }

}
