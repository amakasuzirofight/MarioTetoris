using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1DeadState : EnemyBaseDead, IEnemy1State
        {
            // PlayerのDead状態処理

            public Enemy1StateType StateType =>  Enemy1StateType.DEAD;
            public event Action<Enemy1StateType> ChangeStateEvent;


            void IEnemy1State.OnStart(Enemy1StateType beforeState, Enemy1Core enemy)
            {
                // 死亡処理
                Dead();
            }

            void IEnemy1State.OnUpdate(Enemy1Core enemy)
            {
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
