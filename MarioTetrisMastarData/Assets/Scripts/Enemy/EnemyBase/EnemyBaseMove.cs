using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseMove : MonoBehaviour
    {
        // Enemyの基底移動関係クラス
        protected int dir = -1;

        // 移動メソッド
        protected virtual void Move(Rigidbody2D rb ,float spd) 
        {
            Vector3 scale = transform.localScale;

            rb.velocity = new Vector3(dir * spd, rb.velocity.y, 0f);
            transform.localScale = new Vector3(-dir , scale.y);
        }


        // 距離を返すメソッド
        protected virtual float Distance(GameObject obj)
        {
            float distance = obj.transform.position.x - transform.position.x;

            return distance;
        }


        // オブジェクト検出メソッド
        protected virtual bool Detection(float dis, float diteRange) 
        {
            if (Mathf.Abs(dis) < diteRange) return true;
            return false;
        }


        // 検出したオブジェクト追従メソッド
        protected virtual void Follow(Rigidbody2D rb, float spd, float dis, bool flg)
        {
            Vector3 scale = transform.localScale;

            if (!flg) return;

            if (dis < 0) dir = -1;
            else         dir =  1;

            rb.velocity = new Vector3(dir * spd, rb.velocity.y, 0f);
            transform.localScale = new Vector3(-dir , scale.y);
        }
    }
}
