using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class ProductDetailsViewModel : PropertyChangedNotification
    {
        private static int _productId;
        private static event Action SetProductState;
        public static int ProductId
        {
            get { return _productId; }
            set
            {
                _productId = value;
                SetProductState?.Invoke();
            }
        }

        public string ImageSource
        {
            get { return GetValue(() => ImageSource); }
            set { SetValue(() => ImageSource, value); }
        }
        public string Description
        {
            get { return GetValue(() => Description); }
            set { SetValue(() => Description, value); }
        }
        public string Name
        {
            get { return GetValue(() => Name); }
            set { SetValue(() => Name, value); }
        }
        public string Price
        {
            get { return GetValue(() => Price); }
            set { SetValue(() => Price, value); }
        }
        public string Metal
        {
            get { return GetValue(() => Metal); }
            set { SetValue(() => Metal, value); }
        }
        public string MetalSample
        {
            get { return GetValue(() => MetalSample); }
            set { SetValue(() => MetalSample, value); }
        }
        public string Weight
        {
            get { return GetValue(() => Weight); }
            set { SetValue(() => Weight, value); }
        }
        public string StoneInsert
        {
            get { return GetValue(() => StoneInsert); }
            set { SetValue(() => StoneInsert, value); }
        }
        public string Quantity
        {
            get { return GetValue(() => Quantity); }
            set { SetValue(() => Quantity, value); }
        }

        public ProductDetailsViewModel() 
        {
            SetProduct();
            SetProductState += SetProduct;
            Settings.changeLang += SetProduct;
        }
        private void SetProduct()
        {
            DatabaseUnit db = new DatabaseUnit();
            Product product = db.Products.Get(ProductId);
            ImageSource = Settings.projectPath + "/images/" + product.Pimage;
            Price = product.Price.ToString();
            Weight = product.Pweight.ToString();
            MetalSample = product.MetalSample.ToString();
            Quantity = product.Quantity.ToString();
            switch(Settings.Lang)
            {
                case Settings.Languages.RU:
                    Description = product.PdescriptionRus ?? "";
                    Name = db.Dictionary.Get(product.Pname).WordRus;
                    Metal = db.Metals.Get(product.Metal).MetalNameRus;
                    StoneInsert = db.Stones.Get(product.StoneInsert).StoneNameRus;
                    break;
                case Settings.Languages.EN:
                    Description = product.PdescriptionEn ?? "";
                    Name = db.Dictionary.Get(product.Pname).WordEn;
                    Metal = db.Metals.Get(product.Metal).MetalNameEn;
                    StoneInsert = db.Stones.Get(product.StoneInsert).StoneNameEn;
                    break;
            }
            
        }
    }
}
