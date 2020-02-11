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

        public void Previous(bool pop) 
        {
            if (_history.Previous(pop) is Page page)
                _outlet.Content = page;
        }

        public void Next(bool pop)
        {
            if (_history.Next(pop) is Page page)
                _outlet.Content = page;
        }

        public async Task Navigate(string route, bool useHistory, object param = null)
        {
            if (!(this[route] is NavigationRoute navigationRoute))
                throw new InvalidOperationException($"Route not found ${route}");

            Page page = null;

            if (useHistory && _history[route] is NavigationHistoryItem historyItem)
            {
                page = historyItem.Page;
            }
            else
            {
                page = _provider.GetService(navigationRoute.Page) as Page;

                var parameters = param?
                    .GetType()
                    .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(param));

                var snapshot = new NavigationSnapshot(parameters, route);
                foreach (var gurdType in navigationRoute.Guards)
                {
                    if (!(_provider.GetService(gurdType) is INavigationGuard guard))
                        continue;

                    if (!await guard.CanActivate(snapshot))
                        return;
                }

                var resolvedDatas = new Dictionary<string, object>();
                foreach (var resolverType in navigationRoute.Resolvers)
                {
                    if (!(_provider.GetService(resolverType) is INavigationResolver resolver))
                        continue;

                    var resolvedData = await resolver.Resolve(snapshot);
                    resolvedDatas.TryAdd(resolver.Key, resolvedData);
                }

                if (page.DataContext is IQueryableNavigation qn)
                    await qn.OnQueried(parameters);

                if (page.DataContext is IResolvableNavigation rn)
                    if (!rn.OnResolved(resolvedDatas))
                        return;

                if (page is IQueryableNavigation pqn)
                    await pqn.OnQueried(parameters);

                if (page is IResolvableNavigation prn)
                    if (!prn.OnResolved(resolvedDatas))
                        return;

            }
            
            _history.Add(route, page);
            _outlet.Content = page;
        }

        public void SetFrame(Frame frame) => _outlet = frame;

        public void Pop() => _history.Pop();
    }


}
