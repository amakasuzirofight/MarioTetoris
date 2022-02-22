using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;

namespace Enemy
{
    namespace Enemy1State
    {
        public class EnemyHit : EnemyBaseHPManager, IDamageRecevable
        {
            private IDamageRecevable damageRecevable;
            private Enemy1Core enemyCore;

            void Start()
            {
                damageRecevable = GetComponent<IDamageRecevable>();
                enemyCore = GetComponent<Enemy1Core>();
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                // Player‚Ì–{‘Ì‚É“–‚½‚Á‚½‚ç
                damageRecevable.DamageRecevable(enemyCore.AtkPow);
            }

            public void DamageRecevable(int damage)
            {
                Damage(enemyCore.Hp, damage);
            }
        }

    }

}
