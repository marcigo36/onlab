using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlab
{
    public interface IProcess
    {
        double this[double t] { get; }

        IEnumerable<double> generatePath(double step, double maxt);
    }
}
