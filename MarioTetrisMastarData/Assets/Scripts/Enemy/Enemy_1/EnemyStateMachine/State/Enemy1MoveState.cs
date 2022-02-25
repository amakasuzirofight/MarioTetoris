using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1MoveState : EnemyBaseMove, IEnemy1State
        {
            // PlayerのMove状態処理

            public Enemy1StateType StateType =>  Enemy1StateType.MOVE;
            public event Action<Enemy1StateType> ChangeStateEvent;

            private Enemy1Core  core;
            private Rigidbody2D rb;

            void IEnemy1State.OnStart(Enemy1StateType beforeState, Enemy1Core enemy)
            {
                core ??= GetComponent<Enemy1Core>();
                rb   ??= GetComponent<Rigidbody2D>();
            }

            void IEnemy1State.OnUpdate(Enemy1Core enemy)
            {
                Debug.Log(StateType);

                // 移動メソッド
                Move(rb, core.Spd);
            }

            void IEnemy1State.OnFixedUpdate(Enemy1Core enemy)
            {
            }

            void IEnemy1State.OnEnd(Enemy1StateType nextState, Enemy1Core enemy)
            {
            }

            // ステート変更メソッド
            private void StateChangeManager()
            {
                ChangeStateEvent(Enemy1StateType.DAMAGED);
            }

   
            private void OnTriggerEnter2D(Collider2D collision)
            {
                var player = collision.GetComponent<TestMarioAttack>();

                // Playerの本体に当たったら
                if (player != null)
                {
                    StateChangeManager();
                }

                // Scaleを反転
                dir *= -1;
            }
        }
    }
}
