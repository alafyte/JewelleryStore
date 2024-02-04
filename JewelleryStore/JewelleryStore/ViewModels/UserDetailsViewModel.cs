using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class UserDetailsViewModel : PropertyChangedNotification
    {
        public static int id;
        public static event Action nameChanged;
        public RelayCommand EditCommand { get; set; }
        public string Name
        {
            get { return GetValue(() => Name); }
            set { SetValue(() => Name, value); }
        }
        public string LastName
        {
            get { return GetValue(() => LastName); }
            set { SetValue(() => LastName, value); }
        }
        public string Login
        {
            get { return GetValue(() => Login); }
            set { SetValue(() => Login, value); }
        }
        public string Email
        {
            get { return GetValue(() => Email); }
            set { SetValue(() => Email, value); }
        }
        public string DateBirth
        {
            get { return GetValue(() => DateBirth); }
            set { SetValue(() => DateBirth, value); }
        }
        public ObservableCollection<BillState> Bills
        {
            get { return GetValue(() => Bills); }
            set { SetValue(() => Bills, value); }
        }
        public UserDetailsViewModel() 
        {
            LoadUser();
            EditCommand = new RelayCommand(EditData);
        }
        private void LoadUser()
        {
            DatabaseUnit db = new DatabaseUnit();
            User user = db.Users.Get(id);
            Name = user.GivenName;
            LastName = user.LastName;
            Login = user.NickName;
            Email = user.Email;
            DateBirth = user.DateOfBirth.ToShortDateString();
            LoadOrders();
            Settings.changeLang += SetLocale;
            nameChanged?.Invoke();
        }
        private void LoadOrders()
        {
            DatabaseUnit db = new DatabaseUnit();
            Bills = new ObservableCollection<BillState>();
            foreach (var bill in db.Bills.GetAll().Where(b => b.UserId == id).OrderByDescending(b => b.DateOfOrder))
            {
                BillState bs = new BillState();
                bs.Bill = bill;
                bs.OrderInfos = new ObservableCollection<OrderInfoState>();
                foreach(var o in bill.OrderInfos)
                {
                    OrderInfoState os = new OrderInfoState();
                    os.Pimage = Settings.projectPath + "/images/" + o.ProductCodeNavigation.Pimage;
                    switch(Settings.Lang)
                    {
                        case Settings.Languages.RU:
                            os.Pname = db.Dictionary.Get(o.ProductCodeNavigation.Pname).WordRus;
                            break;
                        case Settings.Languages.EN:
                            os.Pname = db.Dictionary.Get(o.ProductCodeNavigation.Pname).WordEn;
                            break;
                    }
                    os.PNameid = o.ProductCodeNavigation.Pname;
                    os.Quantity = o.Quantity;
                    os.Price = o.ProductCodeNavigation.Price;
                    bs.OrderInfos.Add(os);
                }
                Bills.Add(bs);
            }
        }
        private void SetLocale()
        {
            DatabaseUnit db = new DatabaseUnit();
            foreach (var bill in Bills)
            {
                foreach (var o in bill.OrderInfos)
                {
                    switch (Settings.Lang)
                    {
                        case Settings.Languages.RU:
                            o.Pname = db.Dictionary.Get(o.PNameid).WordRus;
                            break;
                        case Settings.Languages.EN:
                            o.Pname = db.Dictionary.Get(o.PNameid).WordEn;
                            break;
                    }
                }
            }
        }
        private void EditData()
        {
            DatabaseUnit db = new DatabaseUnit();
            WindowService.OpenWindow(WindowService.Windows.UserEditDialog, new EditUserViewModel(id), (s, e) => LoadUser());
        }
    }
}
