using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3StartState : MonoBehaviour, IEnemy3State
        {
            //EnemyのStart状態処理

            public Enemy3StateType StateType => Enemy3StateType.START;
            public event Action<Enemy3StateType> ChangeStateEvent;

            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
                Debug.Log(StateType);
                ChangeStateEvent(Enemy3StateType.STAY);
            }

            void IEnemy3State.OnFixedUpdate(Enemy3Core enemy)
            {
            }

            void IEnemy3State.OnEnd(Enemy3StateType nextState, Enemy3Core enemy)
            {
            }
        }
    }
}
