using Libs.Prism.Navigation.Interfaces;

namespace Libs.Prism.Navigation.Services
{
    public class NavigationContext
    {
        public static INavigationService Service { get; internal set; }
    }
}
