using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace JewelleryStore
{
    public class ProductCheckoutViewModel : PropertyChangedNotification
    {
        DatabaseUnit db = new DatabaseUnit();
        Product product;
        Bill bill;
        public event Action ItemQuantityChanged;
        public Action RemoveItemEvent { get; set; }
        public RelayCommand AddOneCommand { get; set; }
        public RelayCommand RemoveOneCommand { get; set; }
        public RelayCommand RemoveItemCommand { get; set; }
        public OrderInfo item
        {
            get { return GetValue(() => item); }
            set { SetValue(() => item, value); }
        }
        public Cursor Cursor 
        { 
            get { return GetValue(() => Cursor); }
            set { SetValue(() => Cursor, value); }
        }
        public string ProductPhoto
        {
            get { return GetValue(() => ProductPhoto); }
            set { SetValue(() => ProductPhoto, value); }
        }
        public string ProductName
        {
            get { return GetValue(() => ProductName); }
            set { SetValue(() => ProductName, value); }
        }
        public int Quantity
        {
            get { return GetValue(() => Quantity); }
            set { SetValue(() => Quantity, value); }
        }
        public string Price
        {
            get { return GetValue(() => Price); }
            set { SetValue(() => Price, value); }
        }
        public ProductCheckoutViewModel()
        {
            Cursor = CursorCollection.GetCursor();
            Settings.changeLang += ChangeLang;
        }
        public ProductCheckoutViewModel(Bill bill, OrderInfo item)
        {
            Cursor = CursorCollection.GetCursor();
            Settings.changeLang += ChangeLang;

            this.item = item;
            this.bill = bill;
            product = db.Products.Get(item.ProductCode);
            ProductPhoto = Settings.projectPath + "/images/" + product.Pimage;
            switch (Settings.Lang)
            {
                case Settings.Languages.EN:
                    ProductName = db.Dictionary.Get(product.Pname).WordEn;
                    break;
                case Settings.Languages.RU:
                    ProductName = db.Dictionary.Get(product.Pname).WordRus;
                    break;
            }
            Quantity = item.Quantity;
            Price = (item.Quantity * product.Price).ToString() + "$";
            AddOneCommand = new RelayCommand(AddOne);
            RemoveOneCommand = new RelayCommand(RemoveOne);
            RemoveItemCommand = new RelayCommand(RemoveItem);
        }
        private void ChangeLang()
        {
            switch (Settings.Lang)
            {
                case Settings.Languages.EN:
                    ProductName = db.Dictionary.Get(product.Pname).WordEn;
                    break;
                case Settings.Languages.RU:
                    ProductName = db.Dictionary.Get(product.Pname).WordRus;
                    break;
            }
        }
        private void RemoveOne()
        {
            if (item.Quantity > 0)
            {
                item.Quantity -= 1;
                item.Price = item.Quantity * product.Price;
                Quantity = item.Quantity;
                Price = (item.Quantity * product.Price).ToString() + "$";
            }
            if (item.Quantity == 0)
            {
                RemoveItem();
            }
            ItemQuantityChanged?.Invoke();
        }

        private void AddOne()
        {
            try
            {
                if (item.Quantity + 1 > db.Products.Get(item.ProductCode).Quantity)
                {
                    throw new ArgumentException("QuantityOver");
                }
                item.Quantity += 1;
                Quantity = item.Quantity;
                item.Price = item.Quantity * product.Price;
                Price = (item.Quantity * product.Price).ToString() + "$";
                ItemQuantityChanged?.Invoke();
            }
            catch(ArgumentException ex)
            {
                WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel(ex.Message));
            }
        }
        private void RemoveItem()
        {
            bill.OrderInfos.Remove(item);
            ItemQuantityChanged?.Invoke();
        }
    }
}
