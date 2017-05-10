using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlab
{
    public struct BondOption
    {
        public enum Type { Put, Call };

        public Bond UnderlyingBond { set; get; }

        //this amount will be paid to the buyer after maturity
        public double StrikePrice { set; get; }

        //the borrower can decide if he/she can buy the given instrument at the given price
        public double ExecutionTime { set; get; }
    }
}
