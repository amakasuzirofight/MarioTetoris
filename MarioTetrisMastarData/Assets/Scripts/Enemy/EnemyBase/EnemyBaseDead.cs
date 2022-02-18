using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseDead : MonoBehaviour
    {
        // Enemy‚ÌŠî’êŽ€–SƒNƒ‰ƒX

        protected virtual void Dead() 
        {
            Destroy(gameObject);
        } 
    }
}
