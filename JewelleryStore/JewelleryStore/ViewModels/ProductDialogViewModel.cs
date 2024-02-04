using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Automation;

namespace JewelleryStore
{
    public class ProductDialogViewModel : PropertyChangedNotification
    {
        private DatabaseUnit db = new DatabaseUnit();
        private string img;
        private int _id;
        private bool _nameAdded;
        public RelayCommand AddCommand { get; set; }
        public RelayCommand AddImageCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand EditCommand { get; set; }

        public Cursor Cursor { get; set; }
        public Brush RectFillBrush
        {
            get { return GetValue(() => RectFillBrush); }
            set { SetValue(() => RectFillBrush, value); }
        }
        public SolidColorBrush RectStrokeBrush
        {
            get { return GetValue(() => RectStrokeBrush); }
            set { SetValue(() => RectStrokeBrush, value); }
        }
        public bool AddButtonVisibility
        {
            get { return GetValue(() => AddButtonVisibility); }
            set { SetValue(() => AddButtonVisibility, value); }
        }
        public bool EditButtonVisibility
        {
            get { return GetValue(() => EditButtonVisibility); }
            set { SetValue(() => EditButtonVisibility, value); }
        }

        #region Errors
        public bool ErrorPriceVisibility
        {
            get { return GetValue(() => ErrorPriceVisibility); }
            set { SetValue(() => ErrorPriceVisibility, value); }
        }
        public bool ErrorQuantityVisibility
        {
            get { return GetValue(() => ErrorQuantityVisibility); }
            set { SetValue(() => ErrorQuantityVisibility, value); }
        }
        public bool ErrorWeightVisibility
        {
            get { return GetValue(() => ErrorWeightVisibility); }
            set { SetValue(() => ErrorWeightVisibility, value); }
        }
        public bool ErrorMetalSampleVisibility
        {
            get { return GetValue(() => ErrorMetalSampleVisibility); }
            set { SetValue(() => ErrorMetalSampleVisibility, value); }
        }
        public bool ErrorProductNameEnVisibility
        {
            get { return GetValue(() => ErrorProductNameEnVisibility); }
            set { SetValue(() => ErrorProductNameEnVisibility, value); }
        }
        public bool ErrorProductNameRusVisibility
        {
            get { return GetValue(() => ErrorProductNameRusVisibility); }
            set { SetValue(() => ErrorProductNameRusVisibility, value); }
        }
        public bool ErrorMetal
        {
            get { return GetValue(() => ErrorMetal); }
            set { SetValue(() => ErrorMetal, value); }
        }
        public bool ErrorStone
        {
            get { return GetValue(() => ErrorStone); }
            set { SetValue(() => ErrorStone, value); }
        }
        public bool ErrorProductType
        {
            get { return GetValue(() => ErrorProductType); }
            set { SetValue(() => ErrorProductType, value); }
        }
        #endregion

