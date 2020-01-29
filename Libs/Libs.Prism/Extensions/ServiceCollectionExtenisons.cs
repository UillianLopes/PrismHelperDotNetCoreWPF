using System;
using System.Windows;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtenisons
    {
        /// <summary>
        /// Add's a view and a view model to the dependency injection pool, the view constructor need to be a parameterless constructor
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="VM"></typeparam>
        /// <param name="collection"></param>
        /// <param name="configure"></param>
        public static void AddView<V, VM>(this IServiceCollection collection, Action<V> configure = null) where V : Window where VM : class
        {
            collection.AddTransient<VM>();
            collection.AddTransient((provider) =>
            {
                var view = Activator.CreateInstance<V>();

                configure?.Invoke(view);

                view.DataContext = provider.GetRequiredService<VM>();

                return view;
            });
        }

        /// <summary>
        /// Add's a view and a view model to the dependency injection pool, the view constructor need to be a parameterless constructor
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="viewType"></param>
        /// <param name="viewModelType"></param>
        /// <param name="configure"></param>
        public static void AddView(this IServiceCollection collection, Type viewType, Type viewModelType, Action<object> configure = null)
        {
            collection.AddTransient(viewModelType);
            collection.AddTransient(viewType, (provider) =>
            {
                var view = Activator.CreateInstance(viewType);

                configure?.Invoke(view);

                if (view is FrameworkElement element)
                    element.DataContext = provider.GetService(viewModelType);

                return view;
            });
        }
    }
}
