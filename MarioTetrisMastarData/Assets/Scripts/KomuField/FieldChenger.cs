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

        [HideInInspector] public bool activeFlg = false;

        [SerializeField] private KeyCode debugCode = KeyCode.Return;

        protected int fieldNum;

        public virtual GameObject Create()
        {
            return Instantiate(gameObject);
        }

        public void numberSet(int num)
        {
            fieldNum = num;
        }

        public void Update()
        {
            if (Input.GetKeyDown(debugCode))
            {
                if (fieldNum != 0) Utility_.FlgChenger(fieldNum);
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