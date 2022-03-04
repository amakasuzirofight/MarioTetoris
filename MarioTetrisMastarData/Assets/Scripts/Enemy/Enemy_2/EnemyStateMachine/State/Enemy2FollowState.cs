using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2FollowState : EnemyBaseMove, IEnemy2State
        {
            //Enemy��Follow��ԏ���

            public Enemy2StateType StateType => Enemy2StateType.FOLLOW;
            public event Action<Enemy2StateType> ChangeStateEvent;

            private GameObject player;
            private Enemy2Core core;
            private Rigidbody2D rb;

            void IEnemy2State.OnStart(Enemy2StateType beforeState, Enemy2Core enemy)
            {
                player ??= Utility_.playerObject;
                core ??= GetComponent<Enemy2Core>();
                rb   ??= GetComponent<Rigidbody2D>();
            }

            void IEnemy2State.OnUpdate(Enemy2Core enemy)
            {
                Debug.Log(StateType);

                // �Ǐ]����
                Follow(rb, gameObject, core.Spd,Distance(player, gameObject), Detection(Distance(player, gameObject), core.DiteRange));
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
                // �I�u�W�F�N�g�����m�͈͊O�̏ꍇ
                if (!Detection(Distance(player, gameObject), core.DiteRange))
                {
                    ChangeStateEvent(Enemy2StateType.STAY);
                }

                // �U���͈͓��ɓ������ꍇ
                if (Detection(Distance(player, gameObject), core.AtkRange))
                {
                    ChangeStateEvent(Enemy2StateType.ATTACK);
                }
            }


            private void OnTriggerEnter2D(Collider2D collision)
            {
                var player = collision.GetComponent<TestMarioAttack>();

                // Player�̖{�̂ɓ���������
                if (player != null)
                {
                    ChangeStateEvent(Enemy2StateType.DAMAGED);
                    StateChangeManager();
                }
            }
        }

    }


}
