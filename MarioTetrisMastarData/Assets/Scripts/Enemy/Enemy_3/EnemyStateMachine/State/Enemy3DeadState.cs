using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3DeadState : EnemyBaseDead, IEnemy3State
        {
            //Enemy‚ÌDeadó‘Ôˆ—

            public Enemy3StateType StateType => Enemy3StateType.DEAD;
            public event Action<Enemy3StateType> ChangeStateEvent;

            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
                Debug.Log(StateType);
                Dead();
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
