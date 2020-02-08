using Libs.Prism.Navigation.Services;
using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace Libs.Prism.Navigation.Behaviors
{
    public class NavigateNext : TargetedTriggerAction<object>
    {
        public string AreaName
        {
            get { return (string)GetValue(AreaNameProperty); }
            set { SetValue(AreaNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AreaName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaNameProperty =
            DependencyProperty.Register("AreaName", typeof(string), typeof(NavigateNext), new PropertyMetadata(null));

        public bool Pop
        {
            get { return (bool)GetValue(PopProperty); }
            set { SetValue(PopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopProperty =
            DependencyProperty.Register("Pop", typeof(bool), typeof(NavigateNext), new PropertyMetadata(false));

        protected override void Invoke(object parameter)
        {
            if (NavigationContext.Service == null)
                return;

            if (Pop)
                NavigationContext.Service.Pop(AreaName);

            NavigationContext.Service.Next(AreaName, Pop);
        }

    }
}
