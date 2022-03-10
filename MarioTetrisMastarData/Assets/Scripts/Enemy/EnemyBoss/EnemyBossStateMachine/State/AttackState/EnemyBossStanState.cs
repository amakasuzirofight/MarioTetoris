using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossStanState : MonoBehaviour, IEnemyBossState
        {
            // Enemy�̔��e�U����ԏ���

            public EnemyBossStateType StateType => EnemyBossStateType.STAN;
            public event Action<EnemyBossStateType> ChangeStateEvent;

            private EnemyBossCore core;
            private float transTimeCount = 16;


            void IEnemyBossState.OnStart(EnemyBossStateType beforeState, EnemyBossCore enemy)
            {
                core ??= GetComponent<EnemyBossCore>();
                // ���Ł[����ĂȂ��Ă�Animation
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
                // ���̏�Ԃɖ߂�Animation
                ChangeStateEvent(EnemyBossStateType.IDLE);
            }
        }
    }
}
