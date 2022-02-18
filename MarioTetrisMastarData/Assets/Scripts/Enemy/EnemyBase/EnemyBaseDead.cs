using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseDead : MonoBehaviour
    {
        // Enemyの基底死亡クラス

        protected virtual void Dead() 
        {
            Destroy(gameObject);
        } 
    }
}
