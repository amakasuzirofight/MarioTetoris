using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2Hit : EnemyBaseHPManager, IDamageRecevable,IRecoveryReceivable
        {
            // Enemy2の接触判定処理

            private Enemy2Core core;


            void Start()
            {
                core = GetComponent<Enemy2Core>();
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


            // Enemyダメージ処理
            public void DamageRecevable(int damage)
            {
                core.Hp = Damage(core.Hp, damage);
            }

            // Enemy回復処理
            public void RecoveryReceivable(int recoveryAmount)
            {
                core.Hp = Recovery(core.Hp, recoveryAmount);
            }
        }

    }

}
