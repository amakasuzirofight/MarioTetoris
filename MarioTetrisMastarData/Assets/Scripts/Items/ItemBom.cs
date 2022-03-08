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
            if(TryGetComponent<IDamageRecevable>(out var toPlayerHit))
            {
                toPlayerHit.DamageRecevable(damageAmount);
                Hit();
            }
        }
    }
}

