using Microsoft.Extensions.Configuration;
using WPFApp.Domain.Models.Configurations;

namespace WPFApp.Extras.Extensions
{
    public static class IConfigurationExtnsions
    {
        public static Authentication GetAuthentication(this IConfiguration configuration) => configuration
                .GetSection("Authentication")
                .Get<Authentication>();
    }
}
