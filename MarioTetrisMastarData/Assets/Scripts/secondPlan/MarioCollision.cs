using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Enemy;
using Items;
using RobotItem;
namespace Mario
{
    class MarioCollision : MonoBehaviour,IaddAtems

    {
        public event Action<ItemName> GetItemEvent;

        private void Awake()
        {
            Utility.Locator<IaddAtems>.Bind(this);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<IItemHit>()!=null)
            {
                var temp = collision.GetComponent<IItemHit>();
                GetItemEvent(temp.GetItemName());
            }
        }
    }
    
}
