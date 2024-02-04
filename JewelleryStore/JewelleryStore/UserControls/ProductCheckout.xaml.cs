using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace JewelleryStore
{
    /// <summary>
    /// Interaction logic for ProductCheckout.xaml
    /// </summary>
    public partial class ProductCheckout : UserControl
    {
        public ProductCheckout()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty RemoveCommandProperty =
        DependencyProperty.Register("RemoveCommand", typeof(ICommand), typeof(ProductCheckout), new PropertyMetadata(null));

        public ICommand RemoveCommand
        {
            get { return (ICommand)GetValue(RemoveCommandProperty); }
            set { SetValue(RemoveCommandProperty, value); }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (RemoveCommand != null)
            {
                RemoveCommand.Execute(DataContext);
            }
        }
    }
}
