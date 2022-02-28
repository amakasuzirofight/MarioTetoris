using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2Hit : EnemyBaseHPManager, IRecoveryRecevable
        {
            private IRecoveryRecevable damageRecevable;
            private Enemy2Core core;

            void Start()
            {
                damageRecevable = GetComponent<IRecoveryRecevable>();
                core = GetComponent<Enemy2Core>();
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


            // Enemyダメージ処理
            public void RecoveryRecevable(int damage)
            {
                Debug.Log("痛い！！");
                core.Hp = Damage(core.Hp, damage);

                // Playerの攻撃判定をボタンが押された瞬間にすればいける
            }
        }

    }

}
