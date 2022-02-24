using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2Core : EnemyBaseStatus
        {
            [SerializeField, Tooltip("体力")]         private int   hp;
            [SerializeField, Tooltip("攻撃力")]       private int   atkPow;
            [SerializeField, Tooltip("移動速度")]     private float spd;
            [SerializeField, Tooltip("検知範囲")] 　　private float diteRange;
            [SerializeField, Tooltip("攻撃検知範囲")] private float atkRange;

            void Start()
            {
                StatusSet(hp, atkPow, spd, diteRange, atkRange);
            }

            // ステータス初期化メソッド(コンストラクタでいい？？)
            private void StatusSet(int hp, int atkPow, float spd, float diteRange, float atkRange)
            {
                Hp        = hp;
                AtkPow    = atkPow;
                Spd       = spd;
                DiteRange = diteRange;
                AtkRange  = atkRange;
            }
        }
    }
}
