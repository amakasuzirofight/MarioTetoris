using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Field
{
    [Serializable]
    public class FieldChenger : MonoBehaviour
    {
        public FieldBase nextField;

        public bool activeFlg = false;

        public Action nextFieldAction;

        [SerializeField] private KeyCode debugCode = KeyCode.Return;

        public virtual GameObject Create()
        {
            return Instantiate(gameObject);
        }

        public void Update()
        {
            if (Input.GetKeyDown(debugCode))
            {
                activeFlg = true;
            }
        }

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

}