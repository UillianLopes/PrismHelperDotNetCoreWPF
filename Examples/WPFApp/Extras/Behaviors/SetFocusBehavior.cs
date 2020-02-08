using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace WPFApp.Extras.Behaviors
{
    public class SetFocusBehavior : TargetedTriggerAction<object>
    {
        public FrameworkElement FocusedElement
        {
            get { return (FrameworkElement)GetValue(FocusedElementProperty); }
            set { SetValue(FocusedElementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FocusedElement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FocusedElementProperty =
            DependencyProperty.Register("FocusedElement", typeof(FrameworkElement), typeof(SetFocusBehavior), new PropertyMetadata(null));


        protected override void Invoke(object parameter)
        {
            if (parameter is RoutedEventArgs args && args.Source is FrameworkElement element)
                FocusManager.SetFocusedElement(element, FocusedElement);
        }
    }
}
