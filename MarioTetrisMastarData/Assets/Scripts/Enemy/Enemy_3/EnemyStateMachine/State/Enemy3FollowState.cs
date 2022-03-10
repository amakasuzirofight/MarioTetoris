using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3FollowState : EnemyBaseMove, IEnemy3State
        {
            //Enemy��Follow��ԏ���

            public Enemy3StateType StateType => Enemy3StateType.FOLLOW;
            public event Action<Enemy3StateType> ChangeStateEvent;

            private GameObject  player;
            private Enemy3Core  core;
            private Rigidbody2D rb;

            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
                player ??= Utility_.playerObject;
                core   ??= GetComponent<Enemy3Core>();
                rb     ??= GetComponent<Rigidbody2D>();
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
                // �Ǐ]����
                Follow(rb, gameObject,core.Spd, Distance(player,gameObject), Detection(Distance(player,gameObject), core.DiteRange));
                StateChangeManager();
            }

            void IEnemy3State.OnFixedUpdate(Enemy3Core enemy)
            {
            }

            void IEnemy3State.OnEnd(Enemy3StateType nextState, Enemy3Core enemy)
            {
            }


            // �X�e�[�g�ύX���\�b�h
            private void StateChangeManager()
            {
                // �I�u�W�F�N�g�����m�͈͓��̏ꍇ
                if (!Detection(Distance(player, gameObject), core.DiteRange))
                {
                    ChangeStateEvent(Enemy3StateType.STAY);
                }

                // �U���͈͓��̏ꍇ
                if (Detection(Distance(player, gameObject), core.AtkRange))
                {
                    ChangeStateEvent(Enemy3StateType.ATTACK);
                }
            }

            // �����蔻��
            private void OnTriggerEnter2D(Collider2D collision)
            {
                var player = collision.GetComponent<TestMarioAttack>();

                // Player�̍U���ɓ���������
                if (player != null)
                {
                    ChangeStateEvent(Enemy3StateType.DAMAGED);
                    StateChangeManager();
                }
            }
        }
    }
}
