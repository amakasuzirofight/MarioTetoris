using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IGroundCheck
{
    public event System.Action<bool> OnGround;
    public event System.Action<bool> ExitGround;
    //public bool IsGroundCheck();
}