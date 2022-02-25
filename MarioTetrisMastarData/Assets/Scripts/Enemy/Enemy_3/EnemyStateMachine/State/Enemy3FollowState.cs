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

            [SerializeField] private GameObject player;

            public Enemy3StateType StateType => Enemy3StateType.FOLLOW;
            public event Action<Enemy3StateType> ChangeStateEvent;

            private Enemy3Core core;
            private Rigidbody2D rb;

            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
                core ??= GetComponent<Enemy3Core>();
                rb   ??= GetComponent<Rigidbody2D>();
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
                Debug.Log(StateType);

                // �Ǐ]����
                Follow(rb, core.Spd, Distance(player), Detection(Distance(player), core.DiteRange));
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
                if (!Detection(Distance(player), core.DiteRange))
                {
                    ChangeStateEvent(Enemy3StateType.STAY);
                }

                // �U���͈͓��̏ꍇ
                if (Detection(Distance(player), core.AtkRange))
                {
                    ChangeStateEvent(Enemy3StateType.ATTACK);
                }
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                var player = collision.GetComponent<TestMarioAttack>();

                // Player�̖{�̂ɓ���������
                if (player != null)
                {
                    ChangeStateEvent(Enemy3StateType.DAMAGED);
                    StateChangeManager();
                }
            }
        }
    }
}
