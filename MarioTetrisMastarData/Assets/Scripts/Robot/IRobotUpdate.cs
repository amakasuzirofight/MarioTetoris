using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robot
{
    interface IRobotUpdate
    {
        public void RobotUpdate();
        public void RobotFixedUpdate();
    }
}