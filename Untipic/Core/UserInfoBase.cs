﻿using System.Drawing;

namespace Untipic.Core
{
    public abstract class UserInfoBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Point MouseLocation { get; set; }
    }
}
