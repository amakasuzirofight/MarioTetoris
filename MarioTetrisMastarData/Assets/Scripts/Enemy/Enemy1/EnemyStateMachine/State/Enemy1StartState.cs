using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy1State
    {
        public class PlayerStartState : MonoBehaviour, IEnemy1State
        {
            // Player‚ªŽÀ‘•‚·‚é‚ÌIH
            // Player‚ÌStartó‘Ôˆ—

            public Enemy1StateType StateType => Enemy1StateType.START;
            public event Action<Enemy1StateType> ChangeStateEvent;

            private Enemy1Core enemy1Core;

            void IEnemy1State.OnStart(Enemy1StateType beforeState, Enemy1Core enemy)
            {
                enemy1Core ??= GetComponent<Enemy1Core>();
            }

            void IEnemy1State.OnUpdate(Enemy1Core enemy)
            {
                Debug.Log(StateType);
                // ChangeStateEvent(PlayerStateEnum.STAY);
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
