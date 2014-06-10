using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Untipic.Core;

namespace Untipic.Visualization.FillAlgorithm
{
    public class ScanLine
    {
        public void ScanLineFillPolygon(Graphics graph, PolygonBase shape, Color color)
        {
            var rec = new Rectangle(Point.Round(shape.Location), Size.Round(shape.Size));

            //var h = rec.Y + 1;
            var h = rec.Y + rec.Height;
            var et = new SortedDoublyLinkedList<CActiveEdge>[h];
            var active = new SortedDoublyLinkedList<CActiveEdge>();

            for (int i = 0; i < h; i++)
                et[i] = new SortedDoublyLinkedList<CActiveEdge>();

            BuildEdgeList(shape.Vertices.ToList(), ref et);

            //for (int i = rec.Y - rec.Height; i < rec.Y; i++)
            for (int i = rec.Y; i < rec.Y + rec.Height; i++)
            {
                buildActiveList(ref active, ref et[i]);
                if (active.Count != 0)
                {
                    using (var p = new Pen(color, 1F))
                        FillScan(i, ref active, graph, p);
                    updateEdgeList(i, ref active);
                    active.Sort();
                }
            }
        }

        public void ScanLineFillEllipse(Graphics graph, Ellipse shape, Color color)
        {
            int rx = (int) Math.Round(shape.MajorAxis);
            int ry = (int)Math.Round(shape.MinorAxis);
            var o = Point.Round(shape.OrginalPoint.ToPoint());

            var penline = new Pen(color, 1F);

            int x = 0, y = ry;
            int c1 = 2 * ry * ry * x, c2 = 2 * rx * rx * y;
            float p = ry * ry - rx * rx * ry + 0.25F * rx * rx;

            while (c1 < c2)
            {
                Fill2Line(o.X, o.Y, x, y, graph, penline);

                x++;
                if (p < 0)
                {
                    c1 += 2 * ry * ry;
                    p += c1 + ry * ry;
                }
                else
                {
                    y--;
                    c1 += 2 * ry * ry;
                    c2 -= 2 * rx * rx;
                    p += c1 - c2 + ry * ry;
                }
            }

            c1 = 2 * rx * rx * y;
            c2 = 2 * ry * ry * x;
            p = ry * ry * (x + 0.5F) * (x + 0.5F) + rx * rx * (y - 1) * (y - 1) - rx * rx * ry * ry;

            while (y != 0)
            {
                Fill2Line(o.X, o.Y, x, y, graph, penline);

                y--;
                if (p > 0)
                {
                    c1 -= 2 * rx * rx;
                    p += rx * rx - c1;
                }
                else
                {
                    x++;
                    c1 -= 2 * rx * rx;
                    c2 += 2 * ry * ry;
                    p += c2 - c1 + rx * rx;
                }
            }
            Fill2Line(o.X, o.Y, x, y, graph, penline);

            penline.Dispose();
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
                        makeEdgeRec(ref v1, ref v2, yNext(i, cnt, ref points), ref et);
                    else             // down-going edge
                        makeEdgeRec(ref v2, ref v1, yPrev, ref et);
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

        private void FillScan(int line, ref SortedDoublyLinkedList<CActiveEdge> ae, Graphics graph, Pen penline)
        {
            var points = new CActiveEdge[2];
            int i = 0;

            foreach (var edge in ae)
            {
                points[i] = edge;

                if (i == 1)
                    FillLine((int)Math.Round(points[0].XIntersection, 0), (int)Math.Round(points[1].XIntersection, 0), line, graph, penline);

                i = (i + 1) % 2;
            }
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
                    p.Value.XIntersection += p.Value.ReciSlope;
                p = p.Next;
            }
        }

        private void makeEdgeRec(ref Point lower, ref Point upper, int yComp, ref SortedDoublyLinkedList<CActiveEdge>[] et)
        {
            var ae = new CActiveEdge
            {
                ReciSlope = (float)(upper.X - lower.X) / (upper.Y - lower.Y),
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

        private void FillLine(int x1, int x2, int y, Graphics graph, Pen penline)
        {
            graph.DrawLine(penline, x1, y, x2, y);
        }

        private void Fill2Line(int xc, int yc, int x, int y, Graphics graph, Pen penline)
        {
            FillLine(-x + xc, x + xc, y + yc, graph, penline);
            FillLine(-x + xc, x + xc, -y + yc, graph, penline);
        }
    }
}
