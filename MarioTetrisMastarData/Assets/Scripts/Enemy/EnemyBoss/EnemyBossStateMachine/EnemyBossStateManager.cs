using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossStateManager : MonoBehaviour,IEnemyUpdateSendable
        {
            // �X�e�[�g�ύX���鏈��

            [SerializeField] private EnemyBossCore enemyBoss;


            [SerializeField] private EnemyBossStateType[] stateBrock_1;
            [SerializeField] private EnemyBossStateType[] stateBrock_2;
            [SerializeField] private EnemyBossStateType[] stateBrock_3;

            private EnemyBossStateType[] stateFlow;
            private EnemyBossCore core;
            private int thisStateNum = 0;


            private EnemyBossStateType crrentEnemyBossState = EnemyBossStateType.START;
            private Dictionary<EnemyBossStateType, IEnemyBossState> enemyStateDic = new Dictionary<EnemyBossStateType, IEnemyBossState>((int)EnemyBossStateType.COUNT);


            void Start()
            {
                core = GetComponent<EnemyBossCore>();

                // ��������X�e�[�g�N���X���擾
                IEnemyBossState[] stateComponents = GetComponents<IEnemyBossState>();

                if (stateComponents.Length != (int)EnemyBossStateType.COUNT)
                {
                    Debug.LogError("State�̐����Ⴂ�܂�");
                }

                // ������Dictionary�̒��g��ݒ肵�Ă�
                for (int i = 0; i < stateComponents.Length; i++)
                {
                    // �X�e�[�g�N���X��ݒ�
                    IEnemyBossState state = stateComponents[i];

                    // event��ݒ�
                    state.ChangeStateEvent += ChangeState;

                    if (state.StateType == EnemyBossStateType.COUNT)
                    {
                        Debug.LogError("������Enum�ł�");
                        return;
                    }
                    if (enemyStateDic.ContainsKey(state.StateType))
                    {
                        Debug.LogError("State���d�����Ă��܂�");
                        return;
                    }

                    // �Ō�ɂ�����Player�̃X�e�[�g����
                    enemyStateDic[state.StateType] = state;

                    // playerStateDic���Ă̂����݂�Player�̃X�e�[�g��ێ����Ă�
                    // Dictionary��[Key]��value�������Ă���
                    // Key(���O�݂����Ȃ���H)��Player�̃X�e�[�gEnum�ɂ��Ă���
                    // Value�͌��݂�Player�̃X�e�[�g�����Ă�

                    // playerStateDic[�Y����] == ���݂̃X�e�[�g
                }
            }

            private void Update()
            {
                enemyStateDic[crrentEnemyBossState].OnUpdate(enemyBoss);
            }

            private void FixedUpdate()
            {
                enemyStateDic[crrentEnemyBossState].OnFixedUpdate(enemyBoss);
            }

            // �_���[�W��ԑJ�ڃ��\�b�h
            private void DamageState()
            {
                enemyStateDic[crrentEnemyBossState].OnEnd(EnemyBossStateType.IDLE/*�_���[�W*/, enemyBoss);
                //thisStateNum = 0; // �X�e�[�g����񃊃Z�b�g����ꍇ
                crrentEnemyBossState = EnemyBossStateType.DAMAGE; // �_���[�W
                enemyStateDic[crrentEnemyBossState].OnStart(EnemyBossStateType.IDLE, enemyBoss);
            }

            // �X�e�[�g���f����
            public void BreakState()
            {
                DamageState();
            }

            // �X�e�[�g�ύX���\�b�h(event�ϐ��ɑ�����郁�\�b�h)
            public void ChangeState(EnemyBossStateType enemyState)
            {
                if (enemyState is EnemyBossStateType.COUNT)
                {
                    Debug.LogErrorFormat($"Count state is specified ,CurrentState{crrentEnemyBossState}");
                    return;
                }

                enemyStateDic[crrentEnemyBossState].OnEnd(enemyState, enemyBoss);


                // �L�����N�^�[��HP�ɂ���čs���X�e�[�g��ύX����
                if (core.Hp < 30) stateFlow = stateBrock_3;
                else if (core.Hp < 50) stateFlow = stateBrock_2;
                else if (core.Hp <= 100) stateFlow = stateBrock_1;


                // ���g��ύX
                crrentEnemyBossState = stateFlow[thisStateNum];

                // �v�f�����z��̒��������̏ꍇ
                if (thisStateNum < stateFlow.Length - 1) thisStateNum++;
                else thisStateNum = 0;

                enemyStateDic[crrentEnemyBossState].OnStart(enemyState, enemyBoss);
            }

            // Update�C���^�t�F�[�X
            void IEnemyUpdateSendable.EnemyUpdate()
            {
            }
        }
    }
}
