using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseDetection : MonoBehaviour
    {
        // Enemy�̊�ꌟ�o�N���X

        protected virtual bool Ditection(GameObject diteObj, float deteRange)
        {
            float distance = diteObj.transform.position.x - transform.position.x;

            if (Mathf.Abs(distance) < deteRange) return true;
            return false;
        }
    }

}
