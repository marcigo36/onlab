using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlab
{
    public class Bond
    {
        public Bond()
        {
            FaceValue = 1;
            Maturity = 20;
            Coupon = CouponPaymentType.Annual;
            Rate = 0.01;
        }
        public enum CouponPaymentType {
            None,
            Annual,
            SemiAnnual,
            Quarterly };

        //this amount will be paid to the buyer after maturity
        public double FaceValue { set; get; }

        //the instrument will mature after this many years from now
        public double Maturity { set; get; }

        public CouponPaymentType Coupon { set; get; }

        //yearly rate to be paid
        public double Rate { set; get; }

        public double CouponInterval {
            get
            {
                switch (Coupon)
                {
                    case Bond.CouponPaymentType.Annual:
                        return 1;
                    case Bond.CouponPaymentType.SemiAnnual:
                        return 0.5;
                    case Bond.CouponPaymentType.Quarterly:
                        return 0.25;
                    default:
                        return 0;
                }
            }
        }
    }
}
