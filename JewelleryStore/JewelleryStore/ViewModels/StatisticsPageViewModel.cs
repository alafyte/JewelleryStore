using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JewelleryStore
{
    public class StatisticsPageViewModel : PropertyChangedNotification
    {
        DatabaseUnit db = new DatabaseUnit();

        public Cursor Cursor { get; set; }
        public SeriesCollection SeriesCollection 
        {
            get { return GetValue(() => SeriesCollection); }
            set { SetValue(() => SeriesCollection, value); }
        }
        public Func<ChartPoint, string> PointLabel { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public string TotalProfit
        {
            get { return GetValue(() => TotalProfit); }
            set { SetValue(() => TotalProfit, value); }
        }
        public string TotalItems
        {
            get { return GetValue(() => TotalItems); }
            set { SetValue(() => TotalItems, value); }
        }
        public class Info : PropertyChangedNotification
        {
            public string Information
            {
                get { return GetValue(() => Information); }
                set { SetValue(() => Information, value); }
            }
            public Info(string information)
            {
                Information = information;
            }
        }
        public ObservableCollection<Info> Bills
        {
            get { return GetValue(() => Bills); }
            set { SetValue(() => Bills, value); }
        }
        public StatisticsPageViewModel() 
        {
            Cursor = CursorCollection.GetCursor();
            PointLabel = chartPoint =>
             string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            TotalProfit = totalProfit().ToString() + "$";
            TotalItems = itemsTotal().ToString();

            Settings.changeLang += InitDiagram;
            InitDiagram();

            Bills = new ObservableCollection<Info>();
            foreach(var bill in db.Bills.GetAll())
            {
                Bills.Add(new Info(bill.BillId + "\t  " + bill.DateOfOrder.ToString("dd/MM/yyyy HH:mm:ss") + "\t  $" + bill.TotalPrice.ToString()));
            }
        }
        private void InitDiagram()
        {
            int necklaces = count(1);
            int rings = count(2);
            int earrings = count(3);
            int wristwear = count(4);
            SeriesCollection = new SeriesCollection {
                new PieSeries
                {
                    Title = (string) App.Current.Resources["Necklaces"],
                    Values = new ChartValues<ObservableValue> { new ObservableValue(necklaces)},
                    DataLabels = true

                },
                new PieSeries
                {
                    Title = (string) App.Current.Resources["Rings"],
                    Values = new ChartValues<ObservableValue> { new ObservableValue(rings)},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = (string)App.Current.Resources["Earrings"],
                    Values = new ChartValues<ObservableValue> { new ObservableValue(earrings)},
                    DataLabels = true

                },
                new PieSeries
                {
                    Title = (string)App.Current.Resources["Wristwear"],
                    Values = new ChartValues<ObservableValue> { new ObservableValue(wristwear)},
                    DataLabels = true

                }
            };
        }
        private int count(int id)
        {
            int sum = 0;
            foreach (var bill in db.Bills.GetAll())
            {
                foreach (var order in bill.OrderInfos.ToList())
                {
                    var product = db.Products.Get(order.ProductCode);
                    if (product.ProductType == id)
                    {
                        sum += order.Quantity;
                    }
                }
            }
            return sum;
        }
        private int itemsTotal()
        {
            int n = 0;
            foreach (var bill in db.Bills.GetAll())
            {
                foreach (var order in bill.OrderInfos.ToList())
                {
                    n += order.Quantity;
                }
            }

            return n;
        }
        private decimal totalProfit()
        {
            decimal sum = 0;
            foreach (var bill in db.Bills.GetAll())
            {
                sum += bill.TotalPrice;
            }

            return sum;
        }
    }
}
