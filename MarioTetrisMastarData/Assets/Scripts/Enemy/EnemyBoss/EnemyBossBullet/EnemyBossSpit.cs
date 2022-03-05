using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossSpit : MonoBehaviour
        {
            [SerializeField] private GameObject faceObj;
            [SerializeField] private float spd;
            private GameObject player;
            private Rigidbody2D rb;

            private void Start()
            {
                player = Utility_.playerObject;
                rb = GetComponent<Rigidbody2D>();
                //transform.position = faceObj.transform.position;
                Shot();
            }

            // 発射メソッド
            private void Shot()
            {
                float dir = player.transform.position.x > 0f ? 1f : -1f; 
                rb.AddForce(Vector2.right * spd * dir);
                Delete();
            }

            // 消滅メソッド
            private void Delete() 
            {
                // 弾が消える
                Destroy(gameObject, 3f);
            }
        }

    }
}
