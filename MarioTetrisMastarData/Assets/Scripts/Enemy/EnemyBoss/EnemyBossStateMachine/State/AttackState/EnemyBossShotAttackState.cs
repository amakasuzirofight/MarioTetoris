using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossShotAttackState : MonoBehaviour, IEnemyBossState
        {
            //Enemy�̓G�Ăяo����ԏ���

            [SerializeField] private GameObject bullet;
            [SerializeField] private GameObject faceObj;

            public EnemyBossStateType StateType => EnemyBossStateType.SHOTATTACK;
            public event Action<EnemyBossStateType> ChangeStateEvent;

            private EnemyBossCore core;
            private GameObject player;
            private float transTimeCount = 3f;

            void IEnemyBossState.OnStart(EnemyBossStateType beforeState, EnemyBossCore enemy)
            {
                player ??= Utility_.playerObject;
                core ??= GetComponent<EnemyBossCore>();

                ShotStatePos();
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

            // �������\�b�h(�A�j���[�V�����ŌĂяo��)
            public void Genarator()
            {
                Vector3 facePos = faceObj.transform.position;
                int offset = 0;

                for (int i = 0; i < 3; ++i)
                {
                    Instantiate(bullet).transform.position = new Vector3(facePos.x, facePos.y + offset, facePos.z);
                    offset++;
                }
            }

            // Boss�̊�ړ����\�b�h
            private void ShotStatePos()
            {
                // Player�̂���ʒu�����ĉE�[�����[�Ɋ炪�ړ�����
                float dir = player.transform.position.x;

                // Aniamtion���Ăяo��
                if (dir < 0) Debug.Log("�����Ɉړ�");
                if(dir >= 0) Debug.Log("�E���Ɉړ�");
            }
        }
    }
}
