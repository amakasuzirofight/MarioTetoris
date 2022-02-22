using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyUtility
{
    public class WallChecker : MonoBehaviour
    {
#pragma warning disable 649

        // 壁判定処理

        [SerializeField, Tooltip("レイの長さ")]     　　private float raylength = 1f;
        [SerializeField, Tooltip("地面(壁)のレイヤー")] private LayerMask groundLayer;


        // 着地判定メソッド(レイの処理)
        public bool CheckIsGround(CapsuleCollider2D col)
        {
            bool hit;                                               // 当たった時の判定変数
            float rayInitialPos  = col.size.y;                      // RayのY座標初期位置
            float colHalfHeight2 = col.size.y / 3.2f;               // RayのY座標間隔
            Vector3 lineLength = transform.right * raylength;       // レイを飛ばす方向と長さ
            Vector3 checkPos = transform.position;                  // プレイヤーの座標

            const int MAX_LOOP = 3;                                 // ループの回数（レイの本数）


            // checkPosの初期位置
            checkPos.y += rayInitialPos;


            // レイを飛ばす
            hit = true;
            for (int loop = 0; loop < MAX_LOOP; ++loop)
            {
                Debug.DrawLine(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, Color.red);// デバッグでレイを表示
                hit &= Physics2D.Linecast(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, groundLayer);
                checkPos.y -= colHalfHeight2;// 座標を--していく
            }

            return hit;
        }

    }
}
