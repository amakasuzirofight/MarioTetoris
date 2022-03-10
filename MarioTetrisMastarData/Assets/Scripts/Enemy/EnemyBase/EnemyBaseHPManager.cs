using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseHPManager : MonoBehaviour
    {
        // Enemyの基底HPクラス

        private const int MAX_HP = 100;
        private const int MIN_HP = 0;

        protected int Damage(int hp, int damageNum)
        {
            hp -= damageNum;
            return Mathf.Max(hp, MIN_HP);
        }

        protected int Recovery(int hp, int recoveryNum)
        {
            hp += recoveryNum;
            return Mathf.Min(hp, MAX_HP);
        }
    }

}
