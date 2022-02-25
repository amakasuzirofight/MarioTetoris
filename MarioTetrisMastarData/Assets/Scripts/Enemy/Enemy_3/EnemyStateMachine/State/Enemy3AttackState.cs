using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3AttackState : EnemyBaseMove, IEnemy3State
        {
            //Enemy��Damage��ԏ���

            public Enemy3StateType StateType => Enemy3StateType.ATTACK;
            public event Action<Enemy3StateType> ChangeStateEvent;

            private Enemy3Core core;

            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
                core ??= GetComponent<Enemy3Core>();
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
                Debug.Log(StateType);
                Debug.Log("�U���I�I");

                // ���̊Ԃɑ҂����Ԃ����

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
                // �ړ���ԂɑJ��
                ChangeStateEvent(Enemy3StateType.STAY);
            }

        }
    }
}
