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
using WPFApp.Extras.Constants;
using WPFApp.Infra.Data;
using WPFApp.ViewModels;
using WPFApp.ViewModels.Home;
using WPFApp.ViewModels.Tasks.Pages;
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
            
            collection.AddDbContextPool<WPFAppDataContext>(options => options
                .UseSqlite(DatabaseNames.EXAMPLE));

            collection.AddTransient<NotificationManager>();

            collection.AddNavigation(bd => bd
                // Home
                .AddRoute(rb => rb.Page<HomePage, HomeViewModel>(NavigationRoutes.HOME_PAGE))
                // Task
                .AddRoute(rb => rb.Page<Register, RegisterViewModel>(NavigationRoutes.TASK_REGISTER))
                .AddRoute(rb => rb.Page<Detail, DetailViewModel>(NavigationRoutes.TASK_DETAIL))
                .AddRoute(rb => rb.Page<List, DetailViewModel>(NavigationRoutes.TASK_LIST)));    
        }

        protected override Window CreateWindow(IServiceProvider provider) 
        {
            provider.UseNavigation();
            return provider.GetRequiredService<MainWindow>();
        } 

        protected override void InitializeShell(Window window) => base.InitializeShell(window);

    }

    /// <summary>
    /// This is needed if you are using a database
    /// </summary>
    public class WPFAppDataContextFactory : IDesignTimeDbContextFactory<WPFAppDataContext>
    {
        public WPFAppDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WPFAppDataContext>();
            
            optionsBuilder.UseSqlite(DatabaseNames.EXAMPLE);

            return new WPFAppDataContext(optionsBuilder.Options); ;
        }
    }

}
