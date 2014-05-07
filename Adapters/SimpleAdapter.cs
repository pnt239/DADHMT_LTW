using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using TabletC.Core;

namespace TabletC.Adapters
{
    public class SimpleAdapter
    {
        public SimpleAdapter()
        {}

        public void SaveTablet(IPage page)
        {
            Stream writer = File.OpenWrite("data.txt");
            var formatter = new BinaryFormatter();

            //var t = (Quad)page.Layers[1].Shapes[0];
            formatter.Serialize(writer, page);
            writer.Close();
        }
    }
}
