using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;

namespace Untipic.Core
{
    public enum MessureUnit
    {
        Cm = 0,
        Inch,
        Pixels
    }

    public class Util
    {
        public const float Epsilon = 1.0e-15f;

        public static RectangleF GetShapeBoundF(ShapeBase shape)
        {
            return new RectangleF(shape.Location, shape.Size);
        }

        public static Rectangle GetShapeBound(ShapeBase shape)
        {
            return new Rectangle(Point.Round(shape.Location), Size.Round(shape.Size));
        }

        public static double GetDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public static void WriteColor(BinaryWriter writer, Color color)
        {
            writer.Write((byte)color.A);
            writer.Write((byte)color.R);
            writer.Write((byte)color.G);
            writer.Write((byte)color.B);
        }

        public static void WriteVertex(BinaryWriter writer, IVertex vertex)
        {
            writer.Write(vertex.X);
            writer.Write(vertex.Y);
        }

        public static void WritePoint(BinaryWriter writer, PointF point)
        {
            writer.Write(point.X);
            writer.Write(point.Y);
        }

        public static void WriteSize(BinaryWriter writer, SizeF size)
        {
            writer.Write(size.Width);
            writer.Write(size.Height);
        }

        public static Color ReadColor(BinaryReader reader)
        {
            byte a = reader.ReadByte();
            byte r = reader.ReadByte();
            byte g = reader.ReadByte();
            byte b = reader.ReadByte();
            return Color.FromArgb(a, r, g, b);
        }

        public static Vertex ReadVertex(BinaryReader reader)
        {
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();
            return new Vertex(x, y);
        }

        public static PointF ReadPoint(BinaryReader reader)
        {
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();
            return new PointF(x, y);
        }

        public static SizeF ReadSize(BinaryReader reader)
        {
            float w = reader.ReadSingle();
            float h = reader.ReadSingle();
            return new SizeF(w, h);
        }

        public static void SaveDrawingObject(BinaryWriter writer, IDrawingObject obj)
        {
            var type = (Int32)obj.GetObjectType();
            // write object type
            writer.Write(type);

            switch (obj.GetObjectType())
            {
                case DrawingObjectType.Shape:
                {
                    var shape = (ShapeBase) obj;
                    var shapeType = shape.GetShapeType();
                    // write shape type
                    writer.Write((Int32) shapeType);
                    // write shape location
                    WritePoint(writer, shape.Location);
                    // write shape size
                    WriteSize(writer, shape.Size);
                    // write shape outline color
                    WriteColor(writer, shape.OutlineColor);
                    // write shape outline width
                    writer.Write(shape.OutlineWidth);
                    // write shape outline dash
                    writer.Write((Int32) shape.OutlineDash);
                    // write shape fill color
                    WriteColor(writer, shape.FillColor);

                    if (shapeType != ShapeType.Ellipse)
                    {
                        // write count vertex
                        writer.Write((Int32)shape.Vertices.Count);
                        foreach (var vertex in shape.Vertices)
                            WriteVertex(writer, vertex);
                    }
                }
                    break;
            }
        }

        public static IDrawingObject ReadDrawingObject(BinaryReader reader)
        {
            // read object type
            IDrawingObject obj = null;
            int type = reader.ReadInt32();

            switch ((DrawingObjectType)type)
            {
                case DrawingObjectType.Shape:
                    {
                        // read shape type
                        int shapeType = reader.ReadInt32();
                        ShapeBase shape;

                        if ((ShapeType) shapeType == ShapeType.Ellipse)
                            shape = new Ellipse();
                        else
                            shape = new FreePencil();
                        //var shape = ShapeFactory.CreateShape((ShapeType) shapeType);
                        // write shape location
                        shape.Location = ReadPoint(reader);
                        // write shape size
                        shape.Size = ReadSize(reader);
                        // write shape outline color
                        shape.OutlineColor = ReadColor(reader);
                        // write shape outline width
                        shape.OutlineWidth = reader.ReadSingle();
                        // write shape outline dash
                        shape.OutlineDash = (DashStyle)reader.ReadInt32();
                        // write shape fill color
                        shape.FillColor = ReadColor(reader);

                        if (shape.GetShapeType() != ShapeType.Ellipse)
                        {
                            // write count vertex
                            var vcount = reader.ReadInt32();
                            for (int i = 0; i < vcount; i++)
                            {
                                Vertex v = ReadVertex(reader);
                                shape.Vertices.Add(v);
                            }
                        }

                        obj = shape;
                    }
                    break;
            }

            return obj;
        }

        // Kiem tra 1 diem co nam trong da giac khong
        public static bool CheckInnerPoint(IVertexCollection points, IVertex point)
        {
            IVertex currentPoint = point;
            //Ray-cast algorithm is here onward
            int k, j = points.Count - 1;
            var oddNodes = false; //to check whether number of intersections is odd

            for (k = 0; k < points.Count; k++)
            {
                //fetch adjucent points of the polygon
                IVertex polyK = points[k];
                IVertex polyJ = points[j];

                //check the intersections
                if (((polyK.Y > currentPoint.Y) != (polyJ.Y > currentPoint.Y)) &&
                 (currentPoint.X < (polyJ.X - polyK.X) * (currentPoint.Y - polyK.Y) / (polyJ.Y - polyK.Y) + polyK.X))
                    oddNodes = !oddNodes; //switch between odd and even
                j = k;
            }

            //if odd number of intersections
            if (oddNodes)
            {
                //mouse point is inside the polygon
                return true;
            }

            //if even number of intersections
            return false;
        }

        public static float CalculateShapeArea(ShapeBase shape)
        {
            float s = 0;

            if (shape.GetShapeType() == ShapeType.Ellipse)
            {
                var e = shape as Ellipse;
                s = (float) Math.PI*e.MinorAxis*e.MajorAxis;
            }
            else
            {
                var vetices = shape.Vertices.Clone();
                // Them p1 vao cuoi danh sach
                vetices.Add(vetices[0]);
                float x1 = vetices[0].X;
                float y1 = vetices[0].Y;
                for (int i = 1; i < vetices.Count; i++)
                {
                    float x2 = vetices[i].X;
                    float y2 = vetices[i].Y;
                    float dx = x2 - x1;
                    float dy = y2 - y1;
                    s += 0.5F*dx*(dy + 2*y1);

                    x1 = x2;
                    y1 = y2;
                }
            }

            return Math.Abs(s);
        }
    }
}
