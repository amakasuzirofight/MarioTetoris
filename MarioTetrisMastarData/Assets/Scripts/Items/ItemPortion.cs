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
            //if (TryGetComponent<IRecoveryReceivable>(out var toSomethingHit))
            //{
            //    toSomethingHit.RecoveryReceivable(recoveryAmount);
            //    Hit();
            //}
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var toSomethingHit = collision.gameObject.GetComponent<IRecoveryReceivable>();
            if (toSomethingHit != null)
            {
                toSomethingHit.RecoveryReceivable(recoveryAmount);
                Debug.Log("name = " + collision.gameObject.name);
                Hit();
            }
        }
    }
}

