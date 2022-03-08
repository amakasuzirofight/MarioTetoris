using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2StartState : MonoBehaviour, IEnemy2State
        {
            //Enemy‚ÌStartó‘Ôˆ—

            public Enemy2StateType StateType => Enemy2StateType.START;
            public event Action<Enemy2StateType> ChangeStateEvent;


            void IEnemy2State.OnStart(Enemy2StateType beforeState, Enemy2Core enemy)
            {
            }

            void IEnemy2State.OnUpdate(Enemy2Core enemy)
            {
                ChangeStateEvent(Enemy2StateType.STAY);
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
