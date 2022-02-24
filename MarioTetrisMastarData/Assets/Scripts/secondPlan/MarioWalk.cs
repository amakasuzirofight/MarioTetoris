using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioWalk : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExecutionWalk(float speed)
    {

        rigidbody2D.velocity = new Vector2(/*rigidbody2D.velocity.x+*/speed, rigidbody2D.velocity.y);
    }
}
