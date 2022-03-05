using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class FootCheckCol : EnemyBaseMove
    {

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("asdfafaga");            
        }
    }

}
