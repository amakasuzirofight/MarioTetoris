using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseMove : MonoBehaviour
    {
        // Enemy‚ÌŠî’êˆÚ“®ƒNƒ‰ƒX

        protected virtual void Move(float spd, bool isTurn) 
        {
            Vector3 pos = transform.position;
            int dir = 1;

            if (isTurn) dir =  1;
            else        dir = -1;

            transform.position += new Vector3(pos.x * spd * Time.deltaTime, 0f, 0f) * dir;
        }
    }
}
