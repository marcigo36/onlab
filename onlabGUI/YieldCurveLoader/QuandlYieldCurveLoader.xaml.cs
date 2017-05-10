using onlab;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for QuandlYieldCurveLoader.xaml
    /// </summary>
    public partial class QuandlYieldCurveLoader : UserControl
    {
        public QuandlYieldCurveLoader()
        {
            InitializeComponent();
            Curve = onlab.QuandlDataProvider.Instance.GetYieldCurve(DateTime.Now);

            (DataContext as QuandlYieldCurveLoaderViewModel).UpdateCurve(Curve);

        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Curve = onlab.QuandlDataProvider.Instance.GetYieldCurve(datePicker.SelectedDate ?? DateTime.Now);
            OnPropertyChanged("Curve");

            (DataContext as QuandlYieldCurveLoaderViewModel).UpdateCurve(Curve);
        }

        public YieldCurve Curve { get; private set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
