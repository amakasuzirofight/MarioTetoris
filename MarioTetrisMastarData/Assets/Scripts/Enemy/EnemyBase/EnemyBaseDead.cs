using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseDead : MonoBehaviour
    {
        // Enemy�̊�ꎀ�S�N���X

        protected virtual void Dead() 
        {
            Destroy(gameObject);
        } 
    }
}
