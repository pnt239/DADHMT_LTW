using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using TabletC.Core;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RectangleF rec = new RectangleF(12.2F, 4.2F, 3.4F, 6F);
            Rectangle rec_ = (Rectangle)rec;
        }
    }
}
