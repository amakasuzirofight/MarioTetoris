using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inputer
{
    interface ICharInputter
    {
        public float MoveInput();
        public bool JumpInput();
    }
}