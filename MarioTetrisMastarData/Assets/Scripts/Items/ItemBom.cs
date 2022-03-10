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
            animator.SetTrigger("explosionTrriger");
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

