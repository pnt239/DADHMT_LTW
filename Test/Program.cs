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

            ViewPort vp = new ViewPort(MessureUnit.Milimeters, 210, 297);
            Console.WriteLine(vp.Width.ToString());
            Console.WriteLine(vp.Height.ToString());
        }
    }
}
