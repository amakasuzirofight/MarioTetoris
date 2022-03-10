using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3DamageState : EnemyBaseTouched, IEnemy3State
        {
            //Enemy��Damage��ԏ���

            public Enemy3StateType StateType => Enemy3StateType.DAMAGED;
            public event Action<Enemy3StateType> ChangeStateEvent;

            private Enemy3Core core;
            private Animator animator;


            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
                core ??= GetComponent<Enemy3Core>();
                animator ??= GetComponent<Animator>();

                animator.SetTrigger("Damage");
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
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
                // HP��0�̏ꍇ
                if (core.Hp <= 0)
                {
                    // ���S��ԂɑJ��
                    ChangeStateEvent(Enemy3StateType.DEAD);
                }
                else
                {
                    // �ړ���ԂɑJ��
                    ChangeStateEvent(Enemy3StateType.STAY);
                }
            }

        }
    }
}
