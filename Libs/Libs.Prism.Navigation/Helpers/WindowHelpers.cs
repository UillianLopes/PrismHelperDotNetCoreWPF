using Libs.Prism.Navigation.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Libs.Prism.Navigation.Helpers
{
    public class WindowHelpers
    {


        public static string GetInitialRoute(DependencyObject obj)
        {
            return (string)obj.GetValue(InitialRouteProperty);
        }

        public static void SetInitialRoute(DependencyObject obj, string value)
        {
            obj.SetValue(InitialRouteProperty, value);
        }

        /// <summary>
        /// Pass the route with this format "{areaName}|{route}", example "mainArea|home"
        /// </summary>
        public static readonly DependencyProperty InitialRouteProperty =
            DependencyProperty.RegisterAttached("InitialRoute", typeof(string), typeof(WindowHelpers), new PropertyMetadata(null, (dp, args) =>
            {
                if (!(dp is Window window))
                    return;

                if (GetInitialRoute(dp) is string route && !string.IsNullOrEmpty(route))
                {
                    window.ContentRendered -= NavigateOnContentRendered;
                    window.ContentRendered += NavigateOnContentRendered;
                }
                else
                    window.ContentRendered -= NavigateOnContentRendered;

            }));

        private static void NavigateOnContentRendered(object dp, EventArgs e)
        {
            if (!(dp is Window window && GetInitialRoute(window) is string initalRoute && 
                !string.IsNullOrEmpty(initalRoute)))
                return;

            if (!Regex.IsMatch(initalRoute, "[0-9a-zA-Z/\\-.,]+[|]+[0-9a-zA-Z/\\-]"))
                throw new Exception("Invalid inital route, the correct format is \"{areaName}|{route}\", example \"mainArea|home\".");

            var route = initalRoute
                .Split('|');

            if (NavigationContext.Service != null)
                NavigationContext.Service.Navigate(route[0], route[1]);

        }
    }
}
