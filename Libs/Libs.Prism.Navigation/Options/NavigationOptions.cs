using System.Collections.Generic;

namespace Libs.Prism.Navigation.Options
{
    public class NavigationOptions
    {
        internal NavigationOptions(ICollection<NavigationRoute> routes)
        {
            Routes = routes;
        }

        public ICollection<NavigationRoute> Routes { get; }
    }
}
