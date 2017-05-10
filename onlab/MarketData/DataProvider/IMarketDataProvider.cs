﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlab
{
    public interface IMarketDataProvider
    {
        YieldCurve GetYieldCurve();

        YieldCurve GetYieldCurve(DateTime date);
    }
}
