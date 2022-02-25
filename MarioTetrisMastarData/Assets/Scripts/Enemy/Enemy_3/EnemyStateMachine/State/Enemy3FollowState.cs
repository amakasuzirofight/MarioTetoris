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

            [SerializeField] private GameObject player;

            public Enemy3StateType StateType => Enemy3StateType.FOLLOW;
            public event Action<Enemy3StateType> ChangeStateEvent;

            private Enemy3Core core;
            private Rigidbody2D rb;

            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
                core ??= GetComponent<Enemy3Core>();
                rb   ??= GetComponent<Rigidbody2D>();
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
                Debug.Log(StateType);

                // 追従処理
                Follow(rb, core.Spd, Distance(player), Detection(Distance(player), core.DiteRange));
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
                if (!Detection(Distance(player), core.DiteRange))
                {
                    ChangeStateEvent(Enemy3StateType.STAY);
                }

                // 攻撃範囲内の場合
                if (Detection(Distance(player), core.AtkRange))
                {
                    ChangeStateEvent(Enemy3StateType.ATTACK);
                }
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                var player = collision.GetComponent<TestMarioAttack>();

                // Playerの本体に当たったら
                if (player != null)
                {
                    ChangeStateEvent(Enemy3StateType.DAMAGED);
                    StateChangeManager();
                }
            }
        }
    }
}
