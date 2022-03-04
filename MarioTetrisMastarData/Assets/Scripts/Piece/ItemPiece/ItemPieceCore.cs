using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RobotItem;
namespace ItemPiece
{
    class ItemPieceCore : MonoBehaviour, IItemPieceHit
    {
        IAddItemPiece addItemPiece;

     

        public void ItemHit(ItemName name, int num)
        {
            addItemPiece.AddItemPiece(name, num);
        }
        private void Awake()
        {
            Utility.Locator<IItemPieceHit>.Bind(this);
        }
        private void Start()
        {
            addItemPiece = Utility.Locator<IAddItemPiece>.GetT();
        }
    }
}
