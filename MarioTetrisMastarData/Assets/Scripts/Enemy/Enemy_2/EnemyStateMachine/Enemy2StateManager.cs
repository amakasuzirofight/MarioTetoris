using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2StateManager : MonoBehaviour
        {
            // �X�e�[�g�ύX���鏈��

            [SerializeField]
            private Enemy2Core enemy2;

            private Enemy2StateType crrentEnemy2State = Enemy2StateType.START;
            private Dictionary<Enemy2StateType, IEnemy2State> enemyStateDic = new Dictionary<Enemy2StateType, IEnemy2State>((int)Enemy2StateType.COUNT);


            void Start()
            {
                // IPlayer��z��ŕ����擾���Ă���̂͂Ȃ��H
                IEnemy2State[] stateComponents = GetComponents<IEnemy2State>();

                if (stateComponents.Length != (int)Enemy2StateType.COUNT)
                {
                    Debug.LogError("State�̐����Ⴂ�܂�");
                }

                // ������Dictionary�̒��g��ݒ肵�Ă�
                for (int i = 0; i < stateComponents.Length; i++)
                {
                    IEnemy2State state = stateComponents[i];
                    // �N���X���z��ɂȂ��Ă�I�H�����I�H
                    // ����N���X�̕ϐ��ł͂Ȃ��̂��H

                    state.ChangeStateEvent += ChangeState;

                    if (state.StateType == Enemy2StateType.COUNT)
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
                    //Debug.Log(state.StateType);

                    // playerStateDic���Ă̂����݂�Player�̃X�e�[�g��ێ����Ă�
                    // Dictionary��[Key]��value�������Ă���
                    // Key(���O�݂����Ȃ���H)��Player�̃X�e�[�gEnum�ɂ��Ă���
                    // Value�͌��݂�Player�̃X�e�[�g�����Ă�

                    // playerStateDic[�Y����] = �l�����Ă��� 
                }
            }

            void Update()
            {
                enemyStateDic[crrentEnemy2State].OnUpdate(enemy2);
            }

            private void FixedUpdate()
            {
                enemyStateDic[crrentEnemy2State].OnFixedUpdate(enemy2);
            }


            // �����̒���Enum�^�ɂ��Ă��������ۂ��I
            // �X�e�[�g�ύX���\�b�h(event�ϐ��ɑ�����郁�\�b�h)
            public void ChangeState(Enemy2StateType enemyState)
            {
                if (enemyState is Enemy2StateType.COUNT)
                {
                    Debug.LogErrorFormat($"Count state is specified ,CurrentState{crrentEnemy2State}");
                    return;
                }

                enemyStateDic[crrentEnemy2State].OnEnd(enemyState, enemy2);

                // ���g��ύX
                crrentEnemy2State = enemyState;

                enemyStateDic[crrentEnemy2State].OnStart(enemyState, enemy2);
            }
        }
    }

}
