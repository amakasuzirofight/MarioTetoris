using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossShotAttackState : MonoBehaviour, IEnemyBossState
        {
            //Enemy�̓G�Ăяo����ԏ���

            public EnemyBossStateType StateType => EnemyBossStateType.SHOTATTACK;
            public event Action<EnemyBossStateType> ChangeStateEvent;

            private EnemyBossCore core;
            private float transTimeCount = 3f;

            void IEnemyBossState.OnStart(EnemyBossStateType beforeState, EnemyBossCore enemy)
            {
                core ??= GetComponent<EnemyBossCore>();
            }

            void IEnemyBossState.OnUpdate(EnemyBossCore enemy)
            {
                Debug.Log(StateType);
                StateChangeManager();
            }

            void IEnemyBossState.OnFixedUpdate(EnemyBossCore enemy)
            {
            }

            void IEnemyBossState.OnEnd(EnemyBossStateType nextState, EnemyBossCore enemy)
            {
            }

            // �X�e�[�g�ύX���\�b�h
            private void StateChangeManager()
            {
                if (!core.WaitTime(transTimeCount)) return;
                ChangeStateEvent(EnemyBossStateType.IDLE);
            }

        }
    }
}
