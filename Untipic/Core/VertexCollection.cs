using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Untipic.Core
{
    public class VertexCollection : IVertexCollection
    {
        private List<IVertex> _list;
        private int _count;
        private bool _isReadOnly;

        public VertexCollection()
        {
            _list = new List<IVertex>();
        }

        public IEnumerator<IVertex> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IVertex item)
        {
            // Check null
            if (item == null)
                return;

            // The vertex is valid.  Add it to the collection
            _list.Add(item);

            // Fire a VertexAdded event if necessary.
            VertexEventHandler oVertexAdded = this.VertexAdded;

            if (oVertexAdded != null)
            {
                oVertexAdded(this, new VertexEventArgs(item));
            }
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(IVertex item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(IVertex[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(IVertex item)
        {
            bool ret = _list.Remove(item);

            // Fire a VertexRemoved event if necessary
            VertexEventHandler oVertexRemoved = this.VertexRemoved;

            if (oVertexRemoved != null)
            {
                oVertexRemoved(this, new VertexEventArgs(item));
            }

            return ret;
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        public int IndexOf(IVertex item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, IVertex item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public IVertex this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        public IVertex Add()
        {
            IVertex vertex = new Vertex();

            Add(vertex);

            return (vertex);
        }

        public IList<Point> ToList()
        {
            var points = new List<Point>();

            for (int i = 0; i < Count; i++)
                points.Add(_list[i].ToPoint());

            return points;
        }

        public Point[] ToPoints()
        {
            var points = new Point[Count];

            for (int i = 0; i < Count; i++)
                points[i] = _list[i].ToPoint();

            return points;
        }

        public IVertexCollection Clone()
        {
            var t = new VertexCollection();

            foreach (var v in _list)
                t._list.Add(v.Clone());

            return t;
        }

        public event VertexEventHandler VertexAdded;
        public event VertexEventHandler VertexRemoved;
    }
}
