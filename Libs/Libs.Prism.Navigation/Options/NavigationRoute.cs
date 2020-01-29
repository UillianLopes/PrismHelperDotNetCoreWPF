using System;

namespace Libs.Prism.Navigation.Options
{
    public class NavigationRoute
    {
        internal NavigationRoute(string route, Type page, Type viewModel, Type[] resolvers, Type[] guards)
        {
            Route = route;
            Page = page;
            ViewModel = viewModel;
            Resolvers = resolvers;
            Guards = guards;
        }

        public string Route { get; }
        public Type Page { get; }
        public Type ViewModel { get; }
        public Type[] Resolvers { get; }
        public Type[] Guards { get; }
    }
}
