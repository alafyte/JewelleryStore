using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JewelleryStore
{
    public class AdminWindowViewModel : PropertyChangedNotification
    {
        private DatabaseUnit db = new DatabaseUnit();
        private User admin;
        public static int id;
        public RelayCommand SwitchLangCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand ShowProductsCommand { get; set; }
        public RelayCommand ShowStatisticsCommand { get; set; }
        public RelayCommandParam SwitchThemeCommand { get; set; }
        public Uri Page
        {
            get { return GetValue(() => Page); }
            set { SetValue(() => Page, value); }
        }
        public bool LangCheck
        {
            get { return GetValue(() => LangCheck); }
            set { SetValue(() => LangCheck, value); }
        }
        public Cursor Cursor { get; set; }
        public AdminWindowViewModel() 
        {
            Cursor = CursorCollection.GetCursor();
            Page = WindowService.GetPage(WindowService.Pages.ProductsPage);

            admin = db.Users.Get(id);
            ChangeTheme(admin.Theme);


            SwitchLangCommand = new RelayCommand(SwitchLang);
            CloseCommand = new RelayCommand(LogOut);
            ShowProductsCommand = new RelayCommand(ShowProducts);
            ShowStatisticsCommand = new RelayCommand(ShowStatistics);
            SwitchThemeCommand = new RelayCommandParam((tag) => ChangeTheme((string)tag));
            switch(Settings.Lang)
            {
                case Settings.Languages.RU:
                    LangCheck = false;
                    break;
                case Settings.Languages.EN:
                    LangCheck = true;
                    break;
            }
        }
        #region Смена языка
        private void ToRussian()
        {
            Application.Current.Resources.MergedDictionaries.Remove(Settings.ResourceEnLang);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceRusLang);
            Settings.Lang = Settings.Languages.RU;

        }
        private void SwitchLang()
        {
            if (LangCheck)
                ToEnglish();
            else
                ToRussian();
        }
        private void ToEnglish()
        {
            Application.Current.Resources.MergedDictionaries.Remove(Settings.ResourceRusLang);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceEnLang);
            Settings.Lang = Settings.Languages.EN;

        }
        #endregion
        #region Навигация

        private void ShowProducts()
        {
            Page = WindowService.GetPage(WindowService.Pages.ProductsPage);
        }
        private void ShowStatistics()
        {
            Page = WindowService.GetPage(WindowService.Pages.StatisticsPage);
        }
        private void LogOut()
        {
            WindowService.OpenWindow(WindowService.Windows.Login, new LoginViewModel());
            WindowService.CloseWindow(WindowService.Windows.AdminWindow);
        }
        #endregion
        #region Смена темы
        private void ChangeTheme(string tag)
        {
            switch (tag)
            {
                case "Theme1":
                    SwitchTheme1();
                    break;
                case "Theme2":
                    SwitchTheme2();
                    break;
                case "Theme3":
                    SwitchTheme3();
                    break;
            }
        }
        private void SwitchTheme1()
        {
            Application.Current.Resources.MergedDictionaries.Clear();

            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceDefaults);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceLights);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceStyles);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourcePrimaryTeal);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceAccentTeal);

            switch (Settings.Lang)
            {
                case Settings.Languages.EN:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceEnLang);
                    break;
                case Settings.Languages.RU:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceRusLang);
                    break;
            }

            admin.Theme = "Theme1";
            db.Users.Update(admin);
        }

        private void SwitchTheme2()
        {
            Application.Current.Resources.MergedDictionaries.Clear();

            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceDefaults);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceLights);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceStyles);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourcePrimaryPink);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceAccentGreen);

            switch (Settings.Lang)
            {
                case Settings.Languages.EN:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceEnLang);
                    break;
                case Settings.Languages.RU:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceRusLang);
                    break;
            }

            admin.Theme = "Theme2";
            db.Users.Update(admin);
        }

        private void SwitchTheme3()
        {
            Application.Current.Resources.MergedDictionaries.Clear();

            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceDefaults);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceLights);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceStyles);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourcePrimaryBrown);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceAccentCyan);

            switch (Settings.Lang)
            {
                case Settings.Languages.EN:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceEnLang);
                    break;
                case Settings.Languages.RU:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceRusLang);
                    break;
            }

            admin.Theme = "Theme3";
            db.Users.Update(admin);
        }
        #endregion

    }
}
