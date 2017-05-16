using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlab
{
    public class BondOption
    {
        public BondOption()
        {
            UnderlyingBond = new Bond();
            StrikePrice = 0.7;
            ExecutionTime = 10;
            OptionType = Type.Call;
        }
        public enum Type { Call, Put };

        public Type OptionType { get; set; }

        public Bond UnderlyingBond { set; get; }

        //this amount will be paid to the buyer after maturity
        public double StrikePrice { set; get; }

        //the borrower can decide if he/she can buy the given instrument at the given price
        public double ExecutionTime { set; get; }
    }
}
