﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Select
{
    interface ISelectedItem
    {
        public ItemName ISelectedItem();
    }
}
public enum ItemName
{
    Stone=0,
    Portion=1,
    SpeedDown=2,
    Count=3
}
