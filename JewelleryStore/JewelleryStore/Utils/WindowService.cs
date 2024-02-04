using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static JewelleryStore.WindowService;

namespace JewelleryStore
{
    public static class WindowService
    {
        public enum Windows
        {
            Login, 
            MainWindow,
            AdminWindow,
            Message,
            ProductDialog,
            UserDialog,
            UserEditDialog,
            UserEnter,
            ChangePass
        };
        public enum Pages
        {
            ProductDetailsView,
            ProductsPage,
            StatisticsPage,
            UserDetailsView
        };
        #region Windows
        public static MainWindow MainWindow { get; set; }
        public static Login Login { get; set; }
        public static AdminWindow AdminWindow { get; set; }
        public static Message Message { get; set; }
        public static ProductDialog ProductDialog { get; set; }
        public static UserDialog UserDialog { get; set; }
        public static UserEditDialog UserEditDialog { get; set; }
        public static UserEnter UserEnter { get; set; }
        public static ChangePassword ChangePassword { get; set; }
        #endregion


        static WindowService() { }
        public static void OpenWindow(Windows win, PropertyChangedNotification ViewModel, EventHandler onClosingFunc = null)
        {
            Window window = null;
            bool dialog = false;
            switch(win)
            {
                case Windows.Login:
                    window = new Login();
                    Login = (Login)window;
                    break;
                case Windows.MainWindow:
                    window = new MainWindow();
                    MainWindow = (MainWindow)window;
                    break;
                case Windows.AdminWindow:
                    window = new AdminWindow();
                    AdminWindow = (AdminWindow)window;
                    break;
                case Windows.Message:
                    window = new Message();
                    Message = (Message)window;
                    dialog = true;
                    break;
                case Windows.ProductDialog:
                    window = new ProductDialog();
                    ProductDialog = (ProductDialog)window;
                    dialog = true;
                    break;
                case Windows.UserDialog:
                    window = new UserDialog();
                    UserDialog = (UserDialog)window;
                    dialog = true;
                    break;
                case Windows.UserEditDialog:
                    window = new UserEditDialog();
                    UserEditDialog = (UserEditDialog)window;
                    dialog = true;
                    break;
                case Windows.UserEnter:
                    window = new UserEnter();
                    UserEnter = (UserEnter)window;
                    dialog = true;
                    break;
                case Windows.ChangePass:
                    window = new ChangePassword();
                    ChangePassword = (ChangePassword)window;
                    dialog = true;
                    break;
                default:
                    Console.WriteLine("No such Window");
                    break;
            }
            if (window != null)
            {
                window.DataContext = ViewModel;
                if (onClosingFunc != null)
                    window.Closed += onClosingFunc;
                if (dialog)
                    window.ShowDialog();
                else
                    window.Show();
            }
        }

        public static void CloseWindow(Windows win)
        {
            switch (win)
            {
                case Windows.Login:
                    Login.Close();
                    break;
                case Windows.MainWindow:
                    MainWindow.Close();
                    break;
                case Windows.Message:
                    Message.Close();
                    break;
                case Windows.ProductDialog:
                    ProductDialog.Close();
                    break;
                case Windows.UserDialog:
                    UserDialog.Close();
                    break;
                case Windows.UserEditDialog:
                    UserEditDialog.Close();
                    break;
                case Windows.AdminWindow:
                    AdminWindow.Close();
                    break;
                case Windows.UserEnter:
                    UserEnter.Close();
                    break;
                case Windows.ChangePass:
                    ChangePassword.Close();
                    break;
                default:
                    Console.WriteLine("No such Window");
                    break;
            }
        }
        public static Uri GetPage(Pages page)
        {
            Uri uri = null;
            switch(page)
            {
                case Pages.ProductDetailsView:
                    uri = new Uri("pack://application:,,,/Views/ProductDetailsView.xaml");
                    break;
                case Pages.UserDetailsView:
                    uri = new Uri("pack://application:,,,/Views/UserDetailsView.xaml");
                    break;
                case Pages.StatisticsPage:
                    uri = new Uri("pack://application:,,,/Views/StatisticsPage.xaml");
                    break;
                case Pages.ProductsPage:
                    uri = new Uri("pack://application:,,,/Views/ProductsPage.xaml");
                    break;
                default:
                    uri = null;
                    break;
            }
            return uri;
        }

    }
}
