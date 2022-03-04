using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossRightHandAttackState : EnemyBaseMove, IEnemyBossState
        {
            //Enemyの叩きつけ攻撃状態処理

            [SerializeField] private GameObject rightHand;
            [SerializeField] private GameObject player;

            public EnemyBossStateType StateType => EnemyBossStateType.HANDATTACK_RIGHT;
            public event Action<EnemyBossStateType> ChangeStateEvent;

            private EnemyBossCore core;
            private Rigidbody2D rb;
            private float transTimeCount = 5f;
            private float atkWaitCount = 3f;
            private float time;

            void IEnemyBossState.OnStart(EnemyBossStateType beforeState, EnemyBossCore enemy)
            {
                core ??= GetComponent<EnemyBossCore>();
                rb   ??= rightHand.GetComponent<Rigidbody2D>();
                time = 0f;
            }

            void IEnemyBossState.OnUpdate(EnemyBossCore enemy)
            {
                Debug.Log(StateType);

                // 追従処理
                Follow(rb, rightHand, core.Spd, Distance(player, rightHand), true);

                if (!WaitTime(transTimeCount)) return;
                Debug.Log("攻撃可能！！");

                // 攻撃範囲内の場合
                if (Detection(Distance(player, rightHand), core.AtkRange))
                {
                    Debug.Log("Player検知！！");
                    if (!core.WaitTime(atkWaitCount)) return;
                    // Animationを再生
                    Debug.Log("手のひら攻撃！！！");
                    StateChangeManager();
                }
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
                rb.velocity = Vector2.zero;
                ChangeStateEvent(EnemyBossStateType.IDLE);
            }

            // 時間待機メソッド
            public bool WaitTime(float count)
            {
                time += Time.deltaTime;

                if (time >= count)
                {
                    return true;
                }
                return false;
            }

        }
    }
}
