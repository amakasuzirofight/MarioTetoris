using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundCheck : MonoBehaviour, IGroundCheck
{
    public event Action<bool> OnGround;
    public event Action<bool> ExitGround;

    bool isGround;
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
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            Debug.Log("‚ ‚½‚Á‚½" + collision.gameObject.layer);
            OnGround(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            Debug.Log("‚Å‚½");
            OnGround(false);

        }
    }


}
