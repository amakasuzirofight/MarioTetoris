using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossBullet : MonoBehaviour
        {
            // �e�̋�������

            [SerializeField] private float spd;
            private GameObject player;
            private Rigidbody2D rb;

            private void Start()
            {
                player = Utility_.playerObject;
                rb = GetComponent<Rigidbody2D>();
                Shot();
            }

            // ���˃��\�b�h
            private void Shot()
            {
                float dir = player.transform.position.x > 0f ? 1f : -1f; 
                rb.AddForce(Vector2.right * spd * dir);
                Delete();
            }

            // ���Ń��\�b�h
            private void Delete() 
            {
                // �e��������
                Destroy(gameObject, 5f);
            }
        }

    }
}
