using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JewelleryStore
{
    public class AttachedProperties
    {
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.RegisterAttached(
                "Name",
                typeof(string),
                typeof(AttachedProperties),
                new FrameworkPropertyMetadata(default(string), OnValueChanged)
            );

        public static void SetName(FrameworkElement element, string value)
        {
            element.SetValue(NameProperty, value);
        }

        public static string GetName(FrameworkElement element)
        {
            return (string)element.GetValue(NameProperty);
        }

        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                element.Name = (string)element.GetValue(NameProperty);
            }
        }
    }
}
