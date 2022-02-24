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

            [SerializeField] private GameObject player;

            public Enemy2StateType StateType => Enemy2StateType.ATTACK;
            public event Action<Enemy2StateType> ChangeStateEvent;

            private Enemy2Core core;

            void IEnemy2State.OnStart(Enemy2StateType beforeState, Enemy2Core enemy)
            {
                core ??= GetComponent<Enemy2Core>();
            }

            void IEnemy2State.OnUpdate(Enemy2Core enemy)
            {
                Debug.Log(StateType);

                Debug.Log("�U���I�I");

                // ���̊Ԃɑ҂����Ԃ����

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
                ChangeStateEvent(Enemy2StateType.STAY);
            }
        }

    }


}
