using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class Runner
    {
        private readonly IConfigurationComponentBase _configurationComponentBase;

        public Runner(IConfigurationComponentBase configurationComponentBase)
        {
            _configurationComponentBase = configurationComponentBase;
        }

        public void Run()
        {
            MyClass myClass = new MyClass(_configurationComponentBase);

            Console.WriteLine(myClass.Number);
            myClass.Number = 0;
            Console.WriteLine(myClass.Number);

            Console.WriteLine(myClass.Name);
            myClass.Name = "New name";
            Console.WriteLine(myClass.Name);
        }
    }
}
