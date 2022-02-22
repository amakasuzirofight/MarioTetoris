using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtility
{
    public class EnemyGround : MonoBehaviour
    {
#pragma warning disable 649

        // 地面判定処理

        // クラス変数
        private Rigidbody2D rb2d;

        // 地面判定用の変数
        [SerializeField] private float raylength = 1f;  // レイの長さ

        [SerializeField] LayerMask layer;         // 地面のレイヤー 


        // Start is called before the first frame update
        void Start()
        {
            // コンポーネントを取得
            rb2d = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        /// <summary>
        /// 着地判定メソッド(レイの処理)
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool CheckIsGround(CapsuleCollider2D col, int maxLoop, float rayLange)
        {
            const float JUMPUP_CHECK_SPEED = 1f;
            bool hit;                   // 当たった時の判定変数
            Vector3 checkPos = new Vector3(transform.position.x + (col.offset.x * transform.localScale.x), transform.position.y + (col.offset.y * transform.localScale.y), transform.position.z);          // プレイヤーの座標
            float colHalfWidth = ((col.size.x) * transform.localScale.x) / 2;         // プレイヤーの半分のコライダーの大きさ(X)
            float colHalfHigh = ((col.size.y) * transform.localScale.y) / 2;         // プレイヤーの半分のコライダーの大きさ(Y)
            Vector3 lineLength = transform.up * rayLange;  // レイの長さ(要調節)

            // 上昇中は何もしない
            if (rb2d.velocity.y > JUMPUP_CHECK_SPEED)
            {
                return false;
            }

            // checkPosの位置を左端に移動
            checkPos.x -= colHalfWidth;
            checkPos.y -= colHalfHigh;
            // レイを引く
            for (int loop = 0; loop < maxLoop; ++loop)
            {
                Debug.DrawLine(checkPos + transform.up * 0.1f, checkPos - lineLength, Color.red);// デバッグでレイを表示
                hit = Physics2D.Linecast(checkPos + transform.up * 0.1f, checkPos - lineLength, layer);
                if (hit) return true;
                checkPos.x += colHalfWidth;// 座標を＋１していく
            }
            return false;
        }
    }


}
