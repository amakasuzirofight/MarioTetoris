using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1Hit : EnemyBaseHPManager,IDamageRecevable
        {
            // Enemy1の接触判定処理

            private Enemy1Core core;

            void Start()
            {
                core = GetComponent<Enemy1Core>();
            }


            // Enemyダメージ処理
            public void DamageRecevable(int damage)
            {
                core.Hp = Damage(core.Hp, damage);
            }


            // Playerに触れた時にダメージを与える(甘糟待ち)
            private void OnCollisionEnter2D(Collision2D collision)
            {
                //if (collision.gameObject.TryGetComponent(out MarioCore at))
                //{
                //    IDamageRecevable damage = at;

                //    damage.DamageRecevable(core.AtkPow);
                //}
            }
        }
    }
}
