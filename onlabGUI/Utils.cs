using onlab;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlabGUI
{
    class Utils
    {
        public static IEnumerable<DataPoint> Plot(Func<double, double> f, double from, double to, double step)
        {
            for (double t = from; t <= to; t += step)
            {
                yield return new DataPoint(t, f(t));
            }
        }

        public static IEnumerable<DataPoint> Plot(ICurve c, double from, double to, double step)
        {
            for (double t = from; t <= to; t += step)
            {
                yield return new DataPoint(t, c[t]);
            }
        }
    }
}
