using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossHit : EnemyBaseHPManager, IDamageRecevable
        {
            private EnemyBossCore core;
            private EnemyBossStateManager stateManager;

            void Start()
            {
                core = GetComponent<EnemyBossCore>();
                stateManager = GetComponent<EnemyBossStateManager>();
            }


            // Enemy�_���[�W����
            public void DamageRecevable(int damage)
            {
                core.Hp = Damage(core.Hp, damage);

                // �����Œ��f���\�b�h���Ăяo��
                stateManager.BreakState();
            }

            // Player�ɐG�ꂽ���Ƀ_���[�W��^����
            private void OnCollisionEnter2D(Collision2D collision)
            {
                if (collision.gameObject.TryGetComponent(out MarioCore at))
                {
                    IDamageRecevable damage = at;

                    damage.DamageRecevable(core.AtkPow);
                }
            }
        }
    }
}
