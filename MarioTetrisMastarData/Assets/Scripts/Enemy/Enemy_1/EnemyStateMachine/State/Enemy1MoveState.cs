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

            private Enemy1Core enemy1Core;
            private Rigidbody2D rb;

            void IEnemy1State.OnStart(Enemy1StateType beforeState, Enemy1Core enemy)
            {
                enemy1Core ??= GetComponent<Enemy1Core>();
                rb         ??= GetComponent<Rigidbody2D>();
            }

            void IEnemy1State.OnUpdate(Enemy1Core enemy)
            {
                Debug.Log(StateType);
                Move(rb, enemy1Core.Spd);
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
                if(collision.name == "Player")
                {
                    // Playerの攻撃に当たったら
                    StateChangeManager();
                }
                else dir *= -1;
            }


        }
    }
}
