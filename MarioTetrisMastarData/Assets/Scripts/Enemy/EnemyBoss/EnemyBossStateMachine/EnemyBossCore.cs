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
            [SerializeField, Tooltip("�U����(��)")]   private int atkPow_Hand;
            [SerializeField, Tooltip("�U����(��)")]   private int atkPow_Spit;
            [SerializeField, Tooltip("�ړ����x")]     private float spd_Hand;
            [SerializeField, Tooltip("�U�����m�͈�")] private float atkRange;

            private float time = 0;

            void Start()
            {
                StatusSet(hp, atkPow_Hand, atkPow_Spit, spd_Hand, atkRange);
            }

            // �X�e�[�^�X���������\�b�h(�R���X�g���N�^�ł����H�H)
            private void StatusSet(int hp, int atkPow_h,int atkPow_s , float spd, float atkRange)
            {
                Hp = hp;
                AtkPow_Hand = atkPow_h;
                AtkPow_Spit = atkPow_s;
                Spd = spd;
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
