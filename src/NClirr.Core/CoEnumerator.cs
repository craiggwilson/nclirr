using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NClirr.Core
{
    public class CoEnumerator<T> : IDisposable
        where T : class
    {
        private readonly IComparer<T> _comparer;
        private readonly IEnumerator<T> _left;
        private readonly IEnumerator<T> _right;
        private bool _hasLeft;
        private bool _hasRight;
        private T _currentLeft;
        private T _currentRight;
        private bool _started;

        public CoEnumerator(IEnumerable<T> left, IEnumerable<T> right, IComparer<T> comparer)
        {
            _left = left.OrderBy(x => x, comparer).GetEnumerator();
            _right = right.OrderBy(x => x, comparer).GetEnumerator();
            _comparer = comparer;

            _hasLeft = false;
            _hasRight = false;
            _started = false;
        }

        public T CurrentLeft
        {
            get { return _currentLeft; }
        }

        public T CurrentRight
        {
            get { return _currentRight; }
        }

        public bool MoveNext()
        {
            if(!_started)
            {
                _started = true;
                _hasLeft = _left.MoveNext();
                _hasRight = _right.MoveNext();
            }

            int order;

            if(!_hasLeft && !_hasRight)
            {
                _currentLeft = null;
                _currentRight = null;
                return false;
            }

            if(_hasLeft && !_hasRight)
            {
                order = -1;
            }
            else if(!_hasLeft && _hasRight)
            {
                order = 1;
            }
            else
            {
                order = _comparer.Compare(_left.Current, _right.Current);
            }

            if(order < 0)
            {
                _currentLeft = _left.Current;
                _hasLeft = _left.MoveNext();
                _currentRight = null;
            }
            else if(order > 0)
            {
                _currentLeft = null;
                _currentRight = _right.Current;
                _hasRight = _right.MoveNext();
            }
            else
            {
                _currentLeft = _left.Current;
                _hasLeft = _left.MoveNext();
                _currentRight = _right.Current;
                _hasRight = _right.MoveNext();
            }

            return true;
        }

        public void Dispose()
        {
            _left.Dispose();
            _right.Dispose();
        }
    }
}