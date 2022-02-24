using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inputer
{
    public class SelectInput : MonoBehaviour,ISelectInput
    {
        public event Action<SelectBottonType> OnSelectButton;
        public event Action<float> MouceWhileEvent;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                OnSelectButton(SelectBottonType.ArrowDown);
            }
            if(Input.GetMouseButtonDown(0))
            {
                OnSelectButton(SelectBottonType.MouceRight);
            }
            if (Input.GetMouseButtonDown(1))
            {
                OnSelectButton(SelectBottonType.MouceLeft);
            }
            if(Input.mouseScrollDelta.y!=0)
            {
                MouceWhileEvent(Input.mouseScrollDelta.y);
            }
        }
    }
    public enum SelectBottonType
    {
        MouceLeft,MouceRight,ArrowDown,Non
    }
}


