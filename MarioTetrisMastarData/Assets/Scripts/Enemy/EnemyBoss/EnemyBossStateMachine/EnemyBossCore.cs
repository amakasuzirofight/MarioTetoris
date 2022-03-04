using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossCore : EnemyBaseStatus
        {
            [SerializeField, Tooltip("�̗�")]         private int hp;
            [SerializeField, Tooltip("�U����")]       private int atkPow;
            [SerializeField, Tooltip("�ړ����x")]     private float spd;
            [SerializeField, Tooltip("���m�͈�")]     private float diteRange;
            [SerializeField, Tooltip("�U�����m�͈�")] private float atkRange;

            private float time = 0;

            void Start()
            {
                StatusSet(hp, atkPow, spd, diteRange, atkRange);
            }

            // �X�e�[�^�X���������\�b�h(�R���X�g���N�^�ł����H�H)
            private void StatusSet(int hp, int atkPow, float spd, float diteRange, float atkRange)
            {
                Hp = hp;
                AtkPow = atkPow;
                Spd = spd;
                DiteRange = diteRange;
                AtkRange = atkRange;
            }

            // ���ԑҋ@���\�b�h(���[�v)
            public bool WaitTime(float count)
            {
                time += Time.deltaTime;

                if (time >= count)
                {
                    time = 0;
                    return true;
                }
                return false;
            }
        }
    }
}
