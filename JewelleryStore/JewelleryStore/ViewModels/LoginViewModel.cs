using EmailService;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace JewelleryStore
{
    public class LoginViewModel : PropertyChangedNotification
    {
        DatabaseUnit db = new DatabaseUnit();
        int code;
        public RelayCommand SwitchLangCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand RecoverCommand { get; set; }
        public Cursor Cursor
        {
            get { return GetValue(() => Cursor); }
            set { SetValue(() => Cursor, value); }
        }
        public string Username
        {
            get { return GetValue(() => Username); }
            set { SetValue(() => Username, value); }
        }
        public string Password 
        { 
            get { return GetValue(() => Password); }
            set { SetValue(() => Password, value); }
        }
        public bool ErrorMessage
        {
            get { return GetValue(() => ErrorMessage); }
            set { SetValue(() => ErrorMessage, value); }
        }
        public bool LangCheck
        {
            get { return GetValue(() => LangCheck); }
            set { SetValue(() => LangCheck, value); }
        }
        public LoginViewModel() 
        { 
            Cursor = CursorCollection.GetCursor();
            SwitchLangCommand = new RelayCommand(SwitchLang);
            RegisterCommand = new RelayCommand(Register);
            LoginCommand = new RelayCommand(LogIn);
            RecoverCommand = new RelayCommand(Recover);
            ErrorMessage = false;
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    LangCheck = false;
                    break;
                case Settings.Languages.EN:
                    LangCheck = true;
                    break;
            }
        }
        private void LogIn()
        {
            Cursor = CursorCollection.GetCursor("loading");
            try
            {

                string u = Username;
                string p = Password;


                if (db.Users.GetAll().Any(o => o.NickName == u && SaltedHash.Verify(o.UserPasswordSalt, o.UserPasswordHash, Password) && o.Access == "admin"))
                {
                    AdminWindowViewModel.id = db.Users.GetAll().First(o => o.NickName == u && SaltedHash.Verify(o.UserPasswordSalt, o.UserPasswordHash, Password) && o.Access == "admin").UserId;
                    WindowService.OpenWindow(WindowService.Windows.AdminWindow, new AdminWindowViewModel());
                    WindowService.CloseWindow(WindowService.Windows.Login);

                }
                else if (db.Users.GetAll().Any(o => o.NickName == u && SaltedHash.Verify(o.UserPasswordSalt, o.UserPasswordHash, Password)))
                {
                    MainWindowViewModel.userId = db.Users.GetAll().First(o => o.NickName == u && SaltedHash.Verify(o.UserPasswordSalt, o.UserPasswordHash, Password)).UserId;
                    WindowService.OpenWindow(WindowService.Windows.MainWindow, new MainWindowViewModel());
                    WindowService.CloseWindow(WindowService.Windows.Login);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Username = "";
                Password = "";
                ErrorMessage = true;
                Cursor = CursorCollection.GetCursor();
            }
        }
        private void Recover()
        {
            try
            {
                DatabaseUnit db = new DatabaseUnit();
                WindowService.OpenWindow(WindowService.Windows.UserEnter, new UserEnterViewModel("EmailRecovery"));
                if (UserEnterViewModel.DialogResult)
                {
                    User user = db.Users.GetAll().Where(u => u.UserId == UserEnterViewModel.userId).FirstOrDefault();
                    SendVerificationMessage(user.GivenName, user.Email);
                    WindowService.OpenWindow(WindowService.Windows.UserEnter, new UserEnterViewModel("EnterCode", code, user.Email));
                    if (UserEnterViewModel.DialogResult)
                    {
                        WindowService.OpenWindow(WindowService.Windows.ChangePass, new ChangePasswordViewModel(user.UserId));
                        if (ChangePasswordViewModel.DialogResult)
                            WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel("Success"));
                    }
                }
            }
            catch (ArgumentException ex)
            {
                WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel(ex.Message));
            }
            catch (Exception)
            {
                WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel("ErrorEmail"));
            }
            finally
            {
                Cursor = CursorCollection.GetCursor();
            }
        }
        private void SendVerificationMessage(string name, string email)
        {
            Cursor = CursorCollection.GetCursor("loading");
            Random rnd = new Random();
            code = rnd.Next(10000, 99999);
            MessageConstructor messageConstructor = new MessageConstructor(Application.Current.Resources["Recovery"].ToString() ?? "", email);
            messageConstructor.GenerateRecoverCodeMessage(name, code);
            messageConstructor.SendMessage();
        }
        private void SwitchLang()
        {
            if (LangCheck)
                ToEnglish();
            else
                ToRussian();
        }
        private void ToRussian()
        {
            Application.Current.Resources.MergedDictionaries.Remove(Settings.ResourceEnLang);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceRusLang);
            Settings.Lang = Settings.Languages.RU;
        }
        private void Register()
        {
            WindowService.OpenWindow(WindowService.Windows.UserDialog, new UserDialogViewModel());
        }

        private void ToEnglish()
        {
            Application.Current.Resources.MergedDictionaries.Remove(Settings.ResourceRusLang);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceEnLang);
            Settings.Lang = Settings.Languages.EN;
        }
    }
}
