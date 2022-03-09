using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossStanState : MonoBehaviour, IEnemyBossState
        {
            // Enemyの爆弾攻撃状態処理

            public EnemyBossStateType StateType => EnemyBossStateType.STAN;
            public event Action<EnemyBossStateType> ChangeStateEvent;

            private EnemyBossCore core;
            private float transTimeCount = 16;


            void IEnemyBossState.OnStart(EnemyBossStateType beforeState, EnemyBossCore enemy)
            {
                core ??= GetComponent<EnemyBossCore>();
                // ぐでーんってなってるAnimation
            }

            void IEnemyBossState.OnUpdate(EnemyBossCore enemy)
            {
                Debug.Log(StateType);
                StateChangeManager();
            }

            void IEnemyBossState.OnFixedUpdate(EnemyBossCore enemy)
            {
            }

            void IEnemyBossState.OnEnd(EnemyBossStateType nextState, EnemyBossCore enemy)
            {
            }

            // ステート変更メソッド
            private void StateChangeManager()
            {
                if (!core.WaitTime(transTimeCount)) return;
                // 元の状態に戻すAnimation
                ChangeStateEvent(EnemyBossStateType.IDLE);
            }
        }
    }
}
