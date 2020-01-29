using Libs.Prism.Navigation.Interfaces;
using Libs.Prism.Navigation.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Libs.Prism.Navigation.Builders
{
    public class NavigationRouteBuilder 
    {
        private string _route;
        private Type _page;
        private Type _viewModel;
        private readonly ICollection<Type> _resolvers;
        private readonly ICollection<Type> _guards;

        public NavigationRouteBuilder() 
        {
            _resolvers = new List<Type>();
            _guards = new List<Type>();
        }

        public NavigationRouteBuilder Page<T, VM>(string route) where T : Page where VM : class
        {
            _route = route;

            _page = typeof(T);
            _viewModel = typeof(VM);

            return this;
        }

        public NavigationRouteBuilder AddResolver<T>() where T : INavigationResolver
        {
            _resolvers.Add(typeof(T));
            return this;
        }

        public NavigationRouteBuilder AddGuard<T>() where T : INavigationGuard
        {
            _resolvers.Add(typeof(T));
            return this;
        }

        public NavigationRoute Build() => new NavigationRoute(
            _route,
            _page,
            _viewModel,
            _resolvers.ToArray(),
            _guards.ToArray()
        );
    }
}
