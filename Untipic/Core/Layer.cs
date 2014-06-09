using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Untipic.Core
{
    public class Layer
    {
        public Layer()
        {
            _drawingObjects = new List<IDrawingObject>();
        }

        public void AddDrawingObject(IDrawingObject item)
        {
            _drawingObjects.Add(item);
        }

        private List<IDrawingObject> _drawingObjects;
    }
}
