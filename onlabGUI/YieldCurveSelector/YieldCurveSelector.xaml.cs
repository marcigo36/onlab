using onlab;
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
    /// Interaction logic for YieldCurveSelector.xaml
    /// </summary>
    public partial class YieldCurveSelector : UserControl, INotifyPropertyChanged
    {
        public YieldCurveSelector()
        {
            InitializeComponent();

            //we initially set the current curve in the editor
            ycEditor.Curve = ycLoader.Curve;

            qndlTab.SetBinding(YieldCurveSelector.CurveSourceProperty, new Binding() { Source = ycLoader, Path = new PropertyPath("Curve") });
            customTab.SetBinding(YieldCurveSelector.CurveSourceProperty, new Binding() { Source = ycEditor, Path = new PropertyPath("Curve") });
        }

        YieldCurve yc;

        public YieldCurve Curve
        {
            get => yc;
            private set
            {
                yc = value;
                OnPropertyChanged("Curve");
            }
        }

        private void UpdateCurve()
        {
            //getting the attached yield curve from the active tab
            var source = ((TabItem)(tabcontrol.SelectedItem))?.GetValue(YieldCurveSelector.CurveSourceProperty);

            if(source == null)return;

            Curve = source as YieldCurve;
            (DataContext as YieldCurveSelectorViewModel).UpdateDiscountCurve((DiscountCurve)yc);
            //this is incredibly ugly.
            
        }

        private void ycSource_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Curve") UpdateCurve();
        }

        public static readonly DependencyProperty CurveSourceProperty = DependencyProperty.RegisterAttached(
            "CurveSource",
            typeof(YieldCurve),
            typeof(YieldCurveSelector),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender)
        );

        public static void SetCurveSource(UIElement element, YieldCurve value)
        {
            element.SetValue(CurveSourceProperty, value);
        }
        public static YieldCurve GetCurveSource(UIElement element)
        {
            return (YieldCurve)element.GetValue(CurveSourceProperty);
        }

        private void tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender == tabcontrol) UpdateCurve();
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
