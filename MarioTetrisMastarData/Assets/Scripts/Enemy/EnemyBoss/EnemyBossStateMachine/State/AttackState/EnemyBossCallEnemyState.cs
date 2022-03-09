using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossCallEnemyState : MonoBehaviour, IEnemyBossState
        {
            //Enemyの敵呼び出し状態処理

            public EnemyBossStateType StateType => EnemyBossStateType.CALLENEMY;
            public event Action<EnemyBossStateType> ChangeStateEvent;

            private EnemyBossCore core;
            private EnemySpawnManager spawnManager;
            private float transTimeCount = 7f;

            void IEnemyBossState.OnStart(EnemyBossStateType beforeState, EnemyBossCore enemy)
            {
                core ??= GetComponent<EnemyBossCore>();
                spawnManager ??= GetComponent<EnemySpawnManager>();
            }

            void IEnemyBossState.OnUpdate(EnemyBossCore enemy)
            {
                Debug.Log(StateType);

                spawnManager.EnemySpawn();

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
                ChangeStateEvent(EnemyBossStateType.IDLE);
            }

        }
    }
}
