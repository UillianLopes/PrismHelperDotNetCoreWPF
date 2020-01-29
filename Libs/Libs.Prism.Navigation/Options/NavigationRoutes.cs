using System.Collections.Generic;
using System.Linq;

namespace Libs.Prism.Navigation.Options
{
    internal class NavigationRoutes
    {
        private readonly IEnumerable<NavigationRoute> _routes;

        public NavigationRoute this[string route] => _routes
            .FirstOrDefault(rt => rt.Route == route);
        public NavigationRoutes()
        {
        }
        public NavigationRoutes(IEnumerable<NavigationRoute> routes) : this()
        {
            _routes = routes;
        }
    }
}
