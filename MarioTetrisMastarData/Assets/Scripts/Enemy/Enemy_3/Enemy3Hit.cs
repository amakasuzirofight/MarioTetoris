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


            // Enemy�_���[�W����
            public void DamageRecevable(int damage)
            {
                core.Hp = Damage(core.Hp, damage);
            }

            // Player�ɐG�ꂽ���Ƀ_���[�W��^����(�Ñ��҂�)
            private void OnCollisionEnter2D(Collision2D collision)
            {
                if (collision.gameObject.TryGetComponent(out MarioCore at))
                {
                    IDamageRecevable damage = at;

                    Debug.Log("�Ă߂��ǂ����Ă񂾂�");
                    damage.DamageRecevable(core.AtkPow);
                }
            }
        }
    }
}
