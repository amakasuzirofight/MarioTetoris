using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3FollowState : EnemyBaseMove, IEnemy3State
        {
            //EnemyのFollow状態処理

            public Enemy3StateType StateType => Enemy3StateType.FOLLOW;
            public event Action<Enemy3StateType> ChangeStateEvent;

            private GameObject  player;
            private Enemy3Core  core;
            private Rigidbody2D rb;

            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
                player ??= Utility_.playerObject;
                core   ??= GetComponent<Enemy3Core>();
                rb     ??= GetComponent<Rigidbody2D>();
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
                // 追従処理
                Follow(rb, gameObject,core.Spd, Distance(player,gameObject), Detection(Distance(player,gameObject), core.DiteRange));
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
                // オブジェクトが検知範囲内の場合
                if (!Detection(Distance(player, gameObject), core.DiteRange))
                {
                    ChangeStateEvent(Enemy3StateType.STAY);
                }

                // 攻撃範囲内の場合
                if (Detection(Distance(player, gameObject), core.AtkRange))
                {
                    ChangeStateEvent(Enemy3StateType.ATTACK);
                }
            }

            // 当たり判定
            private void OnTriggerEnter2D(Collider2D collision)
            {
                var player = collision.GetComponent<TestMarioAttack>();

                // Playerの攻撃に当たったら
                if (player != null)
                {
                    ChangeStateEvent(Enemy3StateType.DAMAGED);
                    StateChangeManager();
                }
            }
        }
    }
}
