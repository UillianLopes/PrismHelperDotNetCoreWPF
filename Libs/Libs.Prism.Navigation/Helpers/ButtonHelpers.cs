using Libs.Prism.Navigation.Services;
using System.Windows;
using System.Windows.Controls;

namespace Libs.Prism.Navigation.Helpers
{
    public class ButtonHelpers
    {
        public static string GetAreaName(DependencyObject obj) => (string)obj.GetValue(AreaNameProperty);

        public static void SetAreaName(DependencyObject obj, string value) => obj.SetValue(AreaNameProperty, value);

        // Using a DependencyProperty as the backing store for AreaName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaNameProperty =
            DependencyProperty.RegisterAttached("AreaName", typeof(string), typeof(ButtonHelpers), new PropertyMetadata(null));

        public static bool GetUseHistory(DependencyObject obj) => (bool)obj.GetValue(UseHistoryProperty);

        public static void SetUseHistory(DependencyObject obj, bool value) => obj.SetValue(UseHistoryProperty, value);

        // Using a DependencyProperty as the backing store for UseHistory.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseHistoryProperty =
            DependencyProperty.RegisterAttached("UseHistory", typeof(bool), typeof(ButtonHelpers), new PropertyMetadata(false));

        public static object GetQueryParams(DependencyObject obj) => obj.GetValue(QueryParamsProperty);

        public static void SetQueryParams(DependencyObject obj, object value) => obj.SetValue(QueryParamsProperty, value);

        // Using a DependencyProperty as the backing store for QueryParams.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QueryParamsProperty =
            DependencyProperty.RegisterAttached("QueryParams", typeof(object), typeof(ButtonHelpers), new PropertyMetadata(null));

        public static string GetRoute(DependencyObject obj) => (string)obj.GetValue(RouteProperty);

        public static void SetRoute(DependencyObject obj, string value) => obj.SetValue(RouteProperty, value);

        // Using a DependencyProperty as the backing store for Route.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RouteProperty =
            DependencyProperty.RegisterAttached("Route", typeof(string), typeof(ButtonHelpers), new PropertyMetadata(string.Empty, (db, args) =>
            {
                if (!(db is Button button))
                    return;

                button.Click -= Navigate;
                button.Click += Navigate;
            }));

        public static bool GetPop(DependencyObject obj)
        {
            return (bool)obj.GetValue(PopProperty);
        }

        public static void SetPop(DependencyObject obj, bool value)
        {
            obj.SetValue(PopProperty, value);
        }

        // Using a DependencyProperty as the backing store for Pop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopProperty =
            DependencyProperty.RegisterAttached("Pop", typeof(bool), typeof(ButtonHelpers), new PropertyMetadata(false));


        private static void Navigate(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button))
                return;

            if (!(GetAreaName(button) is string areaName && 
                  GetRoute(button) is string route))
                return;

            var param = GetQueryParams(button);
            var useHistory = GetUseHistory(button);

            if (NavigationContext.Service != null)
            {
                if (GetPop(button))
                    NavigationContext.Service.Pop(areaName);

                NavigationContext.Service.Navigate(areaName, route, useHistory, param);
            }
        }


    }
}
