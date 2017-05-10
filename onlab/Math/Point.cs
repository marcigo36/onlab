using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlab
{
    [DebuggerDisplay("{x}->{y}")]
    public struct Point
    {
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public double x, y;
    }
}
