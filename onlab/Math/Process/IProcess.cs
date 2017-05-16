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

        IEnumerable<double> generatePathFrom(double X0, double t0, double step, double maxt);
    }
}
