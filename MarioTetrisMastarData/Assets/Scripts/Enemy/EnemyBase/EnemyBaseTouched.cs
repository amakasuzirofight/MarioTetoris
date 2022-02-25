using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseTouched : MonoBehaviour
    {
        // Enemy接触基底処理


        // のけ反りメソッド(ノックバック)
        protected virtual void KnockBack(GameObject player, Rigidbody2D rb, float power) 
        {
            Vector2 toPlayerDir = player.transform.position - transform.position;
            Vector2 dir = new Vector2(2f, 1f);
            dir.x = toPlayerDir.x < 0 ? 1 : -1;

            rb.AddForce(dir.normalized * power, ForceMode2D.Impulse);
        }
    }
}
