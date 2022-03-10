using Connector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Items
{
    public class ItemBomBlock : ItemBase
    {
        Rigidbody2D rib2d;
        int damageAmount = 1;
        float limitTime = 30f;

        // Start is called before the first frame update
        void Start()
        {
            rib2d = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if(rib2d.velocity.y >= 0)
            {
                limitTime -= Time.deltaTime;

                if(limitTime <= 0)
                {

                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(rib2d.velocity.y < 0)
            {
                var toSomethingHit = collision.gameObject.GetComponent<IDamageRecevable>();
                if (toSomethingHit != null)
                {
                    toSomethingHit.DamageRecevable(damageAmount);
                    Debug.Log("name = " + collision.gameObject.name);
                    Hit();
                }
            }
            //if (TryGetComponent<IDamageRecevable>(out var toSomethingHit))
            //{
            //    toSomethingHit.DamageRecevable(damageAmount);
            //    Hit();
            //}
        }
    }

}
