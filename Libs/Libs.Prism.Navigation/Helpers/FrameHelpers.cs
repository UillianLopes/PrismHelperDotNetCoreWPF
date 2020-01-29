using Libs.Prism.Navigation.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Libs.Prism.Navigation.Helpers
{
    public class FrameHelpers
    {
        public static string GetNavigationArea(DependencyObject obj) => (string)obj.GetValue(NavigationAreaProperty);

        public static void SetNavigationArea(DependencyObject obj, string value) => obj.SetValue(NavigationAreaProperty, value);

        // Using a DependencyProperty as the backing store for NavigationArea.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigationAreaProperty =
            DependencyProperty.RegisterAttached("NavigationArea", typeof(string), typeof(FrameHelpers), new PropertyMetadata(null, (obj, args) => 
            {
                if (!(obj is Frame frame))
                    return;

                if (GetNavigationArea(frame) is string navigationArea && !string.IsNullOrEmpty(navigationArea))
                {
                    frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

                    if (NavigationContext.Service != null)
                        NavigationContext.Service.AddArea(navigationArea, frame);
                }
            }));



    }
}
