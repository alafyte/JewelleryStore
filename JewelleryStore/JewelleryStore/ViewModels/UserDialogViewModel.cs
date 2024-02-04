using EmailService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace JewelleryStore
{
    public class UserDialogViewModel : PropertyChangedNotification
    {
        string access;
        int code;
        User user;
        DatabaseUnit db = new DatabaseUnit();
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand AddUserCommand { get; set; }
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
        public string Password1
        {
            get { return GetValue(() => Password1); }
            set 
            { 
                SetValue(() => Password1, value);
                ErrorPassword = String.Empty;
            }
        }
        public string Password2
        {
            get { return GetValue(() => Password2); }
            set 
            { 
                SetValue(() => Password2, value);
                ErrorPassword = String.Empty;
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
        public string ErrorPassword
        {
            get { return GetValue(() => ErrorPassword); }
            set { SetValue(() => ErrorPassword, value); }
        }
        public string ErrorEmail
        {
            get { return GetValue(() => ErrorEmail); }
            set { SetValue(() => ErrorEmail, value); }
        }
        #endregion

        public UserDialogViewModel(string access = "user") 
        {
            CloseCommand = new RelayCommand(Close);
            AddUserCommand = new RelayCommand(AddUser);
            this.access = access;
            DateOfBirth = DateTime.Now;
            Password1 = string.Empty;
            Password2 = string.Empty;
            Name = string.Empty;
            LastName = string.Empty;
            Login = string.Empty;
            Email = string.Empty;
        }
        private void Close()
        {
            WindowService.CloseWindow(WindowService.Windows.UserDialog);
        }
        private void AddUser()
        {
            try
            {
                Basket basket = new Basket();
                Favorite favorite = new Favorite();
                user = new User();
                #region Name
                if (Name.Length == 0)
                    ErrorNameVisibility = true;
                else
                    user.GivenName = Name;
                #endregion
                #region Email
                if (Email.Length == 0)
                    ErrorEmail = Settings.FindMessage("ErrorEmail");
                #endregion
                #region LastName
                if (LastName.Length == 0)
                    ErrorLastNameVisibility = true;
                else
                    user.LastName = LastName;
                #endregion
                #region Login
                if (Login.Length == 0)
                {
                    ErrorLogin = Settings.FindMessage("ErrorLogin");
                }
                else if (db.Users.GetAll().Where(u => u.NickName == Login).Count() != 0)
                    throw new ArgumentException("ErrorLoginExists");
                else
                    user.NickName = Login;
                #endregion
                #region Passwords
                Regex regex = new Regex(@"^[(a-z|а-я)(A-z|А-Я)0-9]{8,15}$");
                if (!Password1.Equals(Password2))
                {
                    Password1 = "";
                    Password2 = "";
                    throw new ArgumentException("PasswordMismatch");
                }
                else if (Password1.Length == 0 || Password2.Length == 0 || !regex.IsMatch(Password1))
                {
                    Password1 = "";
                    Password2 = "";
                    throw new ArgumentException("ErrorPassword");
                }
                else
                {
                    SaltedHash pass = new SaltedHash(Password1);
                    user.UserPasswordHash = pass.Hash;
                    user.UserPasswordSalt = pass.Salt;
                }
                #endregion
                #region Email
                if (db.Users.GetAll().Where(u => u.Email == Email).Count() != 0)
                    throw new ArgumentException("ErrorEmailInUse");
                else
                    user.Email = Email;
                #endregion
                user.DateOfBirth = DateOfBirth;
                user.Access = access;
                user.IsActive = true;
                user.Theme = "Theme1";
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var context = new ValidationContext(user);
                if (!Validator.TryValidateObject(user, context, results, true))
                {
                    WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel(results.First().ToString()));
                }
                else
                {
                    SendVerificationEmail(user.Email);
                    WindowService.OpenWindow(WindowService.Windows.UserEnter, new UserEnterViewModel("EnterCode", code, user.Email));
                    if (UserEnterViewModel.DialogResult)
                    {
                        db.Users.Add(user);
                        basket.UserId = user.UserId;
                        db.Baskets.Add(basket);
                        favorite.UserId = user.UserId;
                        db.Favorites.Add(favorite);
                        WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel("Success"));
                        Close();
                    }
                }
            }
            catch(Exception ex)
            {
                switch(ex.Message)
                {
                    case "ErrorName":
                        ErrorNameVisibility = true;
                        break;
                    case "ErrorLoginExists":
                        ErrorLogin = Settings.FindMessage("ErrorLoginExists");
                        break;
                    case "ErrorPassword":
                        ErrorPassword = Settings.FindMessage("ErrorPassword");
                        break;
                    case "PasswordMismatch":
                        ErrorPassword = Settings.FindMessage("PasswordMismatch");
                        break;
                    case "ErrorEmailInUse":
                        ErrorEmail = Settings.FindMessage("ErrorEmailInUse");
                        break;
                }
            }
        }
        private void SendVerificationEmail(string email)
        {
            Random rnd = new Random();
            code = rnd.Next(10000, 99999);
            MessageConstructor messageConstructor = new MessageConstructor(Application.Current.Resources["Register"].ToString() ?? "", email);
            messageConstructor.GenerateRegistrationCodeMessage(Name, code);
            messageConstructor.SendMessage();
        }
    }
}
