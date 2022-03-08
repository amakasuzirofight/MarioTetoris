using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3Core : EnemyBaseStatus
        {
            [SerializeField, Tooltip("�̗�")] private int hp;
            [SerializeField, Tooltip("�U����")] private int atkPow;
            [SerializeField, Tooltip("�ړ����x")] private float spd;
            [SerializeField, Tooltip("���m�͈�")] private float diteRange;
            [SerializeField, Tooltip("�U�����m�͈�")] private float atkRange;

            public bool AtkFlg { get; set; } = true; 

            void Start()
            {
                StatusSet(hp, atkPow, spd, diteRange, atkRange);
            }

            // �X�e�[�^�X���������\�b�h
            private void StatusSet(int hp, int atkPow, float spd, float diteRange, float atkRange)
            {
                Hp     = hp;
                AtkPow = atkPow;
                Spd    = spd;
                DiteRange = diteRange;
                AtkRange = atkRange;
            }
        }
    }
}
