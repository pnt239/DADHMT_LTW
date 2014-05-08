using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TabletC.Core;

namespace TabletC.Processor
{
    internal class CActiveEdge : IComparable<CActiveEdge>
    {
        public int YUper { get; set; }
        public double XIntersection { get; set; }
        public Polynomial Equation { get; set; }

        public int CompareTo(CActiveEdge other)
        {
            if (XIntersection < other.XIntersection)
                return -1;
            if (XIntersection > other.XIntersection)
                return 1;
            return 0;
        }
    }

    public class IntegralArea
    {
        public double CalculatePolygonArea(IShape shape)
        {
            double area = 0, a = 0, b = 0;
            var rec = Util.CreateBorder(shape);
            var h = rec.Y + rec.Height+1;
            var et = new SortedDoublyLinkedList<CActiveEdge>[h];
            var active = new SortedDoublyLinkedList<CActiveEdge>();

            for (int i = 0; i < h; i++)
                et[i] = new SortedDoublyLinkedList<CActiveEdge>();

            BuildEdgeList(shape.Vertices, ref et);

            a = rec.Y;

            for (int i = rec.Y; i < rec.Y + rec.Height+1; i++)
            {
                buildActiveList(ref active, ref et[i]);
                if (active.Count != 0)
                {

                    //FillScan(i, ref active, layer, fillColor);
                    if (isIntersection(i, ref active))
                    {
                        b = i;
                        area += CalculatePartArea(ref active, a, b+1);
                        a = i + 1;
                    }

                    updateEdgeList(i, ref active);
                    active.Sort();
                }
            }
            return area;
        }

        private double CalculatePartArea(ref SortedDoublyLinkedList<CActiveEdge> ae, double a, double b)
        {
            double area = 0;

            var points = new CActiveEdge[2];
            int i = 0;

            foreach (var edge in ae)
            {
                points[i] = edge;

                if (i == 1)
                {
                    area += points[1].Equation.Integral(a, b) - points[0].Equation.Integral(a, b);
                }

                i = (i + 1) % 2;
            }

            return area;
        }

        private void BuildEdgeList(IList<Point> points, ref SortedDoublyLinkedList<CActiveEdge>[] et)
        {
            var cnt = points.Count;
            int i, yPrev = points[cnt - 2].Y;

            Point v1 = points[cnt - 1];
            for (i = 0; i < cnt; i++)
            {
                Point v2 = points[i];
                if (v1.Y != v2.Y)
                {
                    // Nonhorizontal line
                    if (v1.Y < v2.Y) // up-going edge
                        MakeEdgeRec(ref v1, ref v2, yNext(i, cnt, ref points), ref et);
                    else             // down-going edge
                        MakeEdgeRec(ref v2, ref v1, yPrev, ref et);
                }
                yPrev = v1.Y;
                v1 = v2;
            }
        }

        private void buildActiveList(ref SortedDoublyLinkedList<CActiveEdge> dest, ref SortedDoublyLinkedList<CActiveEdge> source)
        {
            foreach (var edge in source)
                dest.Add(edge);
        }

        private bool isIntersection(int line, ref SortedDoublyLinkedList<CActiveEdge> ae)
        {
            var p = ae.First;

            while (p != null)
            {
                if (line >= p.Value.YUper)
                    return true;
                p = p.Next;
            }

            return false;
        }

        private void updateEdgeList(int line, ref SortedDoublyLinkedList<CActiveEdge> ae)
        {
            var p = ae.First;

            while (p != null)
            {
                if (line >= p.Value.YUper)
                {
                    ae.Remove(p);
                }
                else
                    p.Value.XIntersection += p.Value.Equation.Solve(line);
                p = p.Next;
            }
        }

        private void MakeEdgeRec(ref Point lower, ref Point upper, int yComp, ref SortedDoublyLinkedList<CActiveEdge>[] et)
        {
            var ae = new CActiveEdge
            {
                Equation = GenerateLineFunction(ref lower, ref upper),
                XIntersection = lower.X
            };

            // Make shorter edge
            if (upper.Y < yComp)
                ae.YUper = upper.Y - 1;
            else
                ae.YUper = upper.Y;
            et[lower.Y].Add(ae);
        }

        private int yNext(int k, int cnt, ref IList<Point> points)
        {
            int j;

            if (k + 1 > cnt - 1)
                j = 0;
            else
                j = k + 1;

            while (points[k].Y == points[j].Y)
                if (j + 1 > cnt - 1)
                    j = 0;
                else
                    j++;

            return points[j].Y;
        }

        private Polynomial GenerateLineFunction(ref Point p1, ref Point p2)
        {
            // phuong trinhf x = f(y)
            double m = (double)(p2.X - p1.X) / (p2.Y - p1.Y);
            double b = p1.X - m*p1.Y;
            return new Polynomial(b, m);
        }
    }
}
