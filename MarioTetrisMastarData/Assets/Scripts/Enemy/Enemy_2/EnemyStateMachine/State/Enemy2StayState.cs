using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2StayState : EnemyBaseMove, IEnemy2State
        {
            //Enemy��Stay��ԏ���

            [SerializeField] private GameObject player;

            public Enemy2StateType StateType => Enemy2StateType.STAY;
            public event Action<Enemy2StateType> ChangeStateEvent;

            private Enemy2Core core;

            void IEnemy2State.OnStart(Enemy2StateType beforeState, Enemy2Core enemy)
            {
                core ??= GetComponent<Enemy2Core>();
            }

            void IEnemy2State.OnUpdate(Enemy2Core enemy)
            {
                Debug.Log(StateType);
                StateChangeManager();
            }

            void IEnemy2State.OnFixedUpdate(Enemy2Core enemy)
            {
            }

            void IEnemy2State.OnEnd(Enemy2StateType nextState, Enemy2Core enemy)
            {
            }

            private void StateChangeManager() 
            {
                // �I�u�W�F�N�g�����m�͈͂ɓ������ꍇ
                if(Detection(Distance(player), core.DiteRange)) 
                {
                    ChangeStateEvent(Enemy2StateType.FOLLOW);
                }
            }


            private void OnTriggerEnter2D(Collider2D collision)
            {
                var player = collision.GetComponent<TestMarioAttack>();

                // Player�̖{�̂ɓ���������
                if (player != null)
                {
                    ChangeStateEvent(Enemy2StateType.DAMAGED);
                }
            }
        }

    }


}
