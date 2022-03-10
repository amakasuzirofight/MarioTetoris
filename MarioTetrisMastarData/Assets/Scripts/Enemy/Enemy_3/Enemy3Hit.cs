using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace Enemy3State
   {
        public class Enemy3Hit : EnemyBaseHPManager, IDamageRecevable,IRecoveryReceivable
        {
            // Enemy3の接触判定処理

            private IDamageRecevable damageRecevable;
            private Enemy3Core core;


            void Start()
            {
                damageRecevable = GetComponent<IDamageRecevable>();
                core = GetComponent<Enemy3Core>();
            }


            // Enemyダメージ処理
            public void DamageRecevable(int damage)
            {
                core.Hp = Damage(core.Hp, damage);
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

            // Enemy回復処理
            public void RecoveryReceivable(int recoveryAmount)
            {
                core.Hp = Recovery(core.Hp, recoveryAmount);
            }
        }
    }
}
