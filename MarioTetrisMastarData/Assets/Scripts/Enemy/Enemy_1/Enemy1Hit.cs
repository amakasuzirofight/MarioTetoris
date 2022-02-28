using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1Hit : EnemyBaseHPManager, IRecoveryRecevable
        {
            private IRecoveryRecevable damageRecevable;
            private Enemy1Core core;

            void Start()
            {
                damageRecevable = GetComponent<IRecoveryRecevable>();
                core = GetComponent<Enemy1Core>();
            }


            // Enemyダメージ処理
            public void RecoveryRecevable(int damage)
            {
                core.Hp = Damage(core.Hp, damage);
            }


            // Playerに触れた時にダメージを与える(甘糟待ち)
            private void OnCollisionEnter2D(Collision2D collision)
            {
                if (collision.gameObject.TryGetComponent(out MarioCore at))
                {
                    IRecoveryRecevable damage = at;

                    Debug.Log("てめえどこ見てんだよ");
                    damage.RecoveryRecevable(core.AtkPow);
                }
            }
        }
    }
}
