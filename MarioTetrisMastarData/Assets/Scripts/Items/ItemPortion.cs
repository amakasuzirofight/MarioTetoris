using Connector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemPortion : ItemBase
    {
        int recoveryAmount = 1;

        public override void Hit()
        {
            Destroy(this.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (TryGetComponent<IRecoveryReceivable>(out var toSometningHit))
            {
                toSometningHit.RecoveryReceivable(recoveryAmount);
                Hit();
            }
        }
    }
}

