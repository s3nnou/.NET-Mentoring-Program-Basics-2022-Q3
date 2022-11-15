using Reflection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Runner
{
    public class MyClass
    {
        private readonly IConfigurationComponentBase _configurationComponentBase;

        public MyClass(IConfigurationComponentBase configurationComponentBase)
        {
            _configurationComponentBase = configurationComponentBase;
        }


        [ConfigurationItem(ProviderType.File)]
        public string Name
        {
            get
            {
                return _configurationComponentBase.LoadSettings<MyClass, string>(nameof(this.Name));
            }

            set
            {
                _configurationComponentBase.SaveSettings<MyClass>(nameof(this.Name), value);
            }
        }

        [ConfigurationItem(ProviderType.ConfigurationManager)]
        public int Number
        {
            get
            {
                return _configurationComponentBase.LoadSettings<MyClass, int>(nameof(this.Number));
            }

            set
            {
                _configurationComponentBase.SaveSettings<MyClass>(nameof(this.Number), value);
            }
        }
    }
}
