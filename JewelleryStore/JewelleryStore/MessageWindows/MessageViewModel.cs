using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace JewelleryStore
{
    public class MessageViewModel : PropertyChangedNotification
    {
        bool loadingFlag;
        public static bool DialogResult;
        public RelayCommandParam CloseCommand { get; set; }
        public string Message
        {
            get { return GetValue(() => Message); }
            set { SetValue(() => Message, value); }
        }
        public Cursor Cursor
        {
            get { return GetValue(() => Cursor); }
            set { SetValue(() => Cursor, value); }
        }
        public int FontSize
        {
            get { return GetValue(() => FontSize); }
            set { SetValue(() => FontSize, value); }
        }
        public bool CancelButVis
        {
            get { return GetValue(() => CancelButVis); }
            set { SetValue(() => CancelButVis, value); }
        }
        public MessageViewModel(string message, bool twoButtonsMessage = false, bool loadingFlag = false)
        {
            Cursor = CursorCollection.GetCursor();
            CloseCommand = new RelayCommandParam((res) => Close(res.ToString()));
            if (twoButtonsMessage)
            {
                CancelButVis = true;
            }

            Message = Settings.FindMessage(message);
            this.loadingFlag = loadingFlag;
        }
        private void Close(string res)
        {
            DialogResult = bool.Parse(res);
            if (loadingFlag)
                Cursor = CursorCollection.GetCursor("loading");
            WindowService.CloseWindow(WindowService.Windows.Message);
        }
    }
}
