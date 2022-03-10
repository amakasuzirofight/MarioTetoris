using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossHandAttackState : EnemyBaseMove, IEnemyBossState
        {
            // Enemyの叩きつけ攻撃状態処理

            [SerializeField] private GameObject animObj;
            [SerializeField] private GameObject handAtkObj;
            [SerializeField] private GameObject idleObj;

            [SerializeField] private GameObject rightHand;
            [SerializeField] private GameObject leftHand;

            [SerializeField] private float moveRangX;
            [SerializeField] private Vector3 defPos_left;
            [SerializeField] private Vector3 defPos_right;
            [SerializeField] private Vector3 pos_left;
            [SerializeField] private Vector3 pos_right;

            public EnemyBossStateType StateType => EnemyBossStateType.HANDATTACK;
            public event Action<EnemyBossStateType> ChangeStateEvent;

            private EnemyBossCore core;
            private GameObject    player;
            private GameObject    handObj;
            private Rigidbody2D   rb_R;
            private Rigidbody2D   rb_L;
            private Rigidbody2D   rb;
            private Animator      animator;
            
            private float transTimeCount = 5f;
            private float atkWaitCount   = 3f;
            private float time;

            

            void IEnemyBossState.OnStart(EnemyBossStateType beforeState, EnemyBossCore enemy)
            {
                player   ??= Utility_.playerObject; 
                core     ??= GetComponent<EnemyBossCore>();
                rb_R     ??= rightHand.GetComponent<Rigidbody2D>();
                rb_L     ??= leftHand.GetComponent<Rigidbody2D>();
                animator ??= animObj.GetComponent<Animator>();

                idleObj.SetActive(false);
                handAtkObj.SetActive(true);

                time = 0f;
            }

            void IEnemyBossState.OnUpdate(EnemyBossCore enemy)
            {
                Debug.Log(StateType);

                // 攻撃する手を変更
                handObj = player.transform.position.x < 0 ? rightHand : leftHand;
                rb      = player.transform.position.x < 0 ? rb_R : rb_L;


                // 反対の手を止める
                if (rb == rb_R) rb_L.velocity = Vector2.zero;
                else            rb_R.velocity = Vector2.zero;

                // 追従処理
                Follow(rb, handObj, core.Spd, Distance(player, handObj), true);

                if (!WaitTime(transTimeCount)) return;
                Debug.Log("攻撃可能！！");

                // 攻撃範囲内の場合
                if (Detection(Distance(player, handObj), core.AtkRange))
                {
                    Debug.Log("Player検知！！");
                    if (!core.WaitTime(atkWaitCount)) return;
                    // Animationを再生
                    Debug.Log("手のひら攻撃！！！");
                    if (handObj == rightHand) animator.SetTrigger("HandAtk_R");
                    else                      animator.SetTrigger("HandAtk_L");
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


                if (!core.WaitTime(5)) return;
                handAtkObj.SetActive(false);
                idleObj.SetActive(true);

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
