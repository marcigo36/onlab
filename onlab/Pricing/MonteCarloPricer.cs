using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlab
{
    public class MonteCarloPricer
    {
        public MonteCarloPricer(IProcess process, double T, double step)
        {
            this.T = T;
            this.step = step;
            this.process = process;
        }

        double T, step;
        IProcess process;
        public List<IEnumerable<double>> paths;
        public List<IEnumerable<double>> zbpCurves;

        public List<double> zbpAvgCurve;

        IEnumerable<double> pathToZeroCouponBondPriceCurve(IEnumerable<double> path)
        {
            //bond price at T is 1
            double zbp = 1;

            List<double> ret = new List<double>();

            //we roll back the time from T, and add it to the curve
            foreach (double r in path)
            {
                ret.Add(zbp);
                //this is the equivalent of integration, based on the zero coupon bond price formula
                zbp *= Math.Exp(-r * step);
            }

            //reversing, to get the curve from t to T
            return ret;
        }

        public void Price(int n)
        {
            paths = Enumerable.Range(0, n).Select(_ => process.generatePath(step, T)).ToList();
            zbpCurves = paths.Select(p => pathToZeroCouponBondPriceCurve(p)).ToList();

            //zbpCurves.Aggregate((p1,p2) => p1.Zip(p2,))
        }
    }
}
