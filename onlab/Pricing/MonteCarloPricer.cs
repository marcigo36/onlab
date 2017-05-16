using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlab
{
    public class MonteCarloPricer
    {
        public MonteCarloPricer(IProcess process, double step)
        {
            this.step = step;
            this.process = process;
        }

        double step;
        IProcess process;
        //public List<IEnumerable<double>> paths;
        //public List<IEnumerable<double>> zbpCurves;

        //public List<double> zbpAvgCurve;

        //IEnumerable<double> pathToZeroCouponBondPriceCurve(IEnumerable<double> path)
        //{
        //    //bond price at T is 1
        //    double zbp = 1;

        //    List<double> ret = new List<double>();

        //    //we roll back the time from T, and add it to the curve
        //    foreach (double r in path)
        //    {
        //        ret.Add(zbp);
        //        //this is the equivalent of integration, based on the zero coupon bond price formula
        //        zbp *= Math.Exp(-r * step);
        //    }

        //    //reversing, to get the curve from t to T
        //    return ret;
        //}

        public void Price(int n)
        {
            //paths = Enumerable.Range(0, n).Select(_ => process.generatePath(step, T)).ToList();
            //zbpCurves = paths.Select(p => pathToZeroCouponBondPriceCurve(p)).ToList();

            //zbpCurves.Aggregate((p1,p2) => p1.Zip(p2,))
        }

        public double priceBond(Bond b, int n, List<IEnumerable<double>> pregeneratedPaths = null)
        {
            double[] ret = new double[n];
            double T = b.Maturity;

            List<IEnumerable<double>> paths;
            if (pregeneratedPaths == null) paths = Enumerable.Range(0, n).Select(_ => process.generatePath(step, T)).ToList();
            else paths = pregeneratedPaths;

            double[] coupons = new double[0];
            double[] coupontimes = new double[0];
            
            switch (b.Coupon)
            {
                case Bond.CouponPaymentType.None:
                    //no coupon payments
                    break;

                case Bond.CouponPaymentType.Annual:
                    int cpn = (T % 1 == 0 ? (int)(T - 1) : (int)T);
                    coupons = new double[cpn];
                    coupontimes = new double[cpn];
                    for (int i = 1; i <= coupons.Length; i++)
                    {
                        coupontimes[i-1] = i;
                    }
                    break;


                case Bond.CouponPaymentType.SemiAnnual:
                    cpn = ((2.0 * T) % 1 == 0 ? (int)((2.0 * T) - 1) : (int)(2.0 * T));
                    coupons = new double[cpn];
                    coupontimes = new double[cpn];
                    for (int i = 1; i <= coupons.Length; i++)
                    {
                        coupontimes[i-1] = 0.5*i;
                    }
                    break;

                case Bond.CouponPaymentType.Quarterly:
                    cpn = ((4.0 * T) % 1 == 0 ? (int)((4.0 * T) - 1) : (int)(4.0 * T));
                    coupons = new double[cpn];
                    coupontimes = new double[cpn];
                    for (int i = 1; i <= coupons.Length; i++)
                    {
                        coupontimes[i-1] = 0.25 * i;
                    }
                    break;

                default:
                    //no coupon payments
                    break;
            }

            int reti = 0;
            foreach (var path in paths)
            {
                double zbp = 1;
                double t = 0;
                int nextcouponindex = 0;
                foreach (var r in path)
                {
                    t += step;
                    zbp *= Math.Exp(-r * step);
                    if(nextcouponindex < coupontimes.Count() && coupontimes[nextcouponindex] < t)
                    {
                        coupons[nextcouponindex] = zbp * b.FaceValue * b.Rate * b.CouponInterval;
                        nextcouponindex++;
                    }
                }

                //these coupons are now at present value
                ret[reti] = b.FaceValue * zbp + coupons.Sum();

                reti++;

            }

            return ret.Average();
        }

        public double priceBondOption(BondOption b, int n)
        {
            double[] ret = new double[n];
            double[] zbp = new double[n];

            Bond u_ = new Bond()
            {
                Coupon = b.UnderlyingBond.Coupon,
                FaceValue = b.UnderlyingBond.FaceValue,
                Maturity = b.UnderlyingBond.Maturity - b.ExecutionTime,
                Rate = b.UnderlyingBond.Rate
            };


            List<IEnumerable<double>> pathsToStrike = Enumerable.Range(0, n).Select(_ => process.generatePath(step, b.ExecutionTime)).ToList();

            int reti = 0;
            foreach (var path in pathsToStrike)
            {
                zbp[reti] = 1;
                foreach (var r in path)
                {
                    zbp[reti] *= Math.Exp(-r * step);
                }

                ret[reti] = priceBond(u_, n, Enumerable.Range(0, n).Select(_ => process.generatePathFrom(path.Last(), b.ExecutionTime, step, b.UnderlyingBond.Maturity)).ToList());
                reti++;
            }

            for (int i = 0; i < ret.Count(); i++)
            {
                if (b.OptionType == BondOption.Type.Call)
                {
                    ret[i] = zbp[i] * Math.Max(ret[i] - b.StrikePrice, 0);
                }
                else //put
                {
                    ret[i] = zbp[i] * Math.Max(b.StrikePrice - ret[i], 0);
                }
            }


            return ret.Average();
        }
    }
}
