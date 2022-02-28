using Connector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemBom : ItemBase
    {
        int damageAmount = 1;

        public override void Hit()
        {
            
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var toPlayerHit = collision.gameObject.GetComponent<IRecoveryRecevable>();

            if(toPlayerHit != null)
            {
                toPlayerHit.RecoveryRecevable(damageAmount);
                Hit();
            }
        }
    }
}

