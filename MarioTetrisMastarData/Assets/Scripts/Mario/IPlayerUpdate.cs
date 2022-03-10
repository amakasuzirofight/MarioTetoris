using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mario
{
    interface IPlayerUpdate
    {
        public void MarioUpdate();
        public void MarioFixedUpdate();
    }
}