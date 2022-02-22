using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMarioAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Connector.IDamageRecevable damageRecevable;
        if (collision.gameObject.GetComponent< Connector.IDamageRecevable>()!=null)
        {
            damageRecevable = collision.gameObject.GetComponent<Connector.IDamageRecevable>();
            damageRecevable.DamageRecevable(5);
        }
    }
}
