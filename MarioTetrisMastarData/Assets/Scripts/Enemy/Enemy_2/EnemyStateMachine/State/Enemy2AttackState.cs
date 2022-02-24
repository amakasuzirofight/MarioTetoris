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

            [SerializeField] private GameObject player;

            public Enemy2StateType StateType => Enemy2StateType.ATTACK;
            public event Action<Enemy2StateType> ChangeStateEvent;

            private Enemy2Core core;

            void IEnemy2State.OnStart(Enemy2StateType beforeState, Enemy2Core enemy)
            {
                core ??= GetComponent<Enemy2Core>();
            }

            void IEnemy2State.OnUpdate(Enemy2Core enemy)
            {
                Debug.Log(StateType);

                Debug.Log("攻撃！！");

                // この間に待ち時間を作る

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
                ChangeStateEvent(Enemy2StateType.STAY);
            }
        }

    }


}
