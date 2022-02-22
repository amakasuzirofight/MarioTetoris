using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseMove : MonoBehaviour
    {
        // Enemyの基底移動関係クラス


        // 移動メソッド
        protected virtual void Move(Rigidbody2D rb ,float spd, bool isTurn) 
        {
            int dir = -1;

            if (isTurn) dir *= -1;

            rb.velocity = new Vector3(dir * spd, rb.velocity.y, 0f);
        }


        // 特定のオブジェクトを検出するメソッド
        protected virtual bool Ditection(GameObject diteObj, float deteRange)
        {
            float distance = diteObj.transform.position.x - transform.position.x;

            if (Mathf.Abs(distance) < deteRange) return true;
            return false;
        }


        // 検出したオブジェクトを追従するメソッド
        protected virtual void Follow(GameObject deteObj, Rigidbody2D rb, float spd, bool flg)
        {
            Vector3 pos = transform.position;
            float distance = deteObj.transform.position.x - pos.x;
            int dir = -1;

            if (!flg) return;

            if (distance < 0) dir = -1;
            else dir = 1;

            rb.velocity = new Vector3(dir * spd, rb.velocity.y, 0f);
        }
    }
}
