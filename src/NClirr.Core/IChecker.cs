using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NClirr.Core
{
    public interface IChecker<T>
    {
        IEnumerable<ApiDifference> Check(T a, T b);
    }
}