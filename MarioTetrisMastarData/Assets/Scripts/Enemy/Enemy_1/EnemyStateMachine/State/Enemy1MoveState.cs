using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1MoveState : EnemyBaseMove, IEnemy1State
        {
            // PlayerのMove状態処理

            public Enemy1StateType StateType => Enemy1StateType.MOVE;
            public event Action<Enemy1StateType> ChangeStateEvent;

            private Enemy1Core  core;
            private EnemyGround enemyGround;
            private Rigidbody2D rb;
            private Animator animator;

            void IEnemy1State.OnStart(Enemy1StateType beforeState, Enemy1Core enemy)
            {
                core        ??= GetComponent<Enemy1Core>();
                enemyGround ??= GetComponent<EnemyGround>();
                rb          ??= GetComponent<Rigidbody2D>();
                animator    ??= GetComponent<Animator>();

                animator.SetBool("Dash", true);
            }

            void IEnemy1State.OnUpdate(Enemy1Core enemy)
            {
                if (!enemyGround.CheckIsGround())
                {
                    dir *= -1;
                }

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

            // 当たり判定
            private void OnTriggerEnter2D(Collider2D collision)
            {
                var player = collision.GetComponent<TestMarioAttack>();

                // Playerの攻撃に当たったら
                if (player != null)
                { 
                    StateChangeManager();
                }


                if (collision.gameObject.tag == "Ground" || collision.gameObject.TryGetComponent(out GetEnemy at))
                {
                    // Scaleを反転
                    dir *= -1;
                }
            }
        }
    }
}
