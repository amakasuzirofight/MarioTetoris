using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2Attack : MonoBehaviour
        {
            // Enemy2‚ÌUŒ‚ˆ—

            private IDamageRecevable damageRecevable;
            private Enemy2Core core;


            void Start()
            {
                damageRecevable = GetComponent<IDamageRecevable>();
                core = GetComponent<Enemy2Core>();
            }


            // Player‚ÉG‚ê‚½‚Éƒ_ƒ[ƒW‚ğ—^‚¦‚é(ŠÃ‘Œ‘Ò‚¿)
            private void OnCollisionEnter2D(Collision2D collision)
            {
                if (collision.gameObject.TryGetComponent(out MarioCore at))
                {
                    IDamageRecevable damage = at;
                    Debug.Log("E‚·III");
                    damage.DamageRecevable(core.AtkPow);
                }
            }
        }
    }
}
