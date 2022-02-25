using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseHPManager : MonoBehaviour
    {
        // Enemy‚ÌŠî’êHPƒNƒ‰ƒX

        private const int MAX_HP = 10;// Œã‚Å•ÏX
        private const int MIN_HP = 0;

        protected int Damage(int hp, int damageNum)
        {
            hp -= damageNum;
            return Mathf.Max(hp, MIN_HP);
        }

        protected float Recovery(int hp, int recoveryNum)
        {
            hp += recoveryNum;
            return Mathf.Min(hp, MAX_HP);
        }
    }

}
