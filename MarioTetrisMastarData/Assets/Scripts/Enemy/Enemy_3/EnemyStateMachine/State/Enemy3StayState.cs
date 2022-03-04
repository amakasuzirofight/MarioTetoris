using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3StayState : EnemyBaseMove, IEnemy3State
        {
            //Enemy��Stay��ԏ���

            [SerializeField] private GameObject player;

            public Enemy3StateType StateType => Enemy3StateType.STAY;
            public event Action<Enemy3StateType> ChangeStateEvent;

            private Enemy3Core core;

            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
                core ??= GetComponent<Enemy3Core>();
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
                Debug.Log(StateType);
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
                // �I�u�W�F�N�g�����m�͈͂ɓ������ꍇ
                if (Detection(Distance(player, gameObject), core.DiteRange))
                {
                    ChangeStateEvent(Enemy3StateType.FOLLOW);
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
