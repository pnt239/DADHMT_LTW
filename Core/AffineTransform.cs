using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace DrawPad
{
    public class AffineTransform
    {
        private float[,] _matrix;

        public AffineTransform()
        {
            _matrix = new float[3, 3];
            for (int i = 0; i < 3; i++)
                _matrix[i, i] = 1;
        }

        public AffineTransform(float m00, float m01, float m10, float m11, float dx, float dy)
        {
            _matrix = new float[3, 3];
            _matrix[0, 0] = m00;
            _matrix[0, 1] = m01;
            _matrix[1, 0] = m10;
            _matrix[1, 1] = m11;
            _matrix[0, 2] = dx;
            _matrix[1, 2] = dy;
            _matrix[2, 2] = 1;
        }

        // Multiplies this Matrix by the matrix specified in the matrix parameter, by prepending the specified Matrix
        public void Multiply(AffineTransform matrix)
        {
            float[,] ret = new float[3, 3];

            for (int i=0; i<3; i++)
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 3; k++)
                        ret[i, j] += _matrix[i, k]*matrix._matrix[k, j];

            _matrix = ret;
        }

        // Applies the specified translation vector (offsetX and offsetY) to this Matrix by prepending the translation vector.
        public void Translate(float offsetX, float offsetY)
        {
            Multiply(new AffineTransform(1, 0, 0, 1, offsetX, offsetY));
        }

        // Prepend to this Matrix a clockwise rotation, around the origin and by the specified angle.
        public void Rotate(double angle)
        {
            float sin = (float)Math.Round(Math.Sin(angle), 15);
            float cos = (float)Math.Round(Math.Cos(angle), 15);

            Multiply(new AffineTransform(cos, -sin, sin, cos, 0, 0));
        }

        // Applies a clockwise rotation to this Matrix around the point specified in the point parameter, and by prepending the rotation.
        public void RotateAt(double angle, Point point)
        {
            Translate(point.X, point.Y);
            Rotate(angle);
            Translate(-point.X, -point.Y);
        }

        // Applies the specified scale vector to this Matrix by prepending the scale vector.
        public void Scale(float scaleX, float scaleY)
        {
            Multiply(new AffineTransform(scaleX, 0, 0, scaleY, 0, 0));
        }

        // Flip follow Ox axis
        public void FlipOx()
        {
            Multiply(new AffineTransform(1, 0, 0, -1, 0, 0));
        }

        // Flip follow Oy axis
        public void FlipOy()
        {
            Multiply(new AffineTransform(-1, 0, 0, 1, 0, 0));
        }

        // Applies the geometric transform represented by this Matrix to a specified points
        public Point TransformPoint(Point point)
        {
            AffineTransform copy = new AffineTransform();

            AffineTransform mtP = new AffineTransform();
            mtP._matrix[0, 0] = point.X;
            mtP._matrix[1, 0] = point.Y;
            mtP._matrix[2, 0] = 1;

            copy.Multiply(this);
            copy.Multiply(mtP);

            return new Point((int)copy._matrix[0, 0], (int)copy._matrix[1, 0]);
        }

        // Applies the geometric transform represented by this Matrix to a specified list of points
        public List<Point> TransformPoints(List<Point> points)
        {
            List<Point> ret = points.Select(TransformPoint).ToList();
            return ret;
        }

        public override string ToString()
        {
            string ret = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    ret += _matrix[i, j].ToString(CultureInfo.InvariantCulture) + " ";
                ret += Environment.NewLine;
            }
            return ret;
        }
    }
}
