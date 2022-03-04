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
            //EnemyのStart状態処理

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
