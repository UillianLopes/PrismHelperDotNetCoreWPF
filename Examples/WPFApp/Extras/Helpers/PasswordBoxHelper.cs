using System.Windows;
using System.Windows.Controls;

namespace WPFApp.Extras.Helpers
{
    public class PasswordBoxHelper
    {
        public static bool GetIsAttached(DependencyObject obj) => (bool)obj.GetValue(IsAttachedProperty);
        public static void SetIsAttached(DependencyObject obj, bool value) => obj.SetValue(IsAttachedProperty, value);

        public static readonly DependencyProperty IsAttachedProperty =
            DependencyProperty.RegisterAttached("IsAttached", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(false, (obj, _) => 
            {
                if (!(obj is PasswordBox passwordBox))
                    return;

                if (GetIsAttached(passwordBox))
                {
                    passwordBox.Loaded += PasswordBoxLoaded;
                    passwordBox.PasswordChanged += PasswordBoxPasswordChanged;
                }
                else
                {
                    passwordBox.Loaded -= PasswordBoxLoaded;
                    passwordBox.PasswordChanged -= PasswordBoxPasswordChanged;
                }
            }));

        private static void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox))
                return;

            SetPassword(passwordBox, passwordBox.Password);
        }

        private static void PasswordBoxLoaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox))
                return;

            passwordBox.PasswordChanged -= PasswordBoxPasswordChanged;
            passwordBox.Password = GetPassword(passwordBox);
            passwordBox.PasswordChanged += PasswordBoxPasswordChanged;

        }

        public static string GetPassword(DependencyObject obj) => (string)obj.GetValue(PasswordProperty);
        public static void SetPassword(DependencyObject obj, string value) => obj.SetValue(PasswordProperty, value);

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxHelper), new PropertyMetadata(null));



    }
}
