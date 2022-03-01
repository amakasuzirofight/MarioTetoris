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
            // Enemy1�̐ڐG���菈��

            private Enemy1Core core;

            void Start()
            {
                core = GetComponent<Enemy1Core>();
            }


            // Enemy�_���[�W����
            public void DamageRecevable(int damage)
            {
                core.Hp = Damage(core.Hp, damage);
            }


            // Player�ɐG�ꂽ���Ƀ_���[�W��^����(�Ñ��҂�)
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
