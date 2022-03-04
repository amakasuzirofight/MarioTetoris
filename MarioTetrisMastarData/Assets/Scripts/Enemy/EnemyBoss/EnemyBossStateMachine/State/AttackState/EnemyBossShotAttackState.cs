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

            [SerializeField] private GameObject bullet;
            [SerializeField] private GameObject faceObj;
            [SerializeField] private Vector3 defPos;

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

                ShotStatePos();
                Instantiate(bullet);
                ReturnPos();
                
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

            private void ShotStatePos() 
            {
                // Player�̂���ʒu�����ĉE�[�����[�Ɋ炪�ړ�����
            }

            private void ReturnPos() 
            {
                // ���̈ʒu�ɖ߂�
            }
        }
    }
}
