using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class ProductState : PropertyChangedNotification
    {
        public ProductState(int id, string image, string name, string price)
        {
            Id = id;
            Image = image;
            Name = name;
            Price = price;
        }
        public ProductState(int id, string image, string name, string price, bool active)
        {
            Id = id;
            Image = image;
            Name = name;
            Price = price;
            IsActive = active;
        }
        public ProductState() { }
        public int Id
        {
            get { return GetValue(() => Id); }
            set { SetValue(() => Id, value); }
        }
        public string Image
        {
            get { return GetValue(() => Image); }
            set { SetValue(() => Image, value); }
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
        public bool IsVisible
        {
            get { return GetValue(() => IsVisible); }
            set { SetValue(() => IsVisible, value); }
        }
        public bool IsActive
        {
            get { return GetValue(() => IsActive); }
            set { SetValue(() => IsActive, value); }
        }
        public bool IsInCart
        {
            get { return GetValue(() => IsInCart); }
            set { SetValue(() => IsInCart, value); }
        }
        public bool IsInFavorites
        {
            get { return GetValue(() => IsInFavorites); }
            set { SetValue(() => IsInFavorites, value); }
        }
        public void CheckActive(bool ShowActiveOnly)
        {
            IsVisible = ShowActiveOnly && IsActive || !ShowActiveOnly && IsActive || !ShowActiveOnly && !IsActive;
        }
    }
}
