using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3StateManager : MonoBehaviour
        {
            // �X�e�[�g�ύX���鏈��

            [SerializeField]
            private Enemy3Core enemy3;

            private Enemy3StateType crrentEnemy3State = Enemy3StateType.START;
            private Dictionary<Enemy3StateType, IEnemy3State> enemyStateDic = new Dictionary<Enemy3StateType, IEnemy3State>((int)Enemy3StateType.COUNT);


            void Start()
            {
                // IPlayer��z��ŕ����擾���Ă���̂͂Ȃ��H
                IEnemy3State[] stateComponents = GetComponents<IEnemy3State>();

                if (stateComponents.Length != (int)Enemy3StateType.COUNT)
                {
                    Debug.LogError("State�̐����Ⴂ�܂�");
                }

                // ������Dictionary�̒��g��ݒ肵�Ă�
                for (int i = 0; i < stateComponents.Length; i++)
                {
                    IEnemy3State state = stateComponents[i];
                    // �N���X���z��ɂȂ��Ă�I�H�����I�H
                    // ����N���X�̕ϐ��ł͂Ȃ��̂��H

                    state.ChangeStateEvent += ChangeState;

                    if (state.StateType == Enemy3StateType.COUNT)
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
                enemyStateDic[crrentEnemy3State].OnUpdate(enemy3);
            }

            private void FixedUpdate()
            {
                enemyStateDic[crrentEnemy3State].OnFixedUpdate(enemy3);
            }


            // �����̒���Enum�^�ɂ��Ă��������ۂ��I
            // �X�e�[�g�ύX���\�b�h(event�ϐ��ɑ�����郁�\�b�h)
            public void ChangeState(Enemy3StateType enemyState)
            {
                if (enemyState is Enemy3StateType.COUNT)
                {
                    Debug.LogErrorFormat($"Count state is specified ,CurrentState{crrentEnemy3State}");
                    return;
                }

                enemyStateDic[crrentEnemy3State].OnEnd(enemyState, enemy3);

                // ���g��ύX
                crrentEnemy3State = enemyState;

                enemyStateDic[crrentEnemy3State].OnStart(enemyState, enemy3);
            }
        }
    }

}
