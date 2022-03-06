using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseStatus : MonoBehaviour
    {
        // Enemy���X�e�[�^�X�Ǘ�����

        public int Hp { get; set; }
        public int AtkPow { get; set; }
        public float Spd { get; set; }
        public float DiteRange { get; set; }
        public float AtkRange { get; set; }
        public int AtkPow_Hand { get; set; }
        public int AtkPow_Spit { get; set; }
    }
}
