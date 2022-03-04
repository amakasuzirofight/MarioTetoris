using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Enemy;
using Items;
using RobotItem;
using UniRx;
namespace Mario
{
    class MarioCollision : MonoBehaviour,IAddAtems,IAddTetrisPiece
    {
        public event Action<ItemName,int> GetItemEvent;
        public event Action<int> GetTetPieceEvent;

        private void Awake()
        {
            Utility.Locator<IAddAtems>.Bind(this);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<IItemHit>()!=null)
            {
                var temp = collision.GetComponent<IItemHit>();
                GetItemEvent(temp.GetItemName(),1);
            }
        }
    }
    
}