        public Product Product { get; set; }
        public string Price
        {
            get { return GetValue(() => Price); }
            set 
            {
                SetValue(() => Price, value);
                try
                {
                    decimal price = decimal.Parse(Price);
                    if (price < 1 || price > 10000)
                        throw new ArgumentException();
                    Product.Price = price;
                    ErrorPriceVisibility = false;
                }
                catch (FormatException)
                {
                    ErrorPriceVisibility = true;
                }
                catch (ArgumentNullException)
                {
                    ErrorPriceVisibility = true;
                }
                catch(ArgumentException)
                {
                    ErrorPriceVisibility = true;
                }
            }
        }
        public string Quantity
        {
            get { return GetValue(() => Quantity); }
            set
            {
                SetValue(() => Quantity, value);
                try
                {
                    int quantity = int.Parse(Quantity);
                    if (quantity < 0 || quantity > 100000)
                        throw new ArgumentException();
                    Product.Quantity = quantity;
                    CheckboxAciveEnabled = Product.Quantity != 0;
                    ErrorQuantityVisibility = false;
                }
                catch (FormatException)
                {
                    ErrorQuantityVisibility = true;
                }
                catch (ArgumentNullException)
                {
                    ErrorQuantityVisibility = true;
                }
                catch (ArgumentException)
                {
                    ErrorQuantityVisibility = true;
                }
            }
        }
        public string MetalSample
        {
            get { return GetValue(() => MetalSample); }
            set 
            { 
                SetValue(() => MetalSample, value);
                try
                {
                    int metalSample = int.Parse(MetalSample);
                    if (metalSample < 325 || metalSample > 999)
                        throw new ArgumentException();
                    Product.MetalSample = metalSample;
                    ErrorMetalSampleVisibility = false;
                }
                catch (FormatException)
                {
                    ErrorMetalSampleVisibility = true;
                }
                catch (ArgumentNullException)
                {
                    ErrorMetalSampleVisibility = true;
                }
                catch (ArgumentException)
                {
                    ErrorMetalSampleVisibility = true;
                }
            }
        }
        public string Weight
        {
            get { return GetValue(() => Weight); }
            set 
            { 
                SetValue(() => Weight, value); 
                try
                {
                    decimal weight = decimal.Parse(Weight);
                    if (weight < 0.01m || weight > 100)
                        throw new ArgumentException();
                    Product.Pweight = weight;
                    ErrorWeightVisibility = false;
                }
                catch (FormatException)
                {
                    ErrorWeightVisibility = true;
                }
                catch (ArgumentNullException)
                {
                    ErrorWeightVisibility = true;
                }
                catch (ArgumentException)
                {
                    ErrorWeightVisibility = true;
                }
            }
        }
        public string WordEn
        {
            get { return GetValue(() => WordEn); }
            set 
            { 
                SetValue(() => WordEn, value);
                ErrorProductNameEnVisibility = false;
            }
        }
        public string WordRus
        {
            get { return GetValue(() => WordRus); }
            set 
            { 
                SetValue(() => WordRus, value);
                ErrorProductNameRusVisibility = false;
            }
        }

        public string DescriptionRus
        {
            get { return GetValue(() => DescriptionRus); }
            set { SetValue(() => DescriptionRus, value); }
        }
        public string DescriptionEn
        {
            get { return GetValue(() => DescriptionEn); }
            set { SetValue(() => DescriptionEn, value); }
        }

