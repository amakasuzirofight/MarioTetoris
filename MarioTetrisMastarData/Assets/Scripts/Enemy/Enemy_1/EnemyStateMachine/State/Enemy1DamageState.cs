using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1DamageState : EnemyBaseHPManager, IEnemy1State
        {
            // Player��Damage��ԏ���

            public Enemy1StateType StateType =>  Enemy1StateType.DAMAGED;
            public event Action<Enemy1StateType> ChangeStateEvent;

            private Enemy1Core core;

            void IEnemy1State.OnStart(Enemy1StateType beforeState, Enemy1Core enemy)
            {
                core ??= GetComponent<Enemy1Core>();
            }

            void IEnemy1State.OnUpdate(Enemy1Core enemy)
            {
                Debug.Log(StateType);
                StateChangeManager();
            }

            void IEnemy1State.OnFixedUpdate(Enemy1Core enemy)
            {
            }

            void IEnemy1State.OnEnd(Enemy1StateType nextState, Enemy1Core enemy)
            {
            }

            // �X�e�[�g�ύX���\�b�h
            private void StateChangeManager()
            {
                // HP��0�̏ꍇ
                if (core.Hp <= 0) 
                {
                    // ���S��ԂɑJ��
                    ChangeStateEvent(Enemy1StateType.DEAD);
                }
                else 
                {
                    // �ړ���ԂɑJ��
                    ChangeStateEvent(Enemy1StateType.MOVE);
                }
            }
        }
    }
}
