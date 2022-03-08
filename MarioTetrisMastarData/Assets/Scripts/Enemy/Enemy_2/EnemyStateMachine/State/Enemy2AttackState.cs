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
            // Enemy��Attack��ԏ���

            public Enemy2StateType StateType => Enemy2StateType.ATTACK;
            public event Action<Enemy2StateType> ChangeStateEvent;

            private GameObject player;
            private Enemy2Core core;

            private float time = 0f;
            private float attackTimeCount = 5f;
            private float transTimeCount  = 3f;


            void IEnemy2State.OnStart(Enemy2StateType beforeState, Enemy2Core enemy)
            {
                player ??= Utility_.playerObject;
                core   ??= GetComponent<Enemy2Core>();
            }

            void IEnemy2State.OnUpdate(Enemy2Core enemy)
            {
                // ���̊Ԃɑ҂����Ԃ����
                if (WaitTime(attackTimeCount))
                { 
                    // Animation���Đ�
                    Debug.Log("�U���I�I");
                }

                StateChangeManager();
            }

            void IEnemy2State.OnFixedUpdate(Enemy2Core enemy)
            {
            }

            void IEnemy2State.OnEnd(Enemy2StateType nextState, Enemy2Core enemy)
            {
            }

            // �X�e�[�g�ύX���\�b�h
            private void StateChangeManager()
            {
                // ���m�͈͊O�̏ꍇ
                if (!Detection(Distance(player, gameObject), core.DiteRange))
                {
                    ChangeStateEvent(Enemy2StateType.STAY);
                }

                // �U���͈͊O�̏ꍇ
                if (!Detection(Distance(player, gameObject), core.AtkRange))
                {
                    if (!WaitTime(transTimeCount)) return;

                    ChangeStateEvent(Enemy2StateType.FOLLOW);
                }
            }

            // �����蔻��
            private void OnTriggerEnter2D(Collider2D collision)
            {
                var player = collision.GetComponent<TestMarioAttack>();

                // Player�̖{�̂ɓ���������
                if (player != null)
                {
                    ChangeStateEvent(Enemy2StateType.DAMAGED);
                }
            }

            // ���ԑҋ@���\�b�h
            private bool WaitTime(float count)
            {
                time += Time.deltaTime;

                if (time >= count)
                {
                    time = 0;
                    return true;
                }
                return false;
            }
        }

    }


}
