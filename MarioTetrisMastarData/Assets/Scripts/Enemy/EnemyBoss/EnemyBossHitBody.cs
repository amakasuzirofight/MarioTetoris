using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossHitBody : EnemyBaseHPManager, IDamageRecevable, IRecoveryReceivable
        {
            [SerializeField] private GameObject manager;

            private EnemyBossCore core;
            private EnemyBossStateManager stateManager;


            void Start()
            {
                core = manager.GetComponent<EnemyBossCore>();
                stateManager = manager.GetComponent<EnemyBossStateManager>();
            }


            // Enemy�_���[�W����
            public void DamageRecevable(int damage)
            {
                Debug.Log(core.Hp);
                core.Hp = Damage(core.Hp, damage);
                Debug.Log(core.Hp);

                // ���f���\�b�h�Ăяo��
                stateManager.BreakState_Damage();
            }

            // Enemy�񕜏���
            public void RecoveryReceivable(int recoveryAmount)
            {
                core.Hp = Recovery(core.Hp, recoveryAmount);
            }
        }
    }
}
