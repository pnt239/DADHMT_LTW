using System.Collections.Generic;

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

        private readonly List<IDrawingObject> _drawingObjects;
    }
}
