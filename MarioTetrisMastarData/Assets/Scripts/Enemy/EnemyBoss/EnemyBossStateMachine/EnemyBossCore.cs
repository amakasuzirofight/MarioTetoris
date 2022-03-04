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
            [SerializeField, Tooltip("攻撃力")]       private int atkPow;
            [SerializeField, Tooltip("移動速度")]     private float spd;
            [SerializeField, Tooltip("検知範囲")]     private float diteRange;
            [SerializeField, Tooltip("攻撃検知範囲")] private float atkRange;

            private float time = 0;

            void Start()
            {
                StatusSet(hp, atkPow, spd, diteRange, atkRange);
            }

            // ステータス初期化メソッド(コンストラクタでいい？？)
            private void StatusSet(int hp, int atkPow, float spd, float diteRange, float atkRange)
            {
                Hp = hp;
                AtkPow = atkPow;
                Spd = spd;
                DiteRange = diteRange;
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
