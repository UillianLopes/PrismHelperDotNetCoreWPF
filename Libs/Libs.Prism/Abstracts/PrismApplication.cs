using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Libs.Prism.Abstracts
{
    public abstract class PrismApplication : Application
    {


        public T Dispatch<T>(Func<T> func) => Current.Dispatcher.Invoke(func);
        public void Dispatch(Action action) => Current.Dispatcher.Invoke(action);

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder();

            CreateConfiguration(configurationBuilder);

            var configuration = configurationBuilder
                .Build();

            if (configuration != null)
            {
                serviceCollection
                    .AddSingleton(configuration);
            }

            ConfigureServices(serviceCollection, configuration);

            var provider = serviceCollection
                .BuildServiceProvider();

            base.OnStartup(e);

            var window = CreateWindow(provider);

            InitializeShell(window);
        }

        /// <summary>
        /// Method called to register our services to the dependency injection
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="configuration"></param>
        protected abstract void ConfigureServices(IServiceCollection collection, IConfiguration configuration);

        /// <summary>
        /// Method called to initialize the configurations of the app, this is the first method called on app startup
        /// </summary>
        /// <param name="configuration"></param>
        protected abstract void CreateConfiguration(IConfigurationBuilder configuration);

        /// <summary>
        /// Method user do define the main window of the application
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        protected abstract Window CreateWindow(IServiceProvider provider);

        /// <summary>
        /// Method called to show the main window, if you override it please call base.InitializeShell(window) inside the override method
        /// </summary>
        /// <param name="window"></param>
        protected virtual void InitializeShell(Window window) => window.ShowDialog();

    }
}
