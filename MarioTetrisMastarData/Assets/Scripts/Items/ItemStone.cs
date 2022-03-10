using Connector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemStone : ItemBase
    {
        Rigidbody2D rib2d;
        int damageAmount = 1;

        // Start is called before the first frame update
        void Start()
        {
            rib2d = GetComponent<Rigidbody2D>();
        }

        public override void Hit()
        {
            Destroy(this.gameObject);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (rib2d.velocity.y < 0)
            {
                var toSomethingHit = collision.gameObject.GetComponent<IDamageRecevable>();
                if (toSomethingHit != null)
                {
                    toSomethingHit.DamageRecevable(damageAmount);
                    Debug.Log("name = " + collision.gameObject.name);
                    Hit();
                }
            }
            
            //if (TryGetComponent<IDamageRecevable>(out var toDamageHit))
            //{
            //    toDamageHit.DamageRecevable(damageAmount);
            //    Hit();
            //}
        }
    }
}

