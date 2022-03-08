using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2DeadState : EnemyBaseDead, IEnemy2State
        {
            // Enemy‚ÌDeadó‘Ôˆ—

            public Enemy2StateType StateType => Enemy2StateType.DEAD;
            public event Action<Enemy2StateType> ChangeStateEvent;


            void IEnemy2State.OnStart(Enemy2StateType beforeState, Enemy2Core enemy)
            {
                // €–Sˆ—
                Dead();
            }

            void IEnemy2State.OnUpdate(Enemy2Core enemy)
            {
            }

            void IEnemy2State.OnFixedUpdate(Enemy2Core enemy)
            {
            }

            void IEnemy2State.OnEnd(Enemy2StateType nextState, Enemy2Core enemy)
            {
            }
        }
    }
}
