using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace JewelleryStore
{
    public class UserEnterViewModel : PropertyChangedNotification
    {
        int code;
        public static bool DialogResult;
        public static int userId;
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand CheckCommand { get; set; }
        public string Message
        {
            get { return GetValue(() => Message); }
            set { SetValue(() => Message, value); }
        }
        public string UserMessage
        {
            get { return GetValue(() => UserMessage); }
            set { SetValue(() => UserMessage, value); }
        }
        public Cursor Cursor
        {
            get { return GetValue(() => Cursor); }
            set { SetValue(() => Cursor, value); }
        }
        public string ErrorMessage
        {
            get { return GetValue(() => ErrorMessage); }
            set { SetValue(() => ErrorMessage, value); }
        }
        public UserEnterViewModel(string message, int code = 0, string email = "")
        {
            Cursor = CursorCollection.GetCursor();
            DialogResult = false;
            userId = -1;
            this.code = code;
            UserMessage = "";
            CloseCommand = new RelayCommand(Close);
            CheckCommand = new RelayCommand(Check);
            switch (message)
            {
                case "EnterCode":
                    Message = Settings.FindMessage("EnterCode") + " " + email ?? "";
                    break;
                default:
                        Message = Settings.FindMessage(message);
                    break;
            }
        }
        private void Close()
        {
            WindowService.CloseWindow(WindowService.Windows.UserEnter);
        }
        private void Check()
        {
            try
            {
                if (code != 0)
                {
                    int userCode;
                    try
                    {
                        userCode = int.Parse(UserMessage);
                    }
                    catch (FormatException)
                    {
                        throw new ArgumentException("ErrorVerificationCode");
                    }

                    if (userCode == code)
                    {
                        DialogResult = true;
                        Cursor = CursorCollection.GetCursor("loading");
                        Close();
                    }
                    else
                        throw new Exception("ErrorVerificationCode");
                }
                else
                {
                    try
                    {
                        if (UserMessage.Length == 0)
                            throw new FormatException();
                        MailAddress m = new MailAddress(UserMessage);
                    }
                    catch(FormatException)
                    {
                        throw new ArgumentException("ErrorEmail");
                    }
                    DatabaseUnit db = new DatabaseUnit();
                    User user = db.Users.GetAll().Where(u => u.Email == UserMessage).FirstOrDefault();
                    if (user != null)
                    {
                        DialogResult = true;
                        userId = user.UserId;
                        Cursor = CursorCollection.GetCursor("loading");
                        Close();
                    }
                    else
                        throw new ArgumentException("UserDoesNotExist");
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = Settings.FindMessage(ex.Message);
            }
        }
    }
}
