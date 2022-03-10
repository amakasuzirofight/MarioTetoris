using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseDead : MonoBehaviour
    {
        // Enemy‚ÌŠî’ê€–SƒNƒ‰ƒX

        protected virtual void Dead() 
        {
            Debug.Log("‚µ‚ñ‚¾I");
            Destroy(gameObject);
        } 
    }
}
