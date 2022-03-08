using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1StartState : MonoBehaviour, IEnemy1State
        {
            // Player‚ÌStartó‘Ôˆ—

            public Enemy1StateType StateType => Enemy1StateType.START;
            public event Action<Enemy1StateType> ChangeStateEvent;


            void IEnemy1State.OnStart(Enemy1StateType beforeState, Enemy1Core enemy)
            {
            }

            void IEnemy1State.OnUpdate(Enemy1Core enemy)
            {
                ChangeStateEvent(Enemy1StateType.MOVE);
            }

            void IEnemy1State.OnFixedUpdate(Enemy1Core enemy)
            {
            }

            void IEnemy1State.OnEnd(Enemy1StateType nextState, Enemy1Core enemy)
            {
            }
        }
    }
}
