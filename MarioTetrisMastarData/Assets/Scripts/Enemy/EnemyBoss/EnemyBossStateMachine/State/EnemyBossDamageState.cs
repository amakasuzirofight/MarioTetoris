using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossDamageState : EnemyBaseDead, IEnemyBossState
        {
            //Enemy‚ÌStartó‘Ôˆ—

            public EnemyBossStateType StateType => EnemyBossStateType.DAMAGE;
            public event Action<EnemyBossStateType> ChangeStateEvent;

            private EnemyBossCore core;
            private float transTimeCount = 3f;

            void IEnemyBossState.OnStart(EnemyBossStateType beforeState, EnemyBossCore enemy)
            {
                core ??= GetComponent<EnemyBossCore>();
            }

            void IEnemyBossState.OnUpdate(EnemyBossCore enemy)
            {
                Debug.Log(StateType);
                ChangeStateManager();
            }

            void IEnemyBossState.OnFixedUpdate(EnemyBossCore enemy)
            {
            }

            void IEnemyBossState.OnEnd(EnemyBossStateType nextState, EnemyBossCore enemy)
            {
            }

            private void ChangeStateManager() 
            {
                if (core.Hp <= 0)
                {
                    Dead();
                }
                else 
                {
                    if (!core.WaitTime(transTimeCount)) return;
                    ChangeStateEvent(EnemyBossStateType.IDLE);
                }
            }
        }
    }
}
