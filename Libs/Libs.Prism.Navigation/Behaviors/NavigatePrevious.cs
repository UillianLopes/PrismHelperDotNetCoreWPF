using Libs.Prism.Navigation.Services;
using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace Libs.Prism.Navigation.Behaviors
{
    public class NavigatePrevious : TargetedTriggerAction<object>
    {
        public string AreaName
        {
            get { return (string)GetValue(AreaNameProperty); }
            set { SetValue(AreaNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AreaName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaNameProperty =
            DependencyProperty.Register("AreaName", typeof(string), typeof(NavigatePrevious), new PropertyMetadata(null));

        public bool Pop
        {
            get { return (bool)GetValue(PopProperty); }
            set { SetValue(PopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopProperty =
            DependencyProperty.Register("Pop", typeof(bool), typeof(NavigatePrevious), new PropertyMetadata(false));

        protected override void Invoke(object parameter)
        {
            if (NavigationContext.Service == null)
                return;

            NavigationContext.Service.Previous(AreaName, Pop);
        }
    }
}
