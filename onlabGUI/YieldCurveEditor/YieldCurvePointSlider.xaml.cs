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
    /// Interaction logic for YieldCurvePointSlider.xaml
    /// </summary>
    public partial class YieldCurvePointSlider : UserControl, INotifyPropertyChanged
    {
        public YieldCurvePointSlider()
        {
            InitializeComponent();
        }

        double val;
        string interval;

        public double Value { get => val; set { val = value; OnPropertyChanged("Value"); } }

        public string IntervalString { get => interval; set { interval = value; OnPropertyChanged("IntervalString"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
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
