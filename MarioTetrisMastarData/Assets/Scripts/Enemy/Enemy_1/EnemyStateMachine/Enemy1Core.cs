using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1Core : EnemyBaseStatus
        {
            [SerializeField, Tooltip("体力")]     private int hp;
            [SerializeField, Tooltip("攻撃力")]   private int atkPow;
            [SerializeField, Tooltip("移動速度")] private float spd;

            void Start()
            {
                StatusSet(hp, atkPow, spd);
            }

            // ステータス初期化メソッド(コンストラクタでいい？？)
            private void StatusSet(int hp, int atkPow, float spd) 
            {
                Hp     = hp;
                AtkPow = atkPow;
                Spd    = spd;
            }
        }
    }
}

