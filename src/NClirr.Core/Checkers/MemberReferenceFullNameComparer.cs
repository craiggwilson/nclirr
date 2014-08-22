using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core.Checkers
{
    public class MemberReferenceFullNameComparer : IComparer<MemberReference>
    {
        public int Compare(MemberReference x, MemberReference y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return 1;
            }
            if (y == null)
            {
                return -1;
            }

            return x.FullName.CompareTo(y.FullName);
        }
    }
}
