using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotItem
{
    interface IGetItemBox
    {
        public Dictionary<ItemName, int> GetItemBox();
        public int GetTetris();
    }
}
