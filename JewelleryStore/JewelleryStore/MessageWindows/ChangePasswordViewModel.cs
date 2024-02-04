using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JewelleryStore
{
    public class ChangePasswordViewModel : PropertyChangedNotification
    {
        int userId;
        public static bool DialogResult;
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand CheckPassCommand { get; set; }
        public Cursor Cursor
        {
            get { return GetValue(() => Cursor); }
            set { SetValue(() => Cursor, value); }
        }
        public string Password1
        {
            get { return GetValue(() => Password1); }
            set { SetValue(() => Password1, value); }
        }
        public string Password2
        {
            get { return GetValue(() => Password2); }
            set { SetValue(() => Password2, value); }
        }
        public string ErrorMessage
        {
            get { return GetValue(() => ErrorMessage); }
            set { SetValue(() => ErrorMessage, value); }
        }

        public ChangePasswordViewModel(int id)
        {
            Cursor = CursorCollection.GetCursor();
            Password1 = "";
            Password2 = "";
            userId = id;
            CloseCommand = new RelayCommand(Close);
            CheckPassCommand = new RelayCommand(CheckPass);
        }
        private void Close()
        {
            WindowService.CloseWindow(WindowService.Windows.ChangePass);
        }

        private void CheckPass()
        {
            try
            {
                DatabaseUnit db = new DatabaseUnit();
                User us = db.Users.Get(userId);
                Regex regex = new Regex(@"^[(a-z|а-я)(A-z|А-Я)0-9]{8,15}$");
                if (Password1.Length == 0 || Password2.Length == 0 || !regex.IsMatch(Password1))
                {
                    Password1 = "";
                    Password2 = "";
                    throw new ArgumentException("ErrorPassword");
                }
                else if (!Password1.Equals(Password2))
                {
                    Password1 = "";
                    Password2 = "";
                    throw new ArgumentException("PasswordMismatch");
                }
                else
                {
                    SaltedHash pass = new SaltedHash(Password1);
                    us.UserPasswordHash = pass.Hash;
                    us.UserPasswordSalt = pass.Salt;
                }
                db.Users.Update(us);
                DialogResult = true;
                Close();
            }
            catch (ArgumentException ex)
            {
                ErrorMessage = Settings.FindMessage(ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMessage = Settings.FindMessage(ex.Message);
            }
        }
    }
}
