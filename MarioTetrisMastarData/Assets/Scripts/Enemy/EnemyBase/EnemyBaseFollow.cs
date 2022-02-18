using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseFollow : MonoBehaviour
    {
        // Enemy‚ÌŠî’ê’Ç]ƒNƒ‰ƒX

        protected virtual void Follow(GameObject deteObj,float spd) 
        {
            Vector3 pos = transform.position;
            float distance = deteObj.transform.position.x - pos.x;
            int dir = 1;

            if (distance < 0) dir =  1;
            else              dir = -1;

            transform.position += new Vector3(pos.x * spd * Time.deltaTime, 0f, 0f) * dir;
        }
    }

}
