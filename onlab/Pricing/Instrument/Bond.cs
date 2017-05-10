using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlab
{
    public class Bond
    {
        public enum CouponPaymentType { None, Annual, SemiAnnual, Quarterly };

        //this amount will be paid to the buyer after maturity
        public double FaceValue { set; get; }

        //the instrument will mature after this many years from now
        public double Maturity { set; get; }

        public CouponPaymentType Coupon { set; get; }

        //yearly rate to be paid
        public double Rate { set; get; }
    }
}
