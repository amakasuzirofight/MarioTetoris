using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputer
{
    public class CharInput : MonoBehaviour, ICharInputter
    {
        public event JumpPush JumpEvent;

        void Start()
        {

        }

        void Update()
        {
            MoveInput();
            if (Input.GetKeyDown(KeyCode.W)) JumpEvent();
        }
        public float MoveInput()
        {
            if (Input.GetKey(KeyCode.D))
            {
                return 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public bool JumpInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                return true;
            }
            return false;
        }

        public Action JumpInputEvent()
        {
            throw new NotImplementedException();
        }
    }

}
