using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseStatus : MonoBehaviour
    {
        public int Hp { get; set; }
        public int AtkPow { get; set; }
        public float Spd { get; set; }
    }
}
