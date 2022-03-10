using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1StateManager : MonoBehaviour,IEnemyUpdateSendable
        {
            // �X�e�[�g�ύX���鏈��

            [SerializeField]
            private Enemy1Core enemy1;

            private Enemy1StateType crrentEnemy1State = Enemy1StateType.START;
            private Dictionary<Enemy1StateType, IEnemy1State> enemyStateDic = new Dictionary<Enemy1StateType, IEnemy1State>((int)Enemy1StateType.COUNT);

            private Rigidbody2D rb;

            void Start()
            {
                rb = GetComponent<Rigidbody2D>();

                // IPlayer��z��ŕ����擾���Ă���̂͂Ȃ��H
                IEnemy1State[] stateComponents = GetComponents<IEnemy1State>();

                if (stateComponents.Length != (int)Enemy1StateType.COUNT)
                {
                    Debug.LogError("State�̐����Ⴂ�܂�");
                }

                // ������Dictionary�̒��g��ݒ肵�Ă�
                for (int i = 0; i < stateComponents.Length; i++)
                {
                    IEnemy1State state = stateComponents[i];
                    // �N���X���z��ɂȂ��Ă�I�H�����I�H
                    // ����N���X�̕ϐ��ł͂Ȃ��̂��H

                    state.ChangeStateEvent += ChangeState;

                    if (state.StateType == Enemy1StateType.COUNT)
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

            private void FixedUpdate()
            {
                enemyStateDic[crrentEnemy1State].OnFixedUpdate(enemy1);
            }


            // �����̒���Enum�^�ɂ��Ă��������ۂ��I
            // �X�e�[�g�ύX���\�b�h(event�ϐ��ɑ�����郁�\�b�h)
            public void ChangeState(Enemy1StateType enemyState)
            {
                if (enemyState is Enemy1StateType.COUNT)
                {
                    Debug.LogErrorFormat($"Count state is specified ,CurrentState{crrentEnemy1State}");
                    return;
                }

                enemyStateDic[crrentEnemy1State].OnEnd(enemyState, enemy1);

                // ���g��ύX
                crrentEnemy1State = enemyState;

                enemyStateDic[crrentEnemy1State].OnStart(enemyState, enemy1);
            }


            //private void Update()
            //{
            //    enemyStateDic[crrentEnemy1State].OnUpdate(enemy1);
            //}

            // Update�C���^�t�F�[�X
            void IEnemyUpdateSendable.EnemyUpdate()
            {
                enemyStateDic[crrentEnemy1State].OnUpdate(enemy1);
            }

            // Velocity�������C���^�t�F�[�X
            void IEnemyUpdateSendable.EnemyVelocityDefault()
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
