using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlab
{
    public class HullWhiteProcess : IProcess
    {
        public HullWhiteProcess(Func<double, double> theta, double a, double sigma)
        {
            this.a = a;
            this.sigma = sigma;
            this.theta = theta;                                                                  
        }

        private double a, sigma;
        private Func<double, double> theta;

        public double this[double t]
        {
            get
            {
                const double defaultTimeStep = 0.0001;

                return generatePath(defaultTimeStep, t).Last();
            }
        }

        public IEnumerable<double> generatePath(double step, double maxt)
        {
            double t = 0;
            double X = theta(0);

            for (; t < maxt; t += step)
            {
                yield return X;

                //step by step solution to the HW formula
                var th = theta(t);
                var dr = (theta(t) - a * X) * step;
                X += (theta(t) - a*X)*step + NormalDistribution.GetValue(0.0, sigma * step);
            }
        } 
    }   
}
