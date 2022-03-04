using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotItem
{
    interface IItemDataChange
    {
        public event Action<Dictionary<ItemName, int>> ChangeItemBoxValue;
        public event Action<int> ChangeTetPieceValue;
    }
}
