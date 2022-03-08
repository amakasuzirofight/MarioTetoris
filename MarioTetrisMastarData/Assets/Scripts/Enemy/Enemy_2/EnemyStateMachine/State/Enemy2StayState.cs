using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2StayState : EnemyBaseMove, IEnemy2State
        {
            //EnemyのStay状態処理

            public Enemy2StateType StateType => Enemy2StateType.STAY;
            public event Action<Enemy2StateType> ChangeStateEvent;

            private GameObject player;
            private Enemy2Core core;


            void IEnemy2State.OnStart(Enemy2StateType beforeState, Enemy2Core enemy)
            {
                player ??= Utility_.playerObject;
                core   ??= GetComponent<Enemy2Core>();
            }

            void IEnemy2State.OnUpdate(Enemy2Core enemy)
            {
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
                // オブジェクトが検知範囲に入った場合
                if(Detection(Distance(player,gameObject), core.DiteRange)) 
                {
                    ChangeStateEvent(Enemy2StateType.FOLLOW);
                }
            }

            // 当たり判定
            private void OnTriggerEnter2D(Collider2D collision)
            {
                var player = collision.GetComponent<TestMarioAttack>();

                // Playerの本体に当たったら
                if (player != null)
                {
                    ChangeStateEvent(Enemy2StateType.DAMAGED);
                }
            }
        }

    }


}
