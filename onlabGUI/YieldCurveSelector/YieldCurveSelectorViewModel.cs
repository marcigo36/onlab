using onlab;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlabGUI
{
    public class YieldCurveSelectorViewModel: INotifyPropertyChanged
    {
        public void UpdateDiscountCurve(DiscountCurve curve)
        {
            this.DiscountCurveAsOxyPlotCurve = Utils.Plot(curve, 0.0, 30.0, 0.01).ToList();
            OnPropertyChanged("DiscountCurveAsOxyPlotCurve");
        }

        public IList<DataPoint> DiscountCurveAsOxyPlotCurve { get; private set; }

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
