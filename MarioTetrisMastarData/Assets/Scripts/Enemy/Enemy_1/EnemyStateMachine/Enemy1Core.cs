using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1Core : EnemyBaseStatus
        {
            [SerializeField, Tooltip("�̗�")]     private int hp;
            [SerializeField, Tooltip("�U����")]   private int atkPow;
            [SerializeField, Tooltip("�ړ����x")] private float spd;

            void Start()
            {
                StatusSet(hp, atkPow, spd);
            }

            // �X�e�[�^�X���������\�b�h(�R���X�g���N�^�ł����H�H)
            private void StatusSet(int hp, int atkPow, float spd) 
            {
                Hp     = hp;
                AtkPow = atkPow;
                Spd    = spd;
            }
        }
    }
}

