using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2FollowState : EnemyBaseMove, IEnemy2State
        {
            //EnemyのFollow状態処理

            public Enemy2StateType StateType => Enemy2StateType.FOLLOW;
            public event Action<Enemy2StateType> ChangeStateEvent;

            private GameObject  player;
            private Enemy2Core  core;
            private Rigidbody2D rb;
            private Animator animator;


            void IEnemy2State.OnStart(Enemy2StateType beforeState, Enemy2Core enemy)
            {
                player ??= Utility_.playerObject;
                core   ??= GetComponent<Enemy2Core>();
                rb     ??= GetComponent<Rigidbody2D>();
                animator ??= GetComponent<Animator>();

                animator.SetBool("Dash", true);
            }

            void IEnemy2State.OnUpdate(Enemy2Core enemy)
            {
                // 追従処理
                Follow(rb, gameObject, core.Spd,Distance(player, gameObject), Detection(Distance(player, gameObject), core.DiteRange));
                StateChangeManager();
            }

            void IEnemy2State.OnFixedUpdate(Enemy2Core enemy)
            {
            }

            void IEnemy2State.OnEnd(Enemy2StateType nextState, Enemy2Core enemy)
            {
            }


            // ステート変更メソッド
            private void StateChangeManager()
            {
                // オブジェクトが検知範囲外の場合
                if (!Detection(Distance(player, gameObject), core.DiteRange))
                {
                    animator.SetBool("Dash", false);
                    ChangeStateEvent(Enemy2StateType.STAY);
                }

                // 攻撃範囲内に入った場合
                if (Detection(Distance(player, gameObject), core.AtkRange))
                {
                    animator.SetBool("Dash", false);
                    ChangeStateEvent(Enemy2StateType.ATTACK);
                }
            }

            // 当たり判定
            private void OnTriggerEnter2D(Collider2D collision)
            {
                var player = collision.GetComponent<TestMarioAttack>();

                // Playerの攻撃に当たったら
                if (player != null)
                {
                    ChangeStateEvent(Enemy2StateType.DAMAGED);
                    StateChangeManager();
                }
            }
        }

    }


}
