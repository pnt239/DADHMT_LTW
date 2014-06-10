using System;
using System.Drawing;

namespace Untipic.Core
{
    public class Viewport
    {
        public Viewport(float res, float zoom)
        {
            _res = res;
            _zoom = zoom;
        }

        public float WinToView(float value)
        {
            return RoundToInt(value * _res * _zoom);
        }

        public PointF WinToView(PointF point)
        {
            return new PointF(WinToView(point.X), WinToView(point.Y));
        }

        public SizeF WinToView(SizeF size)
        {
            return new SizeF(WinToView(size.Width), WinToView(size.Height));
        }

        public ShapeBase WinToView(ShapeBase shape)
        {
            if (shape == null)
                return null;

            var ret = shape.Clone();
            ret.Location = WinToView(shape.Location);
            ret.Size = WinToView(shape.Size);
            ret.OutlineWidth = WinToView(shape.OutlineWidth);

            for (int i = 0; i < shape.Vertices.Count; i++)
            {
                ret.Vertices[i].X = WinToView(shape.Vertices[i].X);
                ret.Vertices[i].Y = WinToView(shape.Vertices[i].Y);
            }

            return ret;
        }

        public float ViewToWin(float value)
        {
            return value / _res / _zoom;
        }

        public PointF ViewToWin(Point point)
        {
            return new PointF(ViewToWin(point.X), ViewToWin(point.Y));
        }

        public PointF ViewToWin(PointF point)
        {
            return new PointF(ViewToWin(point.X), ViewToWin(point.Y));
        }

        public SizeF ViewToWin(SizeF size)
        {
            return new SizeF(ViewToWin(size.Width), ViewToWin(size.Height));
        }

        public ShapeBase ViewToWin(ShapeBase shape)
        {
            var ret = shape.Clone();
            ret.Location = ViewToWin(shape.Location);
            ret.Size = ViewToWin(shape.Size);
            ret.OutlineWidth = ViewToWin(shape.OutlineWidth);

            for (int i = 0; i < shape.Vertices.Count; i++)
            {
                ret.Vertices[i].X = ViewToWin(shape.Vertices[i].X);
                ret.Vertices[i].Y = ViewToWin(shape.Vertices[i].Y);
            }

            return ret;
        }

        private float RoundToInt(float value)
        {
            return (float)Math.Round(value);
        }

        private float _res;
        private float _zoom;
    }
}
