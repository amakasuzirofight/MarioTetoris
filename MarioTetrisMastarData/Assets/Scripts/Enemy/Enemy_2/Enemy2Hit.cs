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


            // Player�ɐG�ꂽ���Ƀ_���[�W��^����(�Ñ��҂�)
            private void OnCollisionEnter2D(Collision2D collision)
            {
                if (collision.gameObject.TryGetComponent(out MarioCore at))
                {
                    IRecoveryRecevable damage = at;

                    Debug.Log("�Ă߂��ǂ����Ă񂾂�");
                    damage.RecoveryRecevable(core.AtkPow);
                }
            }


            // Enemy�_���[�W����
            public void RecoveryRecevable(int damage)
            {
                Debug.Log("�ɂ��I�I");
                core.Hp = Damage(core.Hp, damage);

                // Player�̍U��������{�^���������ꂽ�u�Ԃɂ���΂�����
            }
        }

    }

}
