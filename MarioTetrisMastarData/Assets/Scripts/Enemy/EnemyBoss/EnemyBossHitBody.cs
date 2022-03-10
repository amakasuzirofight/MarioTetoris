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


            // Enemyダメージ処理
            public void DamageRecevable(int damage)
            {
                Debug.Log(core.Hp);
                core.Hp = Damage(core.Hp, damage);
                Debug.Log(core.Hp);

                // 中断メソッド呼び出し
                stateManager.BreakState_Damage();
            }

            // Enemy回復処理
            public void RecoveryReceivable(int recoveryAmount)
            {
                core.Hp = Recovery(core.Hp, recoveryAmount);
            }
        }
    }
}
