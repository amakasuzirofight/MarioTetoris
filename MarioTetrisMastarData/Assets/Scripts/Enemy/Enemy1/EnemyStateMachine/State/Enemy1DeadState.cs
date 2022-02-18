using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1DeadState : EnemyBaseMove, IEnemy1State
        {
            // Playerが実装するの！？
            // PlayerのStart状態処理

            public Enemy1StateType StateType => Enemy1StateType.DEAD;
            public event Action<Enemy1StateType> ChangeStateEvent;

            private Enemy1Core enemy1Core;

            void IEnemy1State.OnStart(Enemy1StateType beforeState, Enemy1Core enemy)
            {
                enemy1Core ??= GetComponent<Enemy1Core>();
            }

            void IEnemy1State.OnUpdate(Enemy1Core enemy)
            {
                Debug.Log(StateType);
            }

            void IEnemy1State.OnFixedUpdate(Enemy1Core enemy)
            {
            }

            void IEnemy1State.OnEnd(Enemy1StateType nextState, Enemy1Core enemy)
            {
            }

            private void StateChangeManager()
            {

            }
        }

    }


}
