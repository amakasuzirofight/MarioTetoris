using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inputer
{
    public delegate void JumpPush();
    interface ICharInputter
    {
        public float MoveInput();
        public bool JumpInput();
        public event JumpPush JumpEvent;
        //public event Action JumpEvent;
        //public Action JumpInputEvent;
    }
}