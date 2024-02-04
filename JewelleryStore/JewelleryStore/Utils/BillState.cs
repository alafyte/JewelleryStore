using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class BillState : PropertyChangedNotification
    {
        public Bill Bill 
        {
            get { return GetValue(() => Bill); }
            set { SetValue(() => Bill, value); }
        }

        public ICollection<OrderInfoState> OrderInfos
        {
            get { return GetValue(() => OrderInfos); }
            set { SetValue(() => OrderInfos, value); }
        }
    }

    public class OrderInfoState : PropertyChangedNotification
    {
        public int PNameid
        {
            get { return GetValue(() => PNameid); }
            set { SetValue(() => PNameid, value); }
        }
        public string Pname
        {
            get { return GetValue(() => Pname); }
            set { SetValue(() => Pname, value); }
        }
        public string Pimage
        {
            get { return GetValue(() => Pimage); }
            set { SetValue(() => Pimage, value); }
        }

        public int Quantity
        {
            get { return GetValue(() => Quantity); }
            set { SetValue(() => Quantity, value); }
        }
        public decimal Price
        {
            get { return GetValue(() => Price); }
            set { SetValue(() => Price, value); }
        }
    }
}
