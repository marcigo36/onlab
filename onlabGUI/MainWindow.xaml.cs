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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ycSelector_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //MS did not fix the OnewayToSource binding bug for years.... how on earth is this possible?
            //https://connect.microsoft.com/VisualStudio/feedback/details/540833/onewaytosource-binding-from-a-readonly-dependency-property
            //this is ugly
            if (e.PropertyName == "Curve")(this.DataContext as MainWindowViewModel).yc = ycSelector.Curve;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MainWindowViewModel).RegenerateAllData();
        }

        private void pricebtn_Click(object sender, RoutedEventArgs e)
        {
            var data = (this.DataContext as MainWindowViewModel);
            data.BondToPrice = (bondinput.DataContext as Bond);
            data.BondOptionToPrice = (bondoptioninput.DataContext as BondOption);

            data.RegenerateAllData();
            data.Price();
        }
    }
}
