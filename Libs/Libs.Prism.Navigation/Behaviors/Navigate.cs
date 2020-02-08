using Libs.Prism.Navigation.Services;
using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace Libs.Prism.Navigation.Behaviors
{
    public class Navigate : TargetedTriggerAction<object>
    {
        public string Route
        {
            get { return (string)GetValue(RouteProperty); }
            set { SetValue(RouteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Route.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RouteProperty =
            DependencyProperty.Register("Route", typeof(string), typeof(Navigate), new PropertyMetadata(null));

        public string AreaName
        {
            get { return (string)GetValue(AreaNameProperty); }
            set { SetValue(AreaNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AreaName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaNameProperty =
            DependencyProperty.Register("AreaName", typeof(string), typeof(Navigate), new PropertyMetadata(null));


        public bool UseHistory
        {
            get { return (bool)GetValue(UseHistoryProperty); }
            set { SetValue(UseHistoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UseHistory.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseHistoryProperty =
            DependencyProperty.Register("UseHistory", typeof(bool), typeof(Navigate), new PropertyMetadata(true));


        public object QueryParams
        {
            get { return (object)GetValue(QueryParamsProperty); }
            set { SetValue(QueryParamsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for QueryParams.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QueryParamsProperty =
            DependencyProperty.Register("QueryParams", typeof(object), typeof(Navigate), new PropertyMetadata(null));


        public bool Pop
        {
            get { return (bool)GetValue(PopProperty); }
            set { SetValue(PopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopProperty =
            DependencyProperty.Register("Pop", typeof(bool), typeof(Navigate), new PropertyMetadata(null));



        protected override void Invoke(object parameter)
        {
            if (NavigationContext.Service == null)
                return;

            if (Pop)
                NavigationContext.Service.Pop(AreaName);

            NavigationContext.Service.Navigate(AreaName, Route, UseHistory, QueryParams);
        }
    }
}
