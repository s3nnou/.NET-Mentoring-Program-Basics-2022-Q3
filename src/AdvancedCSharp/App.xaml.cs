using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AdvancedCSharp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindow>().AsSelf();
            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var main = scope.Resolve<MainWindow>();
                main.ShowDialog();
            }
        }

        private static IContainer Container { get; set; }

    }
}
