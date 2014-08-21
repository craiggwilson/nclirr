using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NClirr.Core
{
    public class FilteringReporter : IReporter
    {
        private readonly IReporter _nested;
        private readonly Func<ApiDifference, bool> _predicate;

        public FilteringReporter(IReporter nested, Func<ApiDifference, bool> predicate)
        {
            _nested = nested;
            _predicate = predicate;
        }

        public void Report(IEnumerable<ApiDifference> differences)
        {
            _nested.Report(differences.Where(_predicate));
        }
    }
}
