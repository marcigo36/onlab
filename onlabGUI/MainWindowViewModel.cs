using INotify;
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
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        //global variables for pricing
        DiscountCurve _dc;
        public DiscountCurve dc
        {
            get => _dc;
            set
            {
                _dc = value;
                OnPropertyChanged("dc");
            }
        }

        YieldCurve _yc;
        public YieldCurve yc
        {
            get => _yc;
            set
            {
                _yc = value;
                dc = (DiscountCurve)yc;
                OnPropertyChanged("yc");
            }
        }

        HullWhiteCalibrator hc;
        HullWhiteProcess hw;

        //istruments to be priced
        //should bind to the UI
        public Bond BondToPrice { get; set; }
        public double BondPrice { get; private set; }

        public BondOption BondOptionToPrice { get; set; }
        public double BondOptionPrice { get; private set; }

        //plottable form of important curves form HW model
        public IList<DataPoint> YieldCurvePoints { get; private set; }
        public IList<DataPoint> ForwardRatePoints{ get; private set; }
        public IList<DataPoint> ThetaPoints { get; private set; }


        //samples from calibrated Hull-White process
        public IList<DataPoint> HW1Points { get; private set; }
        public IList<DataPoint> HW2Points { get; private set; }
        public IList<DataPoint> HW3Points { get; private set; }

        public void RegenerateAllData()
        {
            if (dc == null) return;

            hc = new HullWhiteCalibrator();
            hw = hc.Calibrate(dc);

            GenerateCurves();
            GenerateHullWhiteInstances();

        }

        public void Price() 
        {
            var pricer = new MonteCarloPricer(hw, 0.01);

            BondPrice = pricer.priceBond(BondToPrice, 10);
            OnPropertyChanged("BondPrice");

            BondOptionPrice = pricer.priceBondOption(BondOptionToPrice, 10);
            OnPropertyChanged("BondOptionPrice");
        }



        void GenerateCurves()
        {
            this.ForwardRatePoints = Utils.Plot(hc.ForwardRate, 0.0, 30.0, 0.01).ToList();
            OnPropertyChanged("ForwardRatePoints");
            this.ThetaPoints = Utils.Plot(hc.Theta, 0.0, 30.0, 0.01).ToList();
            OnPropertyChanged("ThetaPoints");
            this.YieldCurvePoints = Utils.Plot(yc, 0.0, 30.0, 0.01).ToList();
            OnPropertyChanged("YieldCurvePoints");
        }

        void GenerateHullWhiteInstances()
        {
            double t = 0;
            this.HW1Points = new List<DataPoint>(hw.generatePath(0.01,30).Select(fx => new DataPoint(t += 0.01, fx)));
            OnPropertyChanged("HW1Points");
            t = 0;
            this.HW2Points = new List<DataPoint>(hw.generatePath(0.01, 30).Select(fx => new DataPoint(t += 0.01, fx)));
            OnPropertyChanged("HW2Points");
            t = 0;
            this.HW3Points = new List<DataPoint>(hw.generatePath(0.01, 30).Select(fx => new DataPoint(t += 0.01, fx)));
            OnPropertyChanged("HW3Points");
        }

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
