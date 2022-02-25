using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseTouched : MonoBehaviour
    {
        // Enemy接触基底処理

        Vector2 dir = new Vector2(5f, 1f);


        // のけ反りメソッド(ノックバック)
        protected virtual void KnockBack(GameObject player, Rigidbody2D rb, float power) 
        {
            Vector2 toPlayerDir = player.transform.position - transform.position;
            dir.x = toPlayerDir.x < 0 ? 1 : -1;

            rb.velocity = Vector2.zero;
            rb.AddForce(dir.normalized * power, ForceMode2D.Impulse);
        }
    }
}
