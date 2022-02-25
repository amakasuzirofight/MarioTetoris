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

            [SerializeField] private GameObject player;

            public Enemy2StateType StateType => Enemy2StateType.FOLLOW;
            public event Action<Enemy2StateType> ChangeStateEvent;

            private Enemy2Core core;
            private Rigidbody2D rb;

            void IEnemy2State.OnStart(Enemy2StateType beforeState, Enemy2Core enemy)
            {
                core ??= GetComponent<Enemy2Core>();
                rb   ??= GetComponent<Rigidbody2D>();
            }

            void IEnemy2State.OnUpdate(Enemy2Core enemy)
            {
                Debug.Log(StateType);
                Follow(rb,core.Spd,Distance(player), Detection(Distance(player), core.DiteRange));

                StateChangeManager();
            }

            void IEnemy2State.OnFixedUpdate(Enemy2Core enemy)
            {
            }

            void IEnemy2State.OnEnd(Enemy2StateType nextState, Enemy2Core enemy)
            {
            }

            private void StateChangeManager()
            {
                // オブジェクトが検知範囲外の場合
                if (!Detection(Distance(player), core.DiteRange))
                {
                    ChangeStateEvent(Enemy2StateType.STAY);
                }

                // 攻撃範囲内に入った場合
                if (Detection(Distance(player), core.AtkRange))
                {
                    ChangeStateEvent(Enemy2StateType.ATTACK);
                }
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                ChangeStateEvent(Enemy2StateType.DAMAGED);
            }
        }

    }


}
