using onlab;
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

namespace onlabGUI
{
    /// <summary>
    /// Interaction logic for BondInput.xaml
    /// </summary>
    public partial class BondInput : UserControl
    {
        public BondInput()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bond = this.DataContext as Bond;
            //this is very ugly, it should be solved with binding and dataconverter..
            bond.Coupon = (Bond.CouponPaymentType)couponcombobox.SelectedIndex;
        }
    }
}
