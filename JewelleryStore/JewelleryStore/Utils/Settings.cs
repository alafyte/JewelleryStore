using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace JewelleryStore
{
    internal static class Settings
    {
        private static Languages _lang;
        public static event Action changeLang;
        public static readonly string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public enum Languages
        {
            RU,
            EN
        }
        public static ResourceDictionary ResourceLights = new ResourceDictionary();
        public static ResourceDictionary ResourceDefaults = new ResourceDictionary();
        public static ResourceDictionary ResourcePrimaryTeal = new ResourceDictionary();
        public static ResourceDictionary ResourceAccentTeal = new ResourceDictionary();
        public static ResourceDictionary ResourcePrimaryPink = new ResourceDictionary();
        public static ResourceDictionary ResourceAccentGreen = new ResourceDictionary();
        public static ResourceDictionary ResourcePrimaryBrown = new ResourceDictionary();
        public static ResourceDictionary ResourceAccentCyan = new ResourceDictionary();
        public static ResourceDictionary ResourceStyles = new ResourceDictionary();
        public static ResourceDictionary ResourceEnLang = new ResourceDictionary();
        public static ResourceDictionary ResourceRusLang = new ResourceDictionary();

        public static Languages Lang
        {
            get
            {
                return _lang;
            }
            set
            {
                _lang = value;
                changeLang?.Invoke();
            }
        }
        static Settings()
        {
            Lang = Languages.RU;

            ResourceLights.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");
            ResourceDefaults.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml");
            ResourcePrimaryTeal.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Teal.xaml");
            ResourceAccentTeal.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Teal.xaml");
            ResourcePrimaryPink.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Pink.xaml");
            ResourceAccentGreen.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Pink.xaml");
            ResourcePrimaryBrown.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Brown.xaml");
            ResourceAccentCyan.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Cyan.xaml");
            ResourceStyles.Source = new Uri("pack://application:,,,/Resources/Styles.xaml");
            ResourceEnLang.Source = new Uri("pack://application:,,,/Resources/StringResources.En.xaml");
            ResourceRusLang.Source = new Uri("pack://application:,,,/Resources/StringResources.Rus.xaml");
        }
        public static string FindMessage(string message)
        {
            if (Application.Current.Resources[message] != null)
                return Application.Current.Resources[message].ToString();
            else
                return message;
        }
    }
}
