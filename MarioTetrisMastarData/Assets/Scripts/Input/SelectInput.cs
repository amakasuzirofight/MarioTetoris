using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inputer
{
    public class SelectInput : MonoBehaviour,ISelectInput
    {
        public event Action<SelectButtonType> OnSelectButton;
        public event Action<float> MouceWhileEvent;

        private void Awake()
        {
            Utility.Locator<ISelectInput>.Bind(this);
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                OnSelectButton(SelectButtonType.ArrowDown);
            }
            if(Input.GetMouseButtonDown(0))
            {
                OnSelectButton(SelectButtonType.MouceLeft);
            }
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("‚Ý‚¬");
                OnSelectButton(SelectButtonType.MouceRight);
            }
            if(Input.mouseScrollDelta.y!=0)
            {
                MouceWhileEvent(Input.mouseScrollDelta.y);
            }
        }
    }
    public enum SelectButtonType
    {
        MouceLeft,MouceRight,ArrowDown,Non
    }
}


