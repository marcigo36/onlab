using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlab
{
    public class HullWhiteCalibrator
    {
        public static Func<double, double> theta;
        public static Func<double, double> f_M;
        const double eps = 0.00001;
        //poor man's derive
        private static double Derive(Func<double, double> f, double x)
        {
            try
            {
                double fx0 = f(x), fx1 = f(x + eps);
                double dy =  fx1 - fx0;
                double dx = eps;

                return dy / dx;
            }
            catch (ArgumentOutOfRangeException e)
            {
                double dy = f(x) - f(x - eps);
                double dx = eps;

                return dy / dx;
            }
        }
        
        public static HullWhiteProcess Calibrate()
        {
            var mktdata = QuandlDataProvider.Instance;

            var disc = (DiscountCurve)mktdata.GetYieldCurve();

            //preset parameters, calibrating these is skipped due to the lack of openly accessible market data
            //might worth implementing later
            double a = 0.31, sigma = 0.008;

            //market instantaneous forward rate
            /*Func<double, double>*/ f_M = 
                
                T => -(disc[T+eps]-disc[T])/(disc[T + eps]*eps);

            /*Func<double, double>*/ theta = 
                
                t => Derive(x => f_M(x), t) 
                   + a*f_M(t) 
                   + sigma*sigma/(2.0*a*a)*(1.0 - Math.Exp(-2.0*a*t));


            Func<double, double> alpha =

                t => a * f_M(t)
                   + sigma * sigma / (2.0 * a * a) * (1.0 - Math.Exp(-2.0 * a * t));



            return new HullWhiteProcess(theta, a, sigma);
        }
    }
}
