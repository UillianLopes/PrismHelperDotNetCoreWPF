using System;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp.Extras.Helpers
{
    public class StackPanelHelper
    {
        public static int GetLayoutGap(DependencyObject obj) => (int)obj.GetValue(LayoutGapProperty);

        public static void SetLayoutGap(DependencyObject obj, int value) => obj.SetValue(LayoutGapProperty, value);

        public static readonly DependencyProperty LayoutGapProperty =
            DependencyProperty.RegisterAttached("LayoutGap", typeof(int), typeof(StackPanelHelper), new PropertyMetadata(0, (dpo, args) => 
            {
                if (!(dpo is StackPanel stackPanel))
                    return;

                if (GetLayoutGap(stackPanel) > 0)
                    stackPanel.Initialized += StackPanelInitialized;

            }));

        private static void StackPanelInitialized(object sender, EventArgs e)
        {
            if (!(sender is StackPanel stackPanel))
                return;

            var orirentation = stackPanel.Orientation;
            var children = stackPanel.Children;
            var gap = GetLayoutGap(stackPanel);

            foreach (FrameworkElement child in children)
            {
                var index = children.IndexOf(child);

                if ((index + 1) >= children.Count)
                    return;

                if (!(child.Margin is Thickness margin))
                    continue;

                double top = margin.Top;
                double left = margin.Left;
                double bottom = margin.Bottom <= 0 ? margin.Bottom + (orirentation == Orientation.Vertical ? gap : 0) : margin.Bottom;
                double right = margin.Right <= 0 ? margin.Right + (orirentation == Orientation.Horizontal ? gap : 0) : margin.Bottom;

                child.Margin = new Thickness(left, top, right, bottom);
            }
        }
    }
}
