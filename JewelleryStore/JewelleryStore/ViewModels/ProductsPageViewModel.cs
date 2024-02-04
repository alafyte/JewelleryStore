using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace JewelleryStore
{
    public class ProductsPageViewModel : PropertyChangedNotification
    {
        public RelayCommandParam SelectItemCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public Cursor Cursor { get; set; }
        public Button? SelectedItem
        {
            get { return GetValue(() => SelectedItem); }
            set { SetValue(() => SelectedItem, value); }
        }
        public string SearchKey
        {
            get { return GetValue(() => SearchKey); }
            set
            {
                if (value is null)
                    SetValue(() => SearchKey, "");
                else
                    SetValue(() => SearchKey, value);
                Search(SearchKey);
            }
        }
        public bool DeleteButtonEnabled
        {
            get { return GetValue(() => DeleteButtonEnabled); }
            set { SetValue(() => DeleteButtonEnabled, value); }
        }
        public bool NothingFoundVisibility
        {
            get { return GetValue(() => NothingFoundVisibility); }
            set { SetValue(() => NothingFoundVisibility, value); }
        }
        public bool EditButtonEnabled
        {
            get { return GetValue(() => EditButtonEnabled); }
            set { SetValue(() => EditButtonEnabled, value); }
        }
        public bool ShowActiveOnly
        {
            get { return GetValue(() => ShowActiveOnly); }
            set 
            { 
                SetValue(() => ShowActiveOnly, value);
                ChangeProductsVisibility();
            }
        }
        public ObservableCollection<ProductState> Products 
        { 
            get { return GetValue(() => Products); }
            set { SetValue(() =>  Products, value); }
        }
        public ObservableCollection<string> SortOptions
        {
            get { return GetValue(() => SortOptions); }
            set { SetValue(() => SortOptions, value); }
        }
        public ObservableCollection<string> TypeOptions
        {
            get { return GetValue(() => TypeOptions); }
            set { SetValue(() => TypeOptions, value); }
        }
        public ObservableCollection<string> MetalOptions
        {
            get { return GetValue(() => MetalOptions); }
            set { SetValue(() => MetalOptions, value); }
        }
        public ObservableCollection<string> InsertOptions
        {
            get { return GetValue(() => InsertOptions); }
            set { SetValue(() => InsertOptions, value); }
        }
        public int SelectedPriceOption
        {
            get { return GetValue(() => SelectedPriceOption); }
            set 
            { 
                SetValue(() => SelectedPriceOption, value);
                switch (SelectedPriceOption)
                {
                    case -1:
                        break;
                    case 0:
                        ShowAll();
                        SelectedTypeOption = -1;
                        SelectedWeightOption = -1;
                        SelectedInsertOption = -1;
                        SelectedMetalOption = -1;
                        break;
                    default:
                        ShowFilterPrice(SelectedPriceOption);
                        SelectedTypeOption = -1;
                        SelectedWeightOption = -1;
                        SelectedMetalOption = -1;
                        SelectedInsertOption = -1;
                        break;
                }
            }
        }
        public int SelectedTypeOption
        {
            get { return GetValue(() => SelectedTypeOption); }
            set 
            { 
                SetValue(() => SelectedTypeOption, value); 
                switch(SelectedTypeOption)
                {
                    case -1:
                        break;
                    case 0:
                        ShowAll();
                        SelectedWeightOption = -1;
                        SelectedPriceOption = -1;
                        SelectedMetalOption = -1;
                        SelectedInsertOption = -1;
                        break;
                    default: 
                        ShowFilterType(SelectedTypeOption);
                        SelectedWeightOption = -1;
                        SelectedPriceOption = -1;
                        SelectedMetalOption = -1;
                        SelectedInsertOption = -1;
                        break;
                }
            }
        }
        public int SelectedWeightOption
        {
            get { return GetValue(() => SelectedWeightOption); }
            set 
            { 
                SetValue(() => SelectedWeightOption, value);
                switch (SelectedWeightOption)
                {
                    case -1:
                        break;
                    case 0:
                        ShowAll();
                        SelectedTypeOption = -1;
                        SelectedPriceOption = -1;
                        SelectedMetalOption = -1;
                        SelectedInsertOption = -1;
                        break;
                    default:
                        ShowFilterWeight(SelectedWeightOption);
                        SelectedTypeOption = -1;
                        SelectedPriceOption = -1;
                        SelectedMetalOption = -1;
                        SelectedInsertOption = -1;
                        break;
                }
            }
        }
        public int SelectedMetalOption
        {
            get { return GetValue(() => SelectedMetalOption); }
            set
            {
                SetValue(() => SelectedMetalOption, value);
                switch (SelectedMetalOption)
                {
                    case -1:
                        break;
                    case 0:
                        ShowAll();
                        SelectedTypeOption = -1;
                        SelectedPriceOption = -1;
                        SelectedWeightOption = -1;
                        SelectedInsertOption = -1;
                        break;
                    default:
                        ShowFilterMetal(SelectedMetalOption);
                        SelectedTypeOption = -1;
                        SelectedPriceOption = -1;
                        SelectedWeightOption = -1;
                        SelectedInsertOption = -1;
                        break;
                }
            }
        }
        public int SelectedInsertOption
        {
            get { return GetValue(() => SelectedInsertOption); }
            set
            {
                SetValue(() => SelectedInsertOption, value);
                switch (SelectedInsertOption)
                {
                    case -1:
                        break;
                    case 0:
                        ShowAll();
                        SelectedTypeOption = -1;
                        SelectedPriceOption = -1;
                        SelectedWeightOption = -1;
                        SelectedMetalOption = -1;
                        break;
                    default:
                        ShowFilterInsert(SelectedInsertOption);
                        SelectedTypeOption = -1;
                        SelectedPriceOption = -1;
                        SelectedWeightOption = -1;
                        SelectedMetalOption = -1;
                        break;
                }
            }
        }
        public ProductsPageViewModel()
        {
            Cursor = CursorCollection.GetCursor();
            SelectItemCommand = new RelayCommandParam((product) => SelectItem((Button)product));
            DeleteCommand = new RelayCommand(DeleteProduct);
            AddCommand = new RelayCommand(AddProduct);
            EditCommand = new RelayCommand(EditProduct);
            SetLocale();
            ShowAll();
            Settings.changeLang += SetLocale;
            Settings.changeLang += ShowAll;

            ResetSort();
        }
        private void ResetSort()
        {
            SelectedPriceOption = -1;
            SelectedTypeOption = -1;
            SelectedWeightOption = -1;
            SelectedMetalOption = -1;
            SelectedInsertOption = -1;
        }
        private void SetLocale()
        {
            SortOptions = new ObservableCollection<string>()
            {
                Application.Current.FindResource("NoSort").ToString(),
                Application.Current.FindResource("Ascending").ToString(),
                Application.Current.FindResource("Descending").ToString()
            };
            TypeOptions = new ObservableCollection<string>()
            {
                Application.Current.FindResource("NoSort").ToString(),
                Application.Current.FindResource("Necklaces").ToString(),
                Application.Current.FindResource("Rings").ToString(),
                Application.Current.FindResource("Earrings").ToString(),
                Application.Current.FindResource("Wristwear").ToString()
            };
            DatabaseUnit db = new DatabaseUnit();
            switch (Settings.Lang)
            {
                case Settings.Languages.EN:
                    MetalOptions = new ObservableCollection<string>(db.Metals.GetAll().Select(m => m.MetalNameEn).ToList());
                    MetalOptions.Insert(0, Application.Current.FindResource("NoSort").ToString());
                    InsertOptions = new ObservableCollection<string>(db.Stones.GetAll().Select(s => s.StoneNameEn).ToList());
                    InsertOptions.Insert(0, Application.Current.FindResource("NoSort").ToString());
                    break;
                case Settings.Languages.RU:
                    MetalOptions = new ObservableCollection<string>(db.Metals.GetAll().Select(m => m.MetalNameRus).ToList());
                    MetalOptions.Insert(0, Application.Current.FindResource("NoSort").ToString());
                    InsertOptions = new ObservableCollection<string>(db.Stones.GetAll().Select(s => s.StoneNameRus).ToList());
                    InsertOptions.Insert(0, Application.Current.FindResource("NoSort").ToString());
                    break;
            }
        }
        private void ShowAll()
        {
            DatabaseUnit db = new DatabaseUnit();
            Products = new ObservableCollection<ProductState>();
            foreach (var p in db.Products.GetAll())
            {

                ProductState state = new ProductState();
                switch (Settings.Lang)
                {
                    case Settings.Languages.RU:
                        state.Name = db.Dictionary.Get(p.Pname).WordRus;
                        break;
                    case Settings.Languages.EN:
                        state.Name = db.Dictionary.Get(p.Pname).WordEn;
                        break;
                }
                state.Id = p.ProductCode;
                state.Image = Settings.projectPath + "/images/" + p.Pimage;
                state.Price = p.Price.ToString() + "$";
                state.IsActive = p.IsActive;
                state.CheckActive(ShowActiveOnly);
                Products.Add(state);

            }
            ResetSort();
        }
        private void ShowFilter(string key)
        {
            ResetSort();
            DatabaseUnit db = new DatabaseUnit();
            Products.Clear();
            foreach (var p in db.Products.GetAll())
            {
                var keyRus = db.Dictionary.Get(p.Pname).WordRus;
                var keyEn = db.Dictionary.Get(p.Pname).WordEn;
                if (keyRus.ToUpper().Contains(key.ToUpper()) || keyEn.ToUpper().Contains(key.ToUpper()))
                {
                    ProductState state = new ProductState();
                    switch (Settings.Lang)
                    {
                        case Settings.Languages.RU:
                            state.Name = keyRus;
                            break;
                        case Settings.Languages.EN:
                            state.Name = keyEn;
                            break;
                    }
                    state.Id = p.ProductCode;
                    state.Image = Settings.projectPath + "/images/" + p.Pimage;
                    state.Price = p.Price.ToString() + "$";
                    state.IsActive = p.IsActive;
                    state.CheckActive(ShowActiveOnly);
                    Products.Add(state);
                }
            }
            if (Products.Count() == 0)
                NothingFoundVisibility = true;
            else
                NothingFoundVisibility = false;
        }
        private void ShowFilterInsert(int id)
        {
            DatabaseUnit db = new DatabaseUnit();
            Products.Clear();
            foreach (var p in db.Products.GetAll())
            {
                if (p.StoneInsert == id)
                {
                    ProductState state = new ProductState();
                    switch (Settings.Lang)
                    {
                        case Settings.Languages.RU:
                            state.Name = db.Dictionary.Get(p.Pname).WordRus;
                            break;
                        case Settings.Languages.EN:
                            state.Name = db.Dictionary.Get(p.Pname).WordEn;
                            break;
                    }
                    state.Id = p.ProductCode;
                    state.Image = Settings.projectPath + "/images/" + p.Pimage;
                    state.Price = p.Price.ToString() + "$";
                    state.IsActive = p.IsActive;
                    state.CheckActive(ShowActiveOnly);
                    Products.Add(state);
                }
            }
            if (Products.Count() == 0)
                NothingFoundVisibility = true;
            else
                NothingFoundVisibility = false;
        }
        private void ShowFilterWeight(int sortType)
        {
            DatabaseUnit db = new DatabaseUnit();
            Products.Clear();
            List<Product> products = db.Products.GetAll();

            switch (sortType)
            {
                case 1:
                    products = products.OrderBy(p => p.Pweight).ToList();
                    break;
                case 2:
                    products = products.OrderByDescending(p => p.Pweight).ToList();
                    break;
            }

            foreach (var p in products)
            {

                ProductState state = new ProductState();
                switch (Settings.Lang)
                {
                    case Settings.Languages.RU:
                        state.Name = db.Dictionary.Get(p.Pname).WordRus;
                        break;
                    case Settings.Languages.EN:
                        state.Name = db.Dictionary.Get(p.Pname).WordEn;
                        break;
                }
                state.Id = p.ProductCode;
                state.Image = Settings.projectPath + "/images/" + p.Pimage;
                state.Price = p.Price.ToString() + "$";
                state.IsActive = p.IsActive;
                state.CheckActive(ShowActiveOnly);
                Products.Add(state);

            }
            if (Products.Count() == 0)
                NothingFoundVisibility = true;
            else
                NothingFoundVisibility = false;
        }
        private void ShowFilterType(int id)
        {
            DatabaseUnit db = new DatabaseUnit();
            Products.Clear();
            foreach (var p in db.Products.GetAll())
            {
                if (p.ProductType == id)
                {
                    ProductState state = new ProductState();
                    switch (Settings.Lang)
                    {
                        case Settings.Languages.RU:
                            state.Name = db.Dictionary.Get(p.Pname).WordRus;
                            break;
                        case Settings.Languages.EN:
                            state.Name = db.Dictionary.Get(p.Pname).WordEn;
                            break;
                    }
                    state.Id = p.ProductCode;
                    state.Image = Settings.projectPath + "/images/" + p.Pimage;
                    state.Price = p.Price.ToString() + "$";
                    state.IsActive = p.IsActive;
                    state.CheckActive(ShowActiveOnly);
                    Products.Add(state);
                }
            }
            if (Products.Count() == 0)
                NothingFoundVisibility = true;
            else
                NothingFoundVisibility = false;
        }
        private void ShowFilterMetal(int id)
        {
            DatabaseUnit db = new DatabaseUnit();
            Products.Clear();
            foreach (var p in db.Products.GetAll())
            {
                if (p.Metal == id)
                {
                    ProductState state = new ProductState();
                    switch (Settings.Lang)
                    {
                        case Settings.Languages.RU:
                            state.Name = db.Dictionary.Get(p.Pname).WordRus;
                            break;
                        case Settings.Languages.EN:
                            state.Name = db.Dictionary.Get(p.Pname).WordEn;
                            break;
                    }
                    state.Id = p.ProductCode;
                    state.Image = Settings.projectPath + "/images/" + p.Pimage;
                    state.Price = p.Price.ToString() + "$";
                    state.IsActive = p.IsActive;
                    state.CheckActive(ShowActiveOnly);
                    Products.Add(state);
                }
            }
            if (Products.Count() == 0)
                NothingFoundVisibility = true;
            else
                NothingFoundVisibility = false;
        }
        private void ShowFilterPrice(int sortType) 
        {
            DatabaseUnit db = new DatabaseUnit();
            List<Product> products = db.Products.GetAll();
            Products.Clear();

            switch (sortType)
            {
                case 1:
                    products = products.OrderBy(p => p.Price).ToList();
                    break; 
                case 2:
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;
            }

            foreach (var p in products)
            {
                ProductState state = new ProductState();
                switch (Settings.Lang)
                {
                    case Settings.Languages.RU:
                        state.Name = db.Dictionary.Get(p.Pname).WordRus;
                        break;
                    case Settings.Languages.EN:
                        state.Name = db.Dictionary.Get(p.Pname).WordEn;
                        break;
                }
                state.Id = p.ProductCode;
                state.Image = Settings.projectPath + "/images/" + p.Pimage;
                state.Price = p.Price.ToString() + "$";
                state.IsActive = p.IsActive;
                state.CheckActive(ShowActiveOnly);
                Products.Add(state);

            }
            if (Products.Count() == 0)
                NothingFoundVisibility = true;
            else
                NothingFoundVisibility = false;
        }
        private void Refresh()
        {
            ShowAll();
        }
        private void Search(string key)
        {
            ShowFilter(key);
            if (key.Length == 0)
                ShowAll();
        }
        private void SelectItem(Button Product)
        {
            if (Product.Equals(SelectedItem))
            {
                Product.Background = Brushes.Transparent;
                SelectedItem = null;
                DeleteButtonEnabled = false;
                EditButtonEnabled = false;
            }
            else if (SelectedItem != null)
            {
                SelectedItem.Background = Brushes.Transparent;
                BrushConverter bc = new BrushConverter();
                Product.Background = bc.ConvertFrom("#D6ECE6") as Brush;
                SelectedItem = Product;
                DeleteButtonEnabled = true;
                EditButtonEnabled = true;
            }
            else
            {
                BrushConverter bc = new BrushConverter();
                Product.Background = bc.ConvertFrom("#D6ECE6") as Brush;
                SelectedItem = Product;
                DeleteButtonEnabled = true;
                EditButtonEnabled = true;
            }

        }
        private void DeleteProduct()
        {
            DatabaseUnit db = new DatabaseUnit();
            string btn = SelectedItem.Name.ToString();
            int id = int.Parse(btn.Remove(0, 3));
            try
            {
                db.Products.Delete(id);
                WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel("Success"));
                ShowAll();
                DeleteButtonEnabled = false;
                EditButtonEnabled = false;
            }
            catch(InvalidOperationException ex)
            {
                WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel(ex.Message));
            }
        }

        private void AddProduct()
        {
            WindowService.OpenWindow(WindowService.Windows.ProductDialog, new ProductDialogViewModel(), (s, ee) => Refresh());
        }

        private void EditProduct()
        {
            string btn = SelectedItem.Name.ToString();
            int id = int.Parse(btn.Remove(0, 3));
            WindowService.OpenWindow(WindowService.Windows.ProductDialog, new ProductDialogViewModel(id), (s, ee) => Refresh());
        }
       
        private void ChangeProductsVisibility()
        {
            foreach(var item in Products)
            {
                item.CheckActive(ShowActiveOnly);
            }
        }
    }
}
