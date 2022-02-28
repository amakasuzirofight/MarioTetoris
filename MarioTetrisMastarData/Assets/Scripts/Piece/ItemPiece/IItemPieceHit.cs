using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPiece
{
    interface IItemPieceHit
    {
        public void ItemHit(ItemName name, int num);
    }
}
