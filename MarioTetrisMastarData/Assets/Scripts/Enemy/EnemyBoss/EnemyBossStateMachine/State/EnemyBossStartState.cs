using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossStartState : MonoBehaviour, IEnemyBossState
        {
            //Enemy‚ÌStartó‘Ôˆ—

            public EnemyBossStateType StateType => EnemyBossStateType.START;
            public event Action<EnemyBossStateType> ChangeStateEvent;

            void IEnemyBossState.OnStart(EnemyBossStateType beforeState, EnemyBossCore enemy)
            { 
            }

            void IEnemyBossState.OnUpdate(EnemyBossCore enemy)
            {   
                Debug.Log(StateType);
                ChangeStateEvent(EnemyBossStateType.IDLE);
            }

            void IEnemyBossState.OnFixedUpdate(EnemyBossCore enemy)
            {
            }

            void IEnemyBossState.OnEnd(EnemyBossStateType nextState, EnemyBossCore enemy)
            {
            }
        }
    }
}
