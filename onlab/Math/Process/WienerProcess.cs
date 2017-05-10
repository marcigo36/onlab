using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlab
{
    public class WienerProcess
    {
        private double nu, sigma;

        public double this[double t]
        {
            get
            {
                return new NormalDistribution( nu, sigma*t).NextVal();
            }
        }

        public IEnumerable<double> generatePath(double step, double maxt)
        {
            double t = 0;
            double X = nu;
           
            for (; t < maxt; t += step)
            {
                yield return X;

                X += NormalDistribution.GetValue(0.0, sigma * step);
            }
        }
    }
}
