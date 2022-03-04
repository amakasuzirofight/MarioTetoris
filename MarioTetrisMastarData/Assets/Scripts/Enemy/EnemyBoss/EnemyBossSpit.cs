using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossSpit : MonoBehaviour
        {
            [SerializeField] private float spd;
            private GameObject player;
            private Rigidbody2D rb;

            private void Start()
            {
                player = Utility_.playerObject;


            }

            private void Shot()
            {
                float dir = player.transform.position.x > 0f ? 1f : -1f; 
                rb.AddForce(Vector2.right * spd * dir);
            }

            private void Delete() 
            {
                // íeÇ™è¡Ç¶ÇÈ
            }
        }

    }
}
