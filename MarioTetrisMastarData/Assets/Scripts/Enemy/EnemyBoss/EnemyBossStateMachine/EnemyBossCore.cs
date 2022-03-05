using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossCore : EnemyBaseStatus
        {
            [SerializeField, Tooltip("体力")]         private int hp;
            [SerializeField, Tooltip("攻撃力(手)")]   private int atkPow_Hand;
            [SerializeField, Tooltip("攻撃力(唾)")]   private int atkPow_Spit;
            [SerializeField, Tooltip("移動速度")]     private float spd_Hand;
            [SerializeField, Tooltip("攻撃検知範囲")] private float atkRange;

            private float time = 0;

            void Start()
            {
                StatusSet(hp, atkPow_Hand, atkPow_Spit, spd_Hand, atkRange);
            }

            // ステータス初期化メソッド(コンストラクタでいい？？)
            private void StatusSet(int hp, int atkPow_h,int atkPow_s , float spd, float atkRange)
            {
                Hp = hp;
                AtkPow_Hand = atkPow_h;
                AtkPow_Spit = atkPow_s;
                Spd = spd;
                AtkRange = atkRange;
            }

            // 時間待機メソッド(ループ)
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
