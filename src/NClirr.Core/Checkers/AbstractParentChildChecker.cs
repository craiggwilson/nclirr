using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core.Checkers
{
    public abstract class AbstractParentChildChecker<TParent, TChild> : IChecker<TParent>
        where TChild : class
    {
        private readonly IComparer<TChild> _comparer;

        protected AbstractParentChildChecker(IComparer<TChild> comparer)
        {
            _comparer = comparer;
        }

        public IEnumerable<ApiDifference> Check(TParent oldParent, TParent newParent)
        {
            var enumerator = new CoEnumerator<TChild>(
                GetChildren(oldParent),
                GetChildren(newParent),
                _comparer);

            try
            {
                while (enumerator.MoveNext())
                {
                    IEnumerable<ApiDifference> differences = null;
                    if (enumerator.CurrentRight == null)
                    {
                        differences = ChildRemoved(oldParent, enumerator.CurrentLeft);
                    }
                    else if (enumerator.CurrentLeft == null)
                    {
                        differences = ChildAdded(oldParent, enumerator.CurrentRight);
                    }
                    else if (enumerator.CurrentLeft != null && enumerator.CurrentRight != null)
                    {
                        differences = CompareChildren(oldParent, enumerator.CurrentLeft, enumerator.CurrentRight);
                    }

                    if(differences != null)
                    {
                        foreach(var difference in differences)
                        {
                            yield return difference;
                        }
                    }
                }
            }
            finally
            {
                enumerator.Dispose();
            }
        }

        protected virtual IEnumerable<ApiDifference> ChildAdded(TParent oldParent, TChild newChild)
        {
            return Enumerable.Empty<ApiDifference>();
        }

        protected virtual IEnumerable<ApiDifference> ChildRemoved(TParent oldParent, TChild oldChild)
        {
            return Enumerable.Empty<ApiDifference>();
        }

        protected virtual IEnumerable<ApiDifference> CompareChildren(TParent oldParent, TChild oldChild, TChild newChild)
        {
            return Enumerable.Empty<ApiDifference>();
        }

        protected abstract IEnumerable<TChild> GetChildren(TParent parent);
    }
}