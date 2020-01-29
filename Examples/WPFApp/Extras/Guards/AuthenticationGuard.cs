using Libs.Prism.Navigation.Interfaces;
using Libs.Prism.Navigation.Options;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WPFApp.Domain.Models.Configurations;
using WPFApp.Extras.Extensions;

namespace WPFApp.Extras.Guards
{
    public class AuthenticationGuard : INavigationGuard
    {
        private readonly IConfiguration _configuration;
        private readonly INavigationService _navigationService;

        public AuthenticationGuard(IConfiguration configuration, INavigationService navigationService)
        {
            _configuration = configuration;
            _navigationService = navigationService;
        }

        public async Task<bool> CanActivate(NavigationSnapshot snapshot)
        {
            if (!(_configuration.GetAuthentication() is Authentication authentication && authentication.IsValid()))
                await _navigationService.Navigate("authentication", "login");

            return true;
        }
    }
}
