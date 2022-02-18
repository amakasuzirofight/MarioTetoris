using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldChenger : MonoBehaviour
{
    public FieldBase nextField;

    private bool activeFlg;

    public FieldBase nextActive()
    {
        return nextField;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player") activeFlg = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player") activeFlg = false;
    }
}
