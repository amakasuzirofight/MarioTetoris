using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseStatus : MonoBehaviour
    {
        // Enemy基底ステータス管理処理

        public int Hp { get; set; }
        public int AtkPow { get; set; }
        public float Spd { get; set; }
    }
}
