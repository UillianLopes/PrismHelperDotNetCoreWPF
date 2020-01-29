using Libs.Prism.Interfaces;
using Libs.Prism.Navigation.Interfaces;
using Libs.Prism.Navigation.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Libs.Prism.Navigation.Services
{
    internal class NavigationArea : INavigationArea
    {
        private readonly IServiceProvider _provider;
        private readonly NavigationRoutes _routes;
        private readonly NavigationHistory _history;
        private Frame _outlet;

        public NavigationArea(IServiceProvider provider)
        {
            _provider = provider;
            _routes = provider.GetRequiredService<NavigationRoutes>();
            _history = new NavigationHistory();
        }

        private NavigationRoute this[string route] => _routes[route];

        public void Previous() => _outlet.Content = _history.Previous();

        public void Next() => _outlet.Content = _history.Next();

        public async Task Navigate(string route, bool useHistory, object param = null)
        {
            if (useHistory && _history[route] is NavigationHistoryItem item)
            {
                _outlet.Content = item.Page;
                _history.Add(route, item.Page);
                return;
            }

            if (!(this[route] is NavigationRoute nr))
                throw new InvalidOperationException($"Route not found ${route}");

            var page = _provider
                .GetService(nr.Page) as Page;

            var dicParam = param?
                .GetType()
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(pr => pr.Name, pr => pr.GetValue(param));

            var snapshot = new NavigationSnapshot(dicParam, route) ;
            
            foreach(var guardType in nr.Guards)
            {
                if (!(_provider.GetService(guardType) is INavigationGuard guard))
                    continue;

                if (!await guard.CanActivate(snapshot))
                    return;
            }

            var resolvedData = new Dictionary<string, object>();

            foreach (var resolverType in nr.Resolvers)
            {
                if (!(_provider.GetService(resolverType) is INavigationResolver resolver))
                    continue;

                var resolved = await resolver.Resolve(snapshot);
                resolvedData.TryAdd(resolver.Key, resolved);
            }

            if (page.DataContext is IQueryableNavigation qn)
                await qn.OnQueried(dicParam);

            if (page.DataContext is IResolvableNavigation rn)
                await rn.OnResolved(resolvedData);

            if (page is IQueryableNavigation pqn)
                await pqn.OnQueried(dicParam);

            if (page is IResolvableNavigation prn)
                await prn.OnResolved(resolvedData);

            _history.Add(route, page);
            _outlet.Content = page;
        }

        public void SetFrame(Frame frame) => _outlet = frame;
    }


}
