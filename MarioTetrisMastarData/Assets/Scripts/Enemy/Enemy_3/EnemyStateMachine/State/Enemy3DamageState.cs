using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3DamageState : EnemyBaseTouched, IEnemy3State
        {
            //EnemyのDamage状態処理

            public Enemy3StateType StateType => Enemy3StateType.DAMAGED;
            public event Action<Enemy3StateType> ChangeStateEvent;

            private Enemy3Core core;
            private Animator animator;


            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
                core ??= GetComponent<Enemy3Core>();
                animator ??= GetComponent<Animator>();

                animator.SetTrigger("Damage");
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
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
                // HPが0の場合
                if (core.Hp <= 0)
                {
                    // 死亡状態に遷移
                    ChangeStateEvent(Enemy3StateType.DEAD);
                }
                else
                {
                    // 移動状態に遷移
                    ChangeStateEvent(Enemy3StateType.STAY);
                }
            }

        }
    }
}
