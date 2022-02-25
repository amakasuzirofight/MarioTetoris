using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2DamageState : EnemyBaseTouched, IEnemy2State
        {
            // EnemyのDamage状態処理

            [SerializeField] private GameObject player;

            public Enemy2StateType StateType => Enemy2StateType.DAMAGED;
            public event Action<Enemy2StateType> ChangeStateEvent;

            private Enemy2Core core;
            private Rigidbody2D rb;

            private float time = 0f;
            private const float TRANS_COUNT = 0.8f;

            void IEnemy2State.OnStart(Enemy2StateType beforeState, Enemy2Core enemy)
            {
                core ??= GetComponent<Enemy2Core>();
                rb ??= GetComponent<Rigidbody2D>();

                KnockBack(player, rb, 5f);
            }

            void IEnemy2State.OnUpdate(Enemy2Core enemy)
            {
                Debug.Log(StateType);
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
                if (!WaitTime(TRANS_COUNT)) return;

                // HPが0の場合
                if (core.Hp <= 0)
                {
                    // 死亡状態に遷移
                    ChangeStateEvent(Enemy2StateType.DEAD);
                }
                else
                {
                    // 移動状態に遷移
                    ChangeStateEvent(Enemy2StateType.STAY);
                }
            }

            // 待ち時間メソッド
            private bool WaitTime(float count)
            {
                time += Time.deltaTime;

                if (time > count)
                {
                    time = 0f;
                    return true;
                }
                return false;
            }

        }

    }


}
