using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseMove : MonoBehaviour
    {
        // Enemyの基底移動クラス

        protected virtual void Move(float spd) 
        {
            Vector3 pos = transform.position;

            if (pos.x > pos.x + 5)
            transform.position += new Vector3(pos.x * spd * Time.deltaTime, 0f, 0f); 
        }
    }
}
