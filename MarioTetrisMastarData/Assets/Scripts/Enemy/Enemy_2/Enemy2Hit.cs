using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2Hit : EnemyBaseHPManager, IDamageRecevable
        {
            private IDamageRecevable damageRecevable;
            private Enemy2Core core;

            void Start()
            {
                damageRecevable = GetComponent<IDamageRecevable>();
                core = GetComponent<Enemy2Core>();
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


            // Enemyダメージ処理
            public void DamageRecevable(int damage)
            {
                Debug.Log("痛い！！");
                core.Hp = Damage(core.Hp, damage);

                // Playerの攻撃判定をボタンが押された瞬間にすればいける
            }
        }

    }

}
