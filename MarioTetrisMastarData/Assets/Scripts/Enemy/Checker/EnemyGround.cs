using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtility
{
    public class EnemyGround : MonoBehaviour
    {
#pragma warning disable 649

        // 地面判定処理

        [SerializeField] LayerMask layer;         // 地面のレイヤー 


        /// <summary>
        /// 着地判定メソッド(レイの処理)
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool CheckIsGround()
        {
            Vector3 offsetPos = new Vector3(0f, 1.5f, 0f);

            Vector3 startVec = transform.position + transform.right * -1f * transform.localScale.x - offsetPos;
            Vector3 endVec = startVec - transform.up * 0.2f;
            Debug.DrawLine(startVec, endVec);
            return Physics2D.Linecast(startVec, endVec, layer);
        }
    }


}
