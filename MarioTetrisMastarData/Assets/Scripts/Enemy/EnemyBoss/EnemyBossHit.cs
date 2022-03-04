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
            private EnemyBossCore core;
            private EnemyBossStateManager stateManager;

            void Start()
            {
                core = GetComponent<EnemyBossCore>();
                stateManager = GetComponent<EnemyBossStateManager>();
            }


            // Enemyダメージ処理
            public void DamageRecevable(int damage)
            {
                core.Hp = Damage(core.Hp, damage);

                // ここで中断メソッドを呼び出す
                stateManager.BreakState();
            }

            // Playerに触れた時にダメージを与える
            private void OnCollisionEnter2D(Collision2D collision)
            {
                if (collision.gameObject.TryGetComponent(out MarioCore at))
                {
                    IDamageRecevable damage = at;

                    damage.DamageRecevable(core.AtkPow);
                }
            }
        }
    }
}
