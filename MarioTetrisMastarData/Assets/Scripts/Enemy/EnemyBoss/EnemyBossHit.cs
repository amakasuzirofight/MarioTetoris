using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossHit : EnemyBaseHPManager, IDamageRecevable
        {
            [SerializeField] private GameObject manager;

            private EnemyBossCore core;
            private EnemyBossStateManager stateManager;

            void Start()
            {
                core = manager.GetComponent<EnemyBossCore>();
                stateManager = manager.GetComponent<EnemyBossStateManager>();
            }


            // Enemyダメージ処理
            public void DamageRecevable(int damage)
            {
                core.Hp = Damage(core.Hp, damage);

                // 中断メソッド呼び出し
                stateManager.BreakState();
            }

            // Playerに触れた時にダメージを与える
            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.gameObject.TryGetComponent(out MarioCore at))
                {
                    IDamageRecevable damage = at;

                    damage.DamageRecevable(core.AtkPow_Hand);
                }
            }
        }
    }
}
