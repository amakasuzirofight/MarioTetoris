using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossShotAttackState : MonoBehaviour, IEnemyBossState
        {
            //Enemyの敵呼び出し状態処理

            [SerializeField] private GameObject bullet;
            [SerializeField] private GameObject faceObj;

            public EnemyBossStateType StateType => EnemyBossStateType.SHOTATTACK;
            public event Action<EnemyBossStateType> ChangeStateEvent;

            private EnemyBossCore core;
            private GameObject player;
            private float transTimeCount = 3f;

            void IEnemyBossState.OnStart(EnemyBossStateType beforeState, EnemyBossCore enemy)
            {
                player ??= Utility_.playerObject;
                core ??= GetComponent<EnemyBossCore>();

                ShotStatePos();
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
                ChangeStateEvent(EnemyBossStateType.IDLE);
            }

            // 生成メソッド(アニメーションで呼び出す)
            public void Genarator()
            {
                Vector3 facePos = faceObj.transform.position;
                int offset = 0;

                for (int i = 0; i < 3; ++i)
                {
                    Instantiate(bullet).transform.position = new Vector3(facePos.x, facePos.y + offset, facePos.z);
                    offset++;
                }
            }

            // Bossの顔移動メソッド
            private void ShotStatePos()
            {
                // Playerのいる位置を見て右端か左端に顔が移動する
                float dir = player.transform.position.x;

                // Aniamtionを呼び出す
                if (dir < 0) Debug.Log("左側に移動");
                if(dir >= 0) Debug.Log("右側に移動");
            }
        }
    }
}
