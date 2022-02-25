using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3AttackState : EnemyBaseMove, IEnemy3State
        {
            //EnemyのDamage状態処理

            public Enemy3StateType StateType => Enemy3StateType.ATTACK;
            public event Action<Enemy3StateType> ChangeStateEvent;

            private Enemy3Core core;

            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
                core ??= GetComponent<Enemy3Core>();
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
                Debug.Log(StateType);
                Debug.Log("攻撃！！");

                // この間に待ち時間を作る

                StateChangeManager();
            }

            void IEnemy3State.OnFixedUpdate(Enemy3Core enemy)
            {
            }

            void IEnemy3State.OnEnd(Enemy3StateType nextState, Enemy3Core enemy)
            {
            }

            // ステート変更メソッド
            private void StateChangeManager()
            {
                // 移動状態に遷移
                ChangeStateEvent(Enemy3StateType.STAY);
            }

        }
    }
}
