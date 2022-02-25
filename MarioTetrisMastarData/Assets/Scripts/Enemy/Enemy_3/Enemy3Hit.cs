using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3Hit : EnemyBaseHPManager, IDamageRecevable
        {
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

            // Playerに触れた時にダメージを与える(甘糟待ち)
            //private void OnCollisionEnter2D(Collision2D collision)
            //{
            //    Transform parent = collision.gameObject.transform;

            //    for (int i = 0; i < parent.childCount; i++)
            //    {
            //        if (parent.GetChild(i).TryGetComponent(out TestMarioAttack at))
            //        {
            //            IDamageRecevable damage = (IDamageRecevable)at;

            //            Debug.Log("てめえどこ見てんだよ");
            //            damage.DamageRecevable(core.AtkPow);
            //        }
            //    }
            //}

        }

    }

}
