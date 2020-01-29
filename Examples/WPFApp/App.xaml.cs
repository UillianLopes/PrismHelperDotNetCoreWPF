using Libs.Prism.Abstracts;
using Libs.Prism.Navigation.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Wpf.Core;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFApp.Domain.Models.Configurations;
using WPFApp.Domain.Services;
using WPFApp.Extras.Constants;
using WPFApp.Extras.Extensions;
using WPFApp.ViewModels;
using WPFApp.ViewModels.Home;
using WPFApp.ViewModels.Players.Pages;
using WPFApp.Views;
using WPFApp.Views.Home;
using WPFApp.Views.Players.Pages;

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
            

            var databaseConfiguration = configuration
                .GetSection("DatabaseConfiguration")
                .Get<DatabaseConfiguration>();

            var clientConfiguration = configuration
                .GetSection("ClientConfiguration")
                .Get<ClientConfiguration>();

            collection.AddSingleton(clientConfiguration);
            collection.AddSingleton(databaseConfiguration);
            collection.AddTransient<AuthenticationService>();
            collection.AddTransient<NotificationManager>();

            collection.AddHttpClient(HttpClientNames.WITHOUT_BASE_URL)
                .ConfigureHttpClient(client =>
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                });

            collection.AddHttpClient(HttpClientNames.WITH_AUTHENTICATION)
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(clientConfiguration.ApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                });

            collection.AddHttpClient(HttpClientNames.WITH_AUTHENTICATION)
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(clientConfiguration.ApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .AddHttpMessageHandler<AuthenticationInterceptor>();

            collection.AddNavigation(bd => 
                bd.AddRoute(rb => rb.Page<HomePage, HomeViewModel>(NavigationRoutes.HOME_PAGE))
                  .AddRoute(rb => rb.Page<Login, LoginViewModel>(NavigationRoutes.LOGIN_PAGE)));
        }

        protected override Window CreateWindow(IServiceProvider provider) 
        {
            provider.UseNavigation();
            return provider.GetRequiredService<MainWindow>();
        } 

        protected override void InitializeShell(Window window)
        {
            base.InitializeShell(window);
        }


    }
    
    public class AuthenticationInterceptor : DelegatingHandler
    {
        private readonly Authentication _authentication;

        public AuthenticationInterceptor(IConfiguration configuration)
        {
            if (configuration.GetAuthentication() is Authentication authentication)
                _authentication = authentication;

        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!(_authentication?.IsValid() == true))
                throw new Exception("Autenticação inválida!");
            
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authentication.Token);

            return SendAsync(request, cancellationToken);
        }
    }
}
