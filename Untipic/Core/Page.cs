using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Untipic.Core
{
    public class Page
    {
        public MessureUnit Unit { get; set; }

        public Page(float width, float height, MessureUnit unit)
        {
            _size = new SizeF(width, height);
            Unit = unit;
            IsRender = false;

            _drawingObjects = new List<IDrawingObject>();
        }

        public event EventArguments.AddedObjectEventHandler AddedShape = null;

        public SizeF Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public bool IsRender { get; set; }

        public List<IDrawingObject> DrawingObjects
        {
            get { return _drawingObjects; }
        }

        public Image ImageBuffer
        {
            get { return _imageBuffer; }
            set { _imageBuffer = value; }
        }

        public void AddDrawingObject(IDrawingObject obj)
        {
            DrawingObjects.Add(obj);
            OnAddedShape(new EventArguments.AddedObjectEventArgs(obj));
        }

        private void OnAddedShape(EventArguments.AddedObjectEventArgs e)
        {
            if (AddedShape != null)
                AddedShape(this, e);
        }

        private readonly List<IDrawingObject> _drawingObjects;
        private SizeF _size;
        private Image _imageBuffer;
    }
}
