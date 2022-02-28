using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario
{
    interface IAddAtems
    {
        public event Action<ItemName,int> GetItemEvent;
    }
}
