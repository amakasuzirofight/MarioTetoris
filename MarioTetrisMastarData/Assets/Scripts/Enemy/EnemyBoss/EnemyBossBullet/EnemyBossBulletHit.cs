using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossBulletHit : EnemyBaseHPManager
        {
            [SerializeField] private GameObject manager;
            private EnemyBossCore core;

            void Start()
            {
                core = manager.GetComponent<EnemyBossCore>();
            }


            // Playerに触れた時にダメージを与える
            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.gameObject.TryGetComponent(out MarioCore at))
                {
                    IDamageRecevable damage = at;

                    damage.DamageRecevable(core.AtkPow_Spit);
                }
            }
        }
    }
}
