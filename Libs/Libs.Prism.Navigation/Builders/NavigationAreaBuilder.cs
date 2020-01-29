using Libs.Prism.Navigation.Options;
using System;
using System.Collections.Generic;

namespace Libs.Prism.Navigation.Builders
{
    public class NavigationAreaBuilder
    {
        private readonly ICollection<NavigationRouteBuilder> _routes;

        public NavigationAreaBuilder()
        {
            _routes = new List<NavigationRouteBuilder>();
        }

        public NavigationAreaBuilder AddRoute(Action<NavigationRouteBuilder> action)
        {
            var builder = new NavigationRouteBuilder();
            
            action(builder);
            
            _routes.Add(builder);

            return this;
        }

        public IEnumerable<NavigationRoute> Build()
        {
            foreach (var route in _routes)
                yield return route.Build();
        }
    }
}
