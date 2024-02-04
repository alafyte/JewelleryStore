using EmailService;
using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JewelleryStore
{
    public class MainWindowViewModel : PropertyChangedNotification
    {
        private Bill bill1 = new Bill();
        public static int userId;

        public RelayCommand CloseCommand { get; set; }
        public RelayCommand SwitchLangCommand { get; set; }
        public RelayCommandParam SwitchThemeCommand { get; set; }
        public RelayCommandParam SelectItemToCartCommand { get; set; }
        public RelayCommandParam SelectItemToFavoriteCommand { get; set; }
        public RelayCommandParam ViewDetailsCommand { get; set; }
        public RelayCommand ShowNecklacesCommand { get; set; }
        public RelayCommand ShowRingsCommand { get; set; }
        public RelayCommand ShowEarringsCommand { get; set; }
        public RelayCommand ShowWristwearCommand { get; set; }
        public RelayCommand ShowAllCommand { get; set; }
        public RelayCommand ShowAccountSettingsCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        public RelayCommand NextPageCommand { get; set; }
        public RelayCommand FavCommand { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand FinishCommand { get; set; }
        public RelayCommandParam RemoveCheckoutCommand { get; set; }

        public Cursor Cursor
        {
            get { return GetValue(() => Cursor); }
            set { SetValue(() => Cursor, value); }
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
        public bool NothingFoundVisibility
        {
            get { return GetValue(() => NothingFoundVisibility); }
            set { SetValue(() => NothingFoundVisibility, value); }
        }
        public string MainImageSource
        {
            get { return GetValue(() => MainImageSource); }
            set { SetValue(() => MainImageSource, value); }
        }
        public string UserGivenName
        {
            get { return GetValue(() => UserGivenName); }
            set { SetValue(() => UserGivenName, value); }
        }
        public bool NavbarVisibility
        {
            get { return GetValue(() => NavbarVisibility); }
            set { SetValue(() => NavbarVisibility, value); }
        }
        public bool ScrollbarVisibility
        {
            get { return GetValue(() => ScrollbarVisibility); }
            set { SetValue(() => ScrollbarVisibility, value); }
        }
        public bool CheckoutVisibility
        {
            get { return GetValue(() => CheckoutVisibility); }
            set { SetValue(() => CheckoutVisibility, value); }
        }
        public bool BackButtonVisibility
        {
            get { return GetValue(() => BackButtonVisibility); }
            set { SetValue(() => BackButtonVisibility, value); }
        }
        public bool FrameVisibility
        {
            get { return GetValue(() => FrameVisibility); }
            set { SetValue(() => FrameVisibility, value); }
        }
        public bool SearchFieldVisibility
        {
            get { return GetValue(() => SearchFieldVisibility); }
            set { SetValue(() => SearchFieldVisibility, value); }
        }
        public bool ToolBarVisibility
        {
            get { return GetValue(() => ToolBarVisibility); }
            set { SetValue(() => ToolBarVisibility, value); }
        }
        public bool PriceLabelVisibility
        {
            get { return GetValue(() => PriceLabelVisibility); }
            set { SetValue(() => PriceLabelVisibility, value); }
        }
        public bool FinishButtonVisibility
        {
            get { return GetValue(() => FinishButtonVisibility); }
            set { SetValue(() => FinishButtonVisibility, value); }
        }
        public bool TotalLabelVisibility
        {
            get { return GetValue(() => TotalLabelVisibility); }
            set { SetValue(() => TotalLabelVisibility, value); }
        }
        public bool FinishButtonEnabled
        {
            get { return GetValue(() => FinishButtonEnabled); }
            set { SetValue(() => FinishButtonEnabled, value); }
        }
        public bool CartVisibility
        {
            get { return GetValue(() => CartVisibility); }
            set { SetValue(() => CartVisibility, value); }
        }
        public bool FavVisibility
        {
            get { return GetValue(() => FavVisibility); }
            set { SetValue(() => FavVisibility, value); }
        }
        public string Price
        {
            get { return GetValue(() => Price); }
            set { SetValue(() => Price, value); }
        }
        public Uri FrameSource
        {
            get { return GetValue(() => FrameSource); }
            set { SetValue(() => FrameSource, value); }
        }
        public ObservableCollection<ProductState> Products
        {
            get { return GetValue(() => Products); }
            set { SetValue(() => Products, value); }
        }
        public ObservableCollection<ProductState> FavoriteProducts
        {
            get { return GetValue(() => FavoriteProducts); }
            set { SetValue(() => FavoriteProducts, value); }
        }
        public ObservableCollection<ProductCheckoutViewModel> ProductCheckouts
        {
            get { return GetValue(() => ProductCheckouts); }
            set { SetValue(() => ProductCheckouts, value); }
        }
        public ObservableCollection<string> SortOptions
        {
            get { return GetValue(() => SortOptions); }
            set { SetValue(() => SortOptions, value); }
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
                        SelectedWeightOption = -1;
                        SelectedInsertOption = -1;
                        SelectedMetalOption = -1;
                        break;
                    default:
                        ShowFilterPrice(SelectedPriceOption);
                        SelectedWeightOption = -1;
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
                        SelectedPriceOption = -1;
                        SelectedMetalOption = -1;
                        SelectedInsertOption = -1;
                        break;
                    default:
                        ShowFilterWeight(SelectedWeightOption);
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
                        SelectedPriceOption = -1;
                        SelectedWeightOption = -1;
                        SelectedInsertOption = -1;
                        break;
                    default:
                        ShowFilterMetal(SelectedMetalOption);
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
                        SelectedPriceOption = -1;
                        SelectedWeightOption = -1;
                        SelectedMetalOption = -1;
                        break;
                    default:
                        ShowFilterInsert(SelectedInsertOption);
                        SelectedPriceOption = -1;
                        SelectedWeightOption = -1;
                        SelectedMetalOption = -1;
                        break;
                }
            }
        }
        public bool LangCheck
        {
            get { return GetValue(() => LangCheck); }
            set { SetValue(() => LangCheck, value); }
        }
        public MainWindowViewModel() 
        { 
            Cursor = CursorCollection.GetCursor();
            ProductCheckouts = new ObservableCollection<ProductCheckoutViewModel>();
            CloseCommand = new RelayCommand(Close);
            SwitchLangCommand = new RelayCommand(SwitchLang);
            SwitchThemeCommand = new RelayCommandParam((tag) => ChangeTheme((string)tag));
            SelectItemToCartCommand = new RelayCommandParam((product) => SelectItemToCart((string)product));
            SelectItemToFavoriteCommand = new RelayCommandParam((product) => SelectItemToFavorite((string)product));
            ViewDetailsCommand = new RelayCommandParam((product) => ViewDetails((string)product));
            ShowNecklacesCommand = new RelayCommand(ShowNecklaces);
            ShowRingsCommand = new RelayCommand(ShowRings);
            ShowEarringsCommand = new RelayCommand(ShowEarrings);
            ShowWristwearCommand = new RelayCommand(ShowWristwear);
            ShowAllCommand = new RelayCommand(ShowAll);
            ShowAccountSettingsCommand = new RelayCommand(UserSettings);
            LogoutCommand = new RelayCommand(LogOut);
            NextPageCommand = new RelayCommand(CartPage);
            FavCommand = new RelayCommand(FavPage);
            GoBackCommand = new RelayCommand(GoBack);
            FinishCommand = new RelayCommand(Finish);
            RemoveCheckoutCommand = new RelayCommandParam(control => RemoveCheckout((ProductCheckoutViewModel)control));

            DatabaseUnit db = new DatabaseUnit();
            User users = db.Users.Get(userId);
            UserGivenName = users.GivenName;
            NavbarVisibility = true;
            SearchFieldVisibility = true;
            ScrollbarVisibility = true;
            ToolBarVisibility = true;
            SetLocale();
            ResetSort();
            ChangeTheme(users.Theme);
            ShowAll();
            Settings.changeLang += ShowAll;
            Settings.changeLang += SetLocale;
            Settings.changeLang += LoadFavoritesList;
            ChangeCheckout();
            LoadFavoritesList();
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
        #region Функции загрузки
        private void ShowAll()
        {
            DatabaseUnit db = new DatabaseUnit();
            Products = new ObservableCollection<ProductState>();
            foreach (var p in db.Products.GetAll())
            {
                if (p.IsActive)
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
                    state.Price = "$" + p.Price.ToString();
                    state.IsInCart = CheckIsInCart(p.ProductCode);
                    state.IsInFavorites = CheckIsInFav(p.ProductCode);
                    Products.Add(state);
                }
            }
        }
        private void LoadFavoritesList()
        {
            DatabaseUnit db = new DatabaseUnit();
            FavoriteProducts = new ObservableCollection<ProductState>();
            foreach (var p in db.Favorites.Get(userId).Products)
            {
                if (p.IsActive)
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
                    state.Price = "$" + p.Price.ToString();
                    state.IsInCart = CheckIsInCart(p.ProductCode);
                    state.IsInFavorites = CheckIsInFav(p.ProductCode);
                    FavoriteProducts.Add(state);
                }
            }
        }
        private void SetLocale()
        {
            SortOptions = new ObservableCollection<string>()
            {
                Application.Current.FindResource("NoSort").ToString(),
                Application.Current.FindResource("Ascending").ToString(),
                Application.Current.FindResource("Descending").ToString()
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
        #endregion

        #region Дополнительные методы
        private bool CheckIsInCart(int code)
        {
            DatabaseUnit db = new DatabaseUnit();
            return db.Baskets.Get(userId).Products.Where(p => p.ProductCode == code).Count() != 0;
        }
        private bool CheckIsInFav(int code)
        {
            DatabaseUnit db = new DatabaseUnit();
            return db.Favorites.Get(userId).Products.Where(p => p.ProductCode == code).Count() != 0;
        }
        private decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var bill in bill1.OrderInfos)
            {
                total += bill.Price;
            }
            return total;
        }
        private void CheckQuantity(ProductCheckoutViewModel model)
        {
            Price = ": $" + CalculateTotal();
            if (model.item.Quantity == 0)
                RemoveCheckout(model);
        }
        private void CheckProductsQuantity(Bill bill)
        {
            DatabaseUnit db = new DatabaseUnit();
            foreach (var o in bill.OrderInfos)
            {
                Product p = db.Products.Get(o.ProductCode);
                p.Quantity -= o.Quantity;
                if (p.Quantity == 0)
                    p.IsActive = false;
                db.Products.Update(p);
            }
        }
        #endregion

        #region Фильтрация
        private void Search(string key)
        {
            ShowFilter(key);
            if (key.Length == 0)
                ShowAll();
        }
        private void ShowFilter(string key)
        {
            DatabaseUnit db = new DatabaseUnit();
            Products.Clear();
            foreach (var p in db.Products.GetAll())
            {
                var keyRus = db.Dictionary.Get(p.Pname).WordRus;
                var keyEn = db.Dictionary.Get(p.Pname).WordEn;
                if (p.IsActive && (keyRus.ToUpper().Contains(key.ToUpper()) || keyEn.ToUpper().Contains(key.ToUpper())))
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
                    state.Price = "$" + p.Price.ToString();
                    state.IsInCart = CheckIsInCart(p.ProductCode);
                    state.IsInFavorites = CheckIsInFav(p.ProductCode);
                    Products.Add(state);
                }
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
                if (p.IsActive && p.ProductType == id)
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
                    state.Price = "$" + p.Price.ToString();
                    state.IsInCart = CheckIsInCart(p.ProductCode);
                    state.IsInFavorites = CheckIsInFav(p.ProductCode);
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
                if (p.IsActive && p.StoneInsert == id)
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
                    state.Price = "$" + p.Price.ToString();
                    state.IsInCart = CheckIsInCart(p.ProductCode);
                    state.IsInFavorites = CheckIsInFav(p.ProductCode);
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
                if (p.IsActive)
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
                    state.Price = "$" + p.Price.ToString();
                    state.IsInCart = CheckIsInCart(p.ProductCode);
                    state.IsInFavorites = CheckIsInFav(p.ProductCode);
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
                if (p.IsActive && p.Metal == id)
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
                    state.Price = "$" + p.Price.ToString();
                    state.IsInCart = CheckIsInCart(p.ProductCode);
                    state.IsInFavorites = CheckIsInFav(p.ProductCode);
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
                if (p.IsActive)
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
                    state.Price = "$" + p.Price.ToString();
                    state.IsInCart = CheckIsInCart(p.ProductCode);
                    state.IsInFavorites = CheckIsInFav(p.ProductCode);
                    Products.Add(state);
                }
            }
            if (Products.Count() == 0)
                NothingFoundVisibility = true;
            else
                NothingFoundVisibility = false;
        }
        private void ShowNecklaces()
        {
            ShowFilterType(1);
        }

        private void ShowRings()
        {
            ShowFilterType(2);
        }

        private void ShowEarrings()
        {
            ShowFilterType(3);
        }
        private void ShowWristwear()
        {
            ShowFilterType(4);
        }
        private void ResetSort()
        {
            SelectedPriceOption = -1;
            SelectedWeightOption = -1;
            SelectedMetalOption = -1;
            SelectedInsertOption = -1;
        }
        #endregion

        #region Методы для работы с товаром
        private void RemoveCheckout(ProductCheckoutViewModel product)
        {
            DatabaseUnit db = new DatabaseUnit();
            Basket basket = db.Baskets.Get(userId);
            ProductCheckouts.Remove(product);
            basket.Products = basket.Products.Except(basket.Products.Where(p => p.ProductCode == product.item.ProductCode)).ToList();

            Products.Where(p => p.Id == product.item.ProductCode).Single().IsInCart = false;
            if (FavoriteProducts.Where(p => p.Id == product.item.ProductCode).Count() != 0)
            {
                FavoriteProducts.Where(p => p.Id == product.item.ProductCode).Single().IsInCart = false;
            }
            db.Baskets.Update(basket);
            if (ProductCheckouts.Count == 0)
                FinishButtonEnabled = false;
        }
        private void SelectItemToCart(string name)
        {
            DatabaseUnit db = new DatabaseUnit();
            int id = int.Parse(name.Remove(0, 3));
            Basket basket = db.Baskets.Get(userId);
            var product = db.Products.Get(id);
            
            if (basket.Products.Where(p => p.ProductCode == product.ProductCode).Count() != 0)
            {
                basket.Products = basket.Products.Except(basket.Products.Where(p => p.ProductCode == product.ProductCode)).ToList();
                Products.Where(p => p.Id == product.ProductCode).Single().IsInCart = false;
                if (FavoriteProducts.Where(p => p.Id == product.ProductCode).Count() != 0)
                {
                    FavoriteProducts.Where(p => p.Id == product.ProductCode).Single().IsInCart = false;
                }
                ProductCheckouts.Remove(ProductCheckouts.Where(p => p.item.ProductCode == product.ProductCode).Single());
                bill1.OrderInfos.Remove(bill1.OrderInfos.Where(o => o.ProductCode == product.ProductCode).Single());
                db.Baskets.Update(basket);
            }
            else
            {
                basket.Products.Add(product);
                Products.Where(p => p.Id == product.ProductCode).Single().IsInCart = true;
                if(FavoriteProducts.Where(p => p.Id == product.ProductCode).Count() != 0)
                {
                    FavoriteProducts.Where(p => p.Id == product.ProductCode).Single().IsInCart = true;
                }
                db.Baskets.Update(basket);
                ChangeCheckout();
            }
        }
        private void ChangeCheckout()
        {
            DatabaseUnit db = new DatabaseUnit();
            Basket basket = db.Baskets.Get(userId);
            var productsToRemove = basket.Products.Where(p => !p.IsActive);
            foreach (var p in productsToRemove.Reverse())
            {
                    basket.Products.Remove(p);
                    db.Baskets.Update(basket);
            }
            foreach (var p in basket.Products)
            {
                if (bill1.OrderInfos.Where(o => o.ProductCode == p.ProductCode).Count() == 0)
                {
                    OrderInfo bill = new OrderInfo()
                    {
                        BillId = bill1.BillId,
                        ProductCode = p.ProductCode,
                        Quantity = 1,
                        Price = p.Price
                    };
                    bill1.OrderInfos.Add(bill);
                    var prodCkeck = new ProductCheckoutViewModel(bill1, bill);
                    prodCkeck.ItemQuantityChanged += () => CheckQuantity(prodCkeck);
                    ProductCheckouts.Add(prodCkeck);
                }
            }
        }
        private void SelectItemToFavorite(string name)
        {
            DatabaseUnit db = new DatabaseUnit();
            int id = int.Parse(name.Remove(0, 3));

            var product = db.Products.Get(id);
            var favorite = db.Favorites.Get(userId);

            if (favorite.Products.Where(p => p.ProductCode == product.ProductCode).Count() != 0)
            {
                favorite.Products = favorite.Products.Except(favorite.Products.Where(p => p.ProductCode == product.ProductCode)).ToList();
                Products.Where(p => p.Id == product.ProductCode).Single().IsInFavorites = false;
                FavoriteProducts.Remove(FavoriteProducts.Where(p => p.Id == product.ProductCode).Single());
                db.Favorites.Update(favorite);
            }
            else
            {
                favorite.Products.Add(product);
                ProductState state = new ProductState();
                switch (Settings.Lang)
                {
                    case Settings.Languages.RU:
                        state.Name = db.Dictionary.Get(product.Pname).WordRus;
                        break;
                    case Settings.Languages.EN:
                        state.Name = db.Dictionary.Get(product.Pname).WordEn;
                        break;
                }
                state.Id = product.ProductCode;
                state.Image = Settings.projectPath + "/images/" + product.Pimage;
                state.Price = "$" + product.Price.ToString();
                state.IsInCart = CheckIsInCart(product.ProductCode);
                state.IsInFavorites = true;
                FavoriteProducts.Add(state);
                Products.Where(p => p.Id == product.ProductCode).Single().IsInFavorites = true;
                db.Favorites.Update(favorite);
            }
        }
        private void Finish()
        {
            try
            {
                decimal total = CalculateTotal();
                string subject = "";
                bill1.TotalPrice = total;
                switch (Settings.Lang)
                {
                    case Settings.Languages.RU:
                        WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel($"Итоговая стоимость: ${bill1.TotalPrice}\nОформить заказ?", true, true));
                        subject = "Информация о заказе";
                        break;
                    case Settings.Languages.EN:
                        WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel($"Total price: ${bill1.TotalPrice}\nPlace an order?", true, true));
                        subject = "Order information";
                        break;
                }
                if (MessageViewModel.DialogResult)
                {
                    DatabaseUnit db = new DatabaseUnit();
                    User user = db.Users.Get(userId);

                    List<Bill> bills = db.Bills.GetAll();
                    int lastId;
                    if (bills.Count != 0 )
                        lastId = db.Bills.GetAll().Last().BillId;
                    else 
                        lastId = 0;
                    MessageConstructor messageConstructor = new MessageConstructor(subject, user.Email);
                    messageConstructor.GenerateOrderMessage(user.GivenName, bill1, lastId + 1);
                    messageConstructor.SendMessage();

                    Basket basket = db.Baskets.Get(userId);
                    FinishButtonEnabled = false;
                    bill1.DateOfOrder = DateTime.Now;
                    bill1.UserId = userId;
                    db.Bills.Add(bill1);
                    CheckProductsQuantity(bill1);
                    bill1 = new Bill();

                    basket.Products.Clear();
                    ProductCheckouts.Clear();
                    db.Baskets.Update(basket);
                    TotalLabelVisibility = false;
                    PriceLabelVisibility = false;
                    WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel("Success"));
                    ShowAll();
                    LoadFavoritesList();
                    ChangeTheme(user.Theme);
                }
            }
            catch(Exception ex)
            {
                WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel(ex.Message));
            }
            finally
            {
                Cursor = CursorCollection.GetCursor();
            }
        }
        #endregion

        #region Навигация
        private void ViewDetails(string name)
        {
            int id = int.Parse(name.Remove(0, 3));
            ScrollbarVisibility = false;
            ToolBarVisibility = false;
            NavbarVisibility = false;
            SearchFieldVisibility = false;
            BackButtonVisibility = true;
            FrameVisibility = true;
            ProductDetailsViewModel.ProductId = id;
            FrameSource = WindowService.GetPage(WindowService.Pages.ProductDetailsView);
        }
        private void CartPage()
        {
            DatabaseUnit db = new DatabaseUnit();
            Basket basket = db.Baskets.Get(userId);
            try
            {
                if (basket.Products.Count == 0 && bill1.OrderInfos.Count == 0)
                    throw new ArgumentException("ErrorEmptyCart");
                ChangeCheckout();
                NavbarVisibility = false;
                ScrollbarVisibility = false;
                ToolBarVisibility = false;

                CheckoutVisibility = true;
                FrameVisibility = false;
                CartVisibility = true;
                FavVisibility = false;
                BackButtonVisibility = true;
                SearchFieldVisibility = false;
                PriceLabelVisibility = true;
                FinishButtonVisibility = true;
                FinishButtonEnabled = true;
                TotalLabelVisibility = true;
                Price = ": $" + CalculateTotal();
            }
            catch (ArgumentException ex)
            {
                WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel(ex.Message));
            }
        }
        private void FavPage()
        {
            DatabaseUnit db = new DatabaseUnit();
            Favorite fav = db.Favorites.Get(userId);
            try
            {
                if (fav.Products.Count == 0)
                    throw new ArgumentException("ErrorEmptyFav");
                NavbarVisibility = false;
                ScrollbarVisibility = false;
                ToolBarVisibility = false;
                FrameVisibility = false;
                CheckoutVisibility = true;
                CartVisibility = false;
                FavVisibility = true;
                BackButtonVisibility = true;
                SearchFieldVisibility = false;
                PriceLabelVisibility = false;
                FinishButtonVisibility = false;
                FinishButtonEnabled = false;
                TotalLabelVisibility = false;
            }
            catch (ArgumentException ex)
            {
                WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel(ex.Message));
            }
        }
        private void GoBack()
        {
            CheckoutVisibility = false;
            CartVisibility = false;
            FavVisibility = false;
            SearchFieldVisibility = true;
            ScrollbarVisibility = true;
            BackButtonVisibility = false;
            NavbarVisibility = true;
            ToolBarVisibility = true;
            FinishButtonVisibility = false;
            FinishButtonVisibility = false;
            FinishButtonEnabled = true;
            TotalLabelVisibility = false;
            PriceLabelVisibility = false;
            FrameVisibility = false;
            Price = "";
        }
        private void UserSettings()
        {
            ScrollbarVisibility = false;
            FavVisibility = false;
            CartVisibility = false;
            ToolBarVisibility = false;
            NavbarVisibility = false;
            SearchFieldVisibility = false;
            FinishButtonVisibility = false;
            PriceLabelVisibility = false;
            TotalLabelVisibility = false;
            CheckoutVisibility = false;
            BackButtonVisibility = true;
            FrameVisibility = true;
            UserDetailsViewModel.id = userId;
            UserDetailsViewModel.nameChanged += () => 
            {
                DatabaseUnit db = new DatabaseUnit();
                UserGivenName = db.Users.Get(userId).GivenName;
            };
            FrameSource = WindowService.GetPage(WindowService.Pages.UserDetailsView);
        }
        #endregion

        #region Смена языка
        private void ToEnglish()
        {
            Application.Current.Resources.MergedDictionaries.Remove(Settings.ResourceRusLang);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceEnLang);
            Settings.Lang = Settings.Languages.EN;

        }
        private void ToRussian()
        {
            Application.Current.Resources.MergedDictionaries.Remove(Settings.ResourceEnLang);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceRusLang);
            Settings.Lang = Settings.Languages.RU;

        }
        private void SwitchLang()
        {
            if (LangCheck)
                ToEnglish();
            else
                ToRussian();
        }
        #endregion

        #region Смена темы
        private void ChangeTheme(string tag)
        {
            switch (tag)
            {
                case "Theme1":
                    SwitchTheme1();
                    break;
                case "Theme2":
                    SwitchTheme2();
                    break;
                case "Theme3":
                    SwitchTheme3();
                    break;
            }
        }
        private void SwitchTheme1()
        {
            DatabaseUnit db = new DatabaseUnit();
            Application.Current.Resources.MergedDictionaries.Clear();

            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceDefaults);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceLights);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceStyles);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourcePrimaryTeal);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceAccentTeal);

            switch(Settings.Lang)
            {
                case Settings.Languages.EN:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceEnLang);
                    break;
                case Settings.Languages.RU:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceRusLang);
                    break;
            }

            MainImageSource = Settings.projectPath + "/images/mainImage.jpg";
            User us = db.Users.Get(userId);
            us.Theme = "Theme1";
            db.Users.Update(us);
        }
        private void SwitchTheme2()
        {
            DatabaseUnit db = new DatabaseUnit();
            Application.Current.Resources.MergedDictionaries.Clear();

            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceDefaults);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceLights);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceStyles);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourcePrimaryPink);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceAccentGreen);

            switch (Settings.Lang)
            {
                case Settings.Languages.EN:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceEnLang);
                    break;
                case Settings.Languages.RU:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceRusLang);
                    break;
            }

            MainImageSource = Settings.projectPath + "/images/mainPhoto3.jpg";

            User us = db.Users.Get(userId);
            us.Theme = "Theme2";
            db.Users.Update(us);
        }
        private void SwitchTheme3()
        {
            DatabaseUnit db = new DatabaseUnit();
            Application.Current.Resources.MergedDictionaries.Clear();

            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceDefaults);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceLights);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceStyles);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourcePrimaryBrown);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceAccentCyan);

            switch (Settings.Lang)
            {
                case Settings.Languages.EN:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceEnLang);
                    break;
                case Settings.Languages.RU:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceRusLang);
                    break;
            }

            MainImageSource = Settings.projectPath + "/images/mainPhoto.jpg";

            User us = db.Users.Get(userId);
            us.Theme = "Theme3";
            db.Users.Update(us);
        }
        #endregion

        #region Закрытие окна
        private void Close()
        {
            WindowService.CloseWindow(WindowService.Windows.MainWindow);
        }
        
        private void LogOut()
        {
            WindowService.OpenWindow(WindowService.Windows.Login, new LoginViewModel());
            WindowService.CloseWindow(WindowService.Windows.MainWindow);
        }
        #endregion

    }
}
