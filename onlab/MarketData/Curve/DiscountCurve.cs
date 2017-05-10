using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlab
{
    public class DiscountCurve : MarketCurve
    {
        public DiscountCurve(ICurve impl) : base(impl) { }

        public static explicit operator YieldCurve(DiscountCurve _this)
        {
            return new YieldCurve(_this.Transform(p => (1.0 - p.x) / (p.y * p.x)));
        }
    }
}
  