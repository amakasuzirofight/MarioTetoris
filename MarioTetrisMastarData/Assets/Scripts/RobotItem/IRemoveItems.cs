using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotItem
{
    interface IRemoveItems
    {
        public void RemoveItems(ItemName name, int num);
        public void RemoveTetrisPiece(int num);
    }
}
