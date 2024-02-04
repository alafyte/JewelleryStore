using EmailService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace JewelleryStore
{
    public class EditUserViewModel : PropertyChangedNotification
    {
        DatabaseUnit db = new DatabaseUnit();
        User user;
        int code;
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand EditUserCommand { get; set; }
        public string Name
        {
            get { return GetValue(() => Name); }
            set 
            { 
                SetValue(() => Name, value);
                ErrorNameVisibility = false;
            }
        }
        public string LastName
        {
            get { return GetValue(() => LastName); }
            set 
            { 
                SetValue(() => LastName, value); 
                ErrorLastNameVisibility = false;
            }
        }
        public string Login
        {
            get { return GetValue(() => Login); }
            set 
            { 
                SetValue(() => Login, value); 
                ErrorLogin = string.Empty;
            }
        }
        public string Email
        {
            get { return GetValue(() => Email); }
            set 
            { 
                SetValue(() => Email, value); 
                ErrorEmail = string.Empty;
            }
        }
        public DateTime DateOfBirth
        {
            get { return GetValue(() => DateOfBirth); }
            set { SetValue(() => DateOfBirth, value); }
        }
        #region Errors
        public bool ErrorNameVisibility
        {
            get { return GetValue(() => ErrorNameVisibility); }
            set { SetValue(() => ErrorNameVisibility, value); }
        }
        public bool ErrorLastNameVisibility
        {
            get { return GetValue(() => ErrorLastNameVisibility); }
            set { SetValue(() => ErrorLastNameVisibility, value); }
        }
        public string ErrorLogin
        {
            get { return GetValue(() => ErrorLogin); }
            set { SetValue(() => ErrorLogin, value); }
        }
        public string ErrorEmail
        {
            get { return GetValue(() => ErrorEmail); }
            set { SetValue(() => ErrorEmail, value); }
        }
        #endregion

        public EditUserViewModel(int id) 
        {
            EditUserCommand = new RelayCommand(EditUserInfo);
            CloseCommand = new RelayCommand(Close);
            user = db.Users.Get(id);
            DateOfBirth = user.DateOfBirth;
            Name = user.GivenName;
            LastName = user.LastName;
            Email = user.Email;
            Login = user.NickName;
        }
        private void Close()
        {
            WindowService.CloseWindow(WindowService.Windows.UserEditDialog);
        }
        private void EditUserInfo()
        {
            try
            {
                bool emailChanged = false;
                #region Name
                if (Name.Length == 0)
                    ErrorNameVisibility = true;
                user.GivenName = Name;
                #endregion
                #region LastName
                if (LastName.Length == 0)
                    ErrorLastNameVisibility = true;
                user.LastName = LastName;
                #endregion
                #region Login
                if (Login.Length == 0)
                {
                    ErrorLogin = Settings.FindMessage("ErrorLogin");
                    user.NickName = Login;
                }
                else if (db.Users.GetAll().Where(u => u.NickName == Login && u.UserId != user.UserId).Count() != 0)
                    throw new ArgumentException("ErrorLoginExists");
                else
                    user.NickName = Login;
                #endregion
                #region Email
                if (Email.Length == 0)
                    throw new ArgumentException("ErrorEmail");
                else if (!user.Email.Equals(Email))
                {
                    emailChanged = true;
                }
                if (db.Users.GetAll().Where(u => u.Email == Email && u.UserId != user.UserId).Count() != 0)
                    throw new ArgumentException("ErrorEmailInUse");
                else
                    user.Email = Email;
                #endregion

                user.DateOfBirth = DateOfBirth;

                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var context = new ValidationContext(user);
                if (!Validator.TryValidateObject(user, context, results, true))
                {
                    WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel(results.First().ToString()));
                }
                else if (emailChanged)
                {
                    SendVerificationEmail(user.Email);
                    WindowService.OpenWindow(WindowService.Windows.UserEnter, new UserEnterViewModel("EnterCode", code, user.Email));
                    if (UserEnterViewModel.DialogResult)
                    {
                        db.Users.Update(user);
                        WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel("Success"));
                        Close();
                    }
                }
                else
                {
                    db.Users.Update(user);
                    WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel("Success"));
                    Close();
                }
            }
            catch (ArgumentException ex)
            {
                switch (ex.Message)
                {
                    case "ErrorName":
                        ErrorNameVisibility = true;
                        break;
                    case "ErrorLoginExists":
                        ErrorLogin = Settings.FindMessage("ErrorLoginExists");
                        break;
                    case "ErrorEmailInUse":
                        ErrorEmail = Settings.FindMessage("ErrorEmailInUse");
                        break;
                    case "ErrorEmail":
                        ErrorEmail = Settings.FindMessage("ErrorEmail");
                        break;
                }
            }
        }
        private void SendVerificationEmail(string email)
        {
            Random rnd = new Random();
            code = rnd.Next(10000, 99999);
            MessageConstructor messageConstructor = new MessageConstructor(Application.Current.Resources["ChangeEmail"].ToString() ?? "", email);
            messageConstructor.GenerateChangeEmailMessage(Name, code);
            messageConstructor.SendMessage();
        }
    }
}
