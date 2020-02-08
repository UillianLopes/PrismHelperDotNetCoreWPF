using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFApp.Extras.Converters
{
    public class EqualsToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value?.Equals(parameter) == true ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}
