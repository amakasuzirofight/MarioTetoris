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
            //Enemy‚ÌStartó‘Ôˆ—

            public Enemy3StateType StateType => Enemy3StateType.START;
            public event Action<Enemy3StateType> ChangeStateEvent;

            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
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
