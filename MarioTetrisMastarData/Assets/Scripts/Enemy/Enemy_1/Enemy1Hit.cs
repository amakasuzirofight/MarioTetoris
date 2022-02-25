using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1Hit : EnemyBaseHPManager, IDamageRecevable
        {
            private IDamageRecevable damageRecevable;
            private Enemy1Core core;

            void Start()
            {
                damageRecevable = GetComponent<IDamageRecevable>();
                core = GetComponent<Enemy1Core>();
            }


            // Enemy�_���[�W����
            public void DamageRecevable(int damage)
            {
                core.Hp = Damage(core.Hp, damage);
            }


            // Player�ɐG�ꂽ���Ƀ_���[�W��^����(�Ñ��҂�)
            //private void OnCollisionEnter2D(Collision2D collision)
            //{
            //    Transform parent = collision.gameObject.transform;

            //    for (int i = 0; i < parent.childCount; i++)
            //    {
            //        if (parent.GetChild(i).TryGetComponent(out TestMarioAttack at))
            //        {
            //            IDamageRecevable damage = (IDamageRecevable)at;

            //            Debug.Log("�Ă߂��ǂ����Ă񂾂�");
            //            damage.DamageRecevable(core.AtkPow);
            //        }
            //    }
            //}

        }

    }

}
