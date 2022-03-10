using Connector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemBom : ItemBase
    {
        Animator animator;
        int damageAmount = 1;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        public override void Hit()
        {
            animator.SetTrigger("explosionTrigger");
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            
            //if (TryGetComponent<IDamageRecevable>(out var toSomethingHit))
            //{
            //    toSomethingHit.DamageRecevable(damageAmount);
            //    Hit();
            //}
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Hit();
            var toSomethingHit = collision.gameObject.GetComponent<IDamageRecevable>();
            if (toSomethingHit != null)
            {
                toSomethingHit.DamageRecevable(damageAmount);
                Debug.Log("name = " + collision.gameObject.name);
            }
        }
    }
}