        public bool ProductActive
        {
            get { return GetValue(() => ProductActive); }
            set { SetValue(() => ProductActive, value); }
        }
        public Dictionary Dictionary
        {
            get { return GetValue(() => Dictionary); }
            set 
            { 
                SetValue(() => Dictionary, value); 
                ErrorProductNameRusVisibility = false;
                ErrorProductNameEnVisibility = false;
            }
        }
        public List<string> Metals
        {
            get { return GetValue(() => Metals); }
            set { SetValue(() => Metals, value); }
        }
        public string SelectedMetal
        {
            get { return GetValue(() => SelectedMetal); }
            set 
            { 
                SetValue(() => SelectedMetal, value);
                ErrorMetal = false;
            }
        }
        public List<string> Stones
        {
            get { return GetValue(() => Stones); }
            set { SetValue(() => Stones, value); }
        }
        public string SelectedStone
        {
            get { return GetValue(() => SelectedStone); }
            set 
            { 
                SetValue(() => SelectedStone, value); 
                ErrorStone = false;
            }
        }
        public int ProductType
        {
            get { return GetValue(() => ProductType); }
            set 
            { 
                SetValue(() => ProductType, value); 
                ErrorProductType = false;
            }
        }
        public bool CheckboxAciveEnabled
        {
            get { return GetValue(() => CheckboxAciveEnabled); }
            set { SetValue(() => CheckboxAciveEnabled, value); }
        }
        public ProductDialogViewModel()
        {
            Cursor = CursorCollection.GetCursor();
            CheckboxAciveEnabled = true;
            Product = new Product();
            Dictionary = new Dictionary();
            switch (Settings.Lang)
            {
                case Settings.Languages.EN:
                    Metals = db.Metals.GetAll().Select(m => m.MetalNameEn).ToList();
                    Stones = db.Stones.GetAll().Select(s => s.StoneNameEn).ToList();
                    break;
                case Settings.Languages.RU:
                    Metals = db.Metals.GetAll().Select(m => m.MetalNameRus).ToList();
                    Stones = db.Stones.GetAll().Select(s => s.StoneNameRus).ToList();
                    break;
            }
            SelectedMetal = "";
            SelectedStone = "";
            ProductType = -1;
            FrameworkElement frameworkElement = new FrameworkElement();
            RectStrokeBrush = (SolidColorBrush)frameworkElement.FindResource("PrimaryHueLightBrush");
            EditButtonVisibility = false;
            AddButtonVisibility = true;
            ProductActive = true;
            EditCommand = new RelayCommand(EditProduct);
            AddCommand = new RelayCommand(AddProduct);
            AddImageCommand = new RelayCommand(AddImage);
            CloseCommand = new RelayCommand(Close);
        }
        public ProductDialogViewModel(int pId)
        {
            Cursor = CursorCollection.GetCursor();
            _id = pId;
            LoadProduct();
            switch (Settings.Lang)
            {
                case Settings.Languages.EN:
                    Metals = db.Metals.GetAll().Select(m => m.MetalNameEn).ToList();
                    SelectedMetal = db.Metals.Get(Product.Metal).MetalNameEn;
                    Stones = db.Stones.GetAll().Select(s => s.StoneNameEn).ToList();
                    SelectedStone = db.Stones.Get(Product.StoneInsert).StoneNameEn;
                    break;
                case Settings.Languages.RU:
                    Metals = db.Metals.GetAll().Select(m => m.MetalNameRus).ToList();
                    SelectedMetal = db.Metals.Get(Product.Metal).MetalNameRus;
                    Stones = db.Stones.GetAll().Select(s => s.StoneNameRus).ToList();
                    SelectedStone = db.Stones.Get(Product.StoneInsert).StoneNameRus;
                    break;
            }
            ProductType = db.ProductTypes.Get(Product.ProductType).ProductTypeId - 1;
            img = Product.Pimage;
            var projectPath = Settings.projectPath;
            var source = new BitmapImage(new Uri(projectPath + "/images/" + Product.Pimage));
            ImageBrush imgBrush = new ImageBrush(source);
            RectFillBrush = imgBrush;
            RectStrokeBrush = Brushes.Transparent;
            EditButtonVisibility = true;
            AddButtonVisibility = false;

            EditCommand = new RelayCommand(EditProduct);
            AddCommand = new RelayCommand(AddProduct);
            AddImageCommand = new RelayCommand(AddImage);
            CloseCommand = new RelayCommand(Close);
        }
        private void LoadProduct()
        {
            Product = db.Products.Get(_id);
            Quantity = Product.Quantity.ToString();
            Price = Product.Price.ToString();
            Weight = Product.Pweight.ToString();
            MetalSample = Product.MetalSample.ToString();
            Dictionary = db.Dictionary.Get(Product.Pname);
            WordEn = Dictionary.WordEn;
            WordRus = Dictionary.WordRus;
            DescriptionRus = Product.PdescriptionRus ?? "";
            DescriptionEn = Product.PdescriptionEn ?? "";
            ProductActive = Product.IsActive;
            if (Product.Quantity == 0)
                CheckboxAciveEnabled = false;
        }
        private void AddProduct()
        {
            try
            {
                ValidateData();

                Dictionary.WordEn = WordEn;
                Dictionary.WordRus = WordRus;

                if (!_nameAdded)
                {
                    db.Dictionary.Add(Dictionary);
                    _nameAdded = true;
                }
                else
                    db.Dictionary.Update(Dictionary);
                Product.Pname = Dictionary.WordId;


                Product.Pimage = img;
                Product.PdescriptionEn = DescriptionEn;
                Product.PdescriptionRus = DescriptionRus;
                Product.ProductType = ProductType + 1;
                #region IsActive
                if (Product.Quantity == 0)
                {
                    ProductActive = false;
                    Product.IsActive = false;
                }
                else
                    Product.IsActive = ProductActive;
                #endregion
                    Product.StoneInsert = db.Stones.GetAll().Where(s => s.StoneNameEn == SelectedStone || s.StoneNameRus == SelectedStone).FirstOrDefault().StoneId;
                #region Metal
                if (SelectedMetal.Length != 0)
                    Product.Metal = db.Metals.GetAll().Where(s => s.MetalNameEn == SelectedMetal || s.MetalNameRus == SelectedMetal).FirstOrDefault().MetalId;
                else
                    throw new ArgumentException("ErrorMetal");
                #endregion
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var context = new ValidationContext(Product);
                if (!Validator.TryValidateObject(Product, context, results, true))
                {
                    throw new ArgumentException(results.First().ToString());
                }
                else
                {
                    db.Products.Add(Product);
                    WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel("Success"));
                    Close();
                }
            }
            catch (ArgumentException ex)
            {
                WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel(ex.Message));
            }
            catch (Exception ex)
            {

            }
        }
        private void ValidateData()
        {
            bool flag = false;
            if (WordEn == null || WordEn.Length == 0)
            {
                flag = true;
                ErrorProductNameEnVisibility = true;
            }
            else
                ErrorProductNameEnVisibility = false;
            if (WordRus == null || WordRus.Length == 0)
            {
                flag = true;
                ErrorProductNameRusVisibility = true;
            }
            else 
                ErrorProductNameRusVisibility = false;
            if (Price == null)
            {
                flag = true;
                ErrorPriceVisibility = true;
            }
            if (Quantity == null)
            {
                flag = true;
                ErrorQuantityVisibility = true;
            }
            if (Weight == null)
            {
                flag = true;
                ErrorWeightVisibility = true;
            }
            if (MetalSample == null)
            {
                flag = true;
                ErrorMetalSampleVisibility = true;
            }
            if (SelectedMetal == "")
            {
                flag = true;
                ErrorMetal = true;
            }
            if (SelectedStone == "")
            {
                flag = true;
                ErrorStone = true;
            }
            if (ProductType == -1)
            {
                flag = true;
                ErrorProductType = true;
            }
            if (flag)
            {
                throw new Exception();
            }
        }
        private void AddImage()
        {
            try
            {
                var filePicker = new OpenFileDialog();

                filePicker.DefaultExt = ".jpg";
                filePicker.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";

                bool? result = filePicker.ShowDialog();

                if (result == true)
                {
                    // Open document 
                    string filePath = filePicker.FileName;

                    string[] parts = filePath.Split('\\');

                    string fileName = parts[parts.Length - 1];
                    img = fileName;

                    var projectPath = Settings.projectPath;

                    File.Copy(filePath, projectPath + "/images/" + fileName, true);

                    var source = new BitmapImage(new Uri(projectPath + "/images/" + fileName));
                    ImageBrush imgBrush = new ImageBrush(source);
                    RectFillBrush = imgBrush;
                    RectStrokeBrush = Brushes.Transparent;
                }
            }
            catch (Exception ex)
            {
                WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel("ErrorSameImage"));
            }
        }
        private void EditProduct()
        {
            try
            {
                ValidateData();

                Dictionary.WordEn = WordEn;
                Dictionary.WordRus = WordRus;

                db.Dictionary.Update(Dictionary);

                Product.Pimage = img;
                Product.PdescriptionEn = DescriptionEn;
                Product.PdescriptionRus = DescriptionRus;
                Product.ProductType = ProductType + 1;
                #region IsActive
                if (Product.Quantity == 0)
                    ProductActive = false;
                else
                    Product.IsActive = ProductActive;
                #endregion
                #region Stone
                if (SelectedStone.Length != 0)
                    Product.StoneInsert = db.Stones.GetAll().Where(s => s.StoneNameEn == SelectedStone || s.StoneNameRus == SelectedStone).FirstOrDefault().StoneId;
                else
                    throw new ArgumentException("ErrorStoneInsert");
                #endregion
                #region Metal
                if (SelectedMetal.Length != 0)
                    Product.Metal = db.Metals.GetAll().Where(s => s.MetalNameEn == SelectedMetal || s.MetalNameRus == SelectedMetal).FirstOrDefault().MetalId;
                else
                    throw new ArgumentException("ErrorMetal");
                #endregion

                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var context = new ValidationContext(Product);
                if (!Validator.TryValidateObject(Product, context, results, true))
                {
                    throw new ArgumentException(results.First().ToString());
                }
                else
                {
                    db.Products.Update(Product);
                    WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel("Success"));
                    Close();
                }
            }
            catch (ArgumentException ex)
            {
                WindowService.OpenWindow(WindowService.Windows.Message, new MessageViewModel(ex.Message));
            }
            catch(Exception ex)
            {

            }
        }
        private void Close()
        {
            WindowService.CloseWindow(WindowService.Windows.ProductDialog);
        }
    }
}
