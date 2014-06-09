using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Untipic.Core
{
    public abstract class UserInfoBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Point MouseLocation { get; set; }
    }
}
