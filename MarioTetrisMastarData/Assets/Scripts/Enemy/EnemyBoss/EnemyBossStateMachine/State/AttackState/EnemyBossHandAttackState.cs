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
            // Enemy�̒@�����U����ԏ���

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

                // �U��������ύX
                handObj = player.transform.position.x < 0 ? rightHand : leftHand;
                rb      = player.transform.position.x < 0 ? rb_R : rb_L;


                // ���΂̎���~�߂�
                if (rb == rb_R) rb_L.velocity = Vector2.zero;
                else            rb_R.velocity = Vector2.zero;

                // �Ǐ]����
                Follow(rb, handObj, core.Spd, Distance(player, handObj), true);

                if (!WaitTime(transTimeCount)) return;
                Debug.Log("�U���\�I�I");

                // �U���͈͓��̏ꍇ
                if (Detection(Distance(player, handObj), core.AtkRange))
                {
                    Debug.Log("Player���m�I�I");
                    if (!core.WaitTime(atkWaitCount)) return;
                    // Animation���Đ�
                    Debug.Log("��̂Ђ�U���I�I�I");
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


            // �X�e�[�g�ύX���\�b�h
            private void StateChangeManager()
            {
                rb.velocity = Vector2.zero;


                if (!core.WaitTime(5)) return;
                handAtkObj.SetActive(false);
                idleObj.SetActive(true);

                ChangeStateEvent(EnemyBossStateType.IDLE);
            }

            // ���ԑҋ@���\�b�h
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
