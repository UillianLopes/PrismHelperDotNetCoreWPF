using Libs.Prism.Abstracts;
using Libs.Prism.Navigation.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Wpf.Core;
using System;
using System.IO;
using System.Windows;
using WPFApp.Domain.Contracts;
using WPFApp.Extras.Constants;
using WPFApp.Infra.Data;
using WPFApp.Infra.Repositories;
using WPFApp.ViewModels;
using WPFApp.ViewModels.Home;
using WPFApp.ViewModels.Tasks.Pages;
using WPFApp.ViewModels.Tasks.Resolvers;
using WPFApp.Views;
using WPFApp.Views.Home;
using WPFApp.Views.Tasks.Pages;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void CreateConfiguration(IConfigurationBuilder configuration) => configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        protected override void ConfigureServices(IServiceCollection collection, IConfiguration configuration)
        {

            collection.AddView<MainWindow, MainWindowViewModel>(v => v.WindowStartupLocation = WindowStartupLocation.CenterScreen);
            
            var connectionString = configuration["ConnectionString"];

            collection.AddDbContextPool<WPFAppDataContext>(options => options
                .UseSqlite(connectionString));

            collection.AddTransient<NotificationManager>();
            collection.AddScoped<IGenericRepository, GenericRepository>();

            collection.AddNavigation(bd => bd
                // Home
                .AddRoute(rb => rb.Page<HomePage, HomeViewModel>(NavigationRoutes.HOME_PAGE))
                // Task
                .AddRoute(rb => rb.Page<Register, RegisterViewModel>(NavigationRoutes.TASK_REGISTER))
                .AddRoute(rb => rb.Page<Detail, DetailViewModel>(NavigationRoutes.TASK_DETAIL)
                    .AddResolver<TaskDetailResolver>())
                .AddRoute(rb => rb.Page<List, ListViewModel>(NavigationRoutes.TASK_LIST)
                    .AddResolver<TaskListResolver>()));;
            collection.AddScoped<IUnityOfWork, UnityOfWork>();
        }

        protected override Window CreateWindow(IServiceProvider provider) 
        {
            provider.UseNavigation();

            UpdateDatabase(provider);

            return provider.GetRequiredService<MainWindow>();
        } 

        protected override void InitializeShell(Window window) => base.InitializeShell(window);

        private void UpdateDatabase(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<WPFAppDataContext>();

            if (context.Database.EnsureCreated())
                context.Database.Migrate();
        }
    }

    /// <summary>
    /// This is needed if you are using a database
    /// </summary>
    public class WPFAppDataContextFactory : IDesignTimeDbContextFactory<WPFAppDataContext>
    {
        public WPFAppDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WPFAppDataContext>();
            optionsBuilder.UseSqlite("Data Source=migrationsdb.db");
            return new WPFAppDataContext(optionsBuilder.Options); ;
        }
    }


}
