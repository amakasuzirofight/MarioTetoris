using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossLeftHandAttackState : EnemyBaseMove, IEnemyBossState
        {
            //Enemy�̒@�����U����ԏ���

            [SerializeField] private GameObject leftHand;
            [SerializeField] private GameObject player;

            public EnemyBossStateType StateType => EnemyBossStateType.HANDATTACK_LEFT;
            public event Action<EnemyBossStateType> ChangeStateEvent;

            private EnemyBossCore core;
            private Rigidbody2D rb;
            private float atkTimeCount = 5f;
            private float atkWaitCount = 3f;
            private float time;

            void IEnemyBossState.OnStart(EnemyBossStateType beforeState, EnemyBossCore enemy)
            {
                core ??= GetComponent<EnemyBossCore>();
                rb ??= leftHand.GetComponent<Rigidbody2D>();
                time = 0f;
            }

            void IEnemyBossState.OnUpdate(EnemyBossCore enemy)
            {
                Debug.Log(StateType);

                // �Ǐ]����
                Follow(rb, leftHand, core.Spd, Distance(player, leftHand), true);

                if (!WaitTime(atkTimeCount)) return;
                Debug.Log("�U���\�I�I");

                // �U���͈͓��̏ꍇ
                if (Detection(Distance(player, leftHand), core.AtkRange))
                {
                    Debug.Log("Player���m�I�I");
                    if (!core.WaitTime(atkWaitCount)) return;
                    // Animation���Đ�
                    Debug.Log("��̂Ђ�U���I�I�I");
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
