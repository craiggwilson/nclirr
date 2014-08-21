using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NClirr.Core;

namespace NClirr.Core
{
    public interface IReporter
    {
        void Report(IEnumerable<ApiDifference> differences);
    }
}