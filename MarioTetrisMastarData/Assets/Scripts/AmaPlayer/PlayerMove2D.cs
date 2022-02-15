using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMove2D :IPlayerAction
    {
        public event ChangeStater changeStateEvent;
        public event CoreUpdate coreUpdateEvent;

        public void StateUpdate()
        {

        }
    }

}
