using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2AttackState : EnemyBaseMove, IEnemy2State
        {
            // EnemyのAttack状態処理

            public Enemy2StateType StateType => Enemy2StateType.ATTACK;
            public event Action<Enemy2StateType> ChangeStateEvent;

            private GameObject player;
            private Enemy2Core core;

            private float time = 0f;
            private float attackTimeCount = 5f;
            private float transTimeCount  = 3f;


            void IEnemy2State.OnStart(Enemy2StateType beforeState, Enemy2Core enemy)
            {
                player ??= Utility_.playerObject;
                core   ??= GetComponent<Enemy2Core>();
            }

            void IEnemy2State.OnUpdate(Enemy2Core enemy)
            {
                // この間に待ち時間を作る
                if (WaitTime(attackTimeCount))
                { 
                    // Animationを再生
                    Debug.Log("攻撃！！");
                }

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
                // 検知範囲外の場合
                if (!Detection(Distance(player, gameObject), core.DiteRange))
                {
                    ChangeStateEvent(Enemy2StateType.STAY);
                }

                // 攻撃範囲外の場合
                if (!Detection(Distance(player, gameObject), core.AtkRange))
                {
                    if (!WaitTime(transTimeCount)) return;

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

            // 時間待機メソッド
            private bool WaitTime(float count)
            {
                time += Time.deltaTime;

                if (time >= count)
                {
                    time = 0;
                    return true;
                }
                return false;
            }
        }

    }


}
