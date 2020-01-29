using Libs.Prism.Navigation.Builders;
using Libs.Prism.Navigation.Interfaces;
using Libs.Prism.Navigation.Options;
using Libs.Prism.Navigation.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Libs.Prism.Navigation.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddNavigation(this IServiceCollection collection, Action<NavigationAreaBuilder> action)
        {
            var builder = new NavigationAreaBuilder();

            action(builder);

            var routes = builder.Build();

            foreach (var route in routes)
                collection.AddView(route.Page, route.ViewModel);

            var resolvers = routes
                .SelectMany(rt => rt.Resolvers)
                .GroupBy(rt => rt.FullName)
                .Select(rt => rt.FirstOrDefault());

            foreach (var resolver in resolvers)
                collection.AddTransient(resolver);

            var guards = routes
                .SelectMany(rt => rt.Guards)
                .GroupBy(rt => rt.FullName)
                .Select(rt => rt.FirstOrDefault());

            foreach (var guard in guards)
                collection.AddTransient(guard);

            var nRoutes = new NavigationRoutes(routes);

            collection.AddSingleton(nRoutes);
            collection.AddSingleton<INavigationService, NavigationService>();
            
        }

        public static void UseNavigation(this IServiceProvider provider)
        {
            NavigationContext.Service = provider.GetRequiredService<INavigationService>();
        }
    }
}
