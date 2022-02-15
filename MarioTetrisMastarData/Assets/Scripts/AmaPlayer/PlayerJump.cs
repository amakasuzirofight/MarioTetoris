using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerJump :IPlayerAction
    {
        public event ChangeStater changeStateEvent;
        public event CoreUpdate coreUpdateEvent;

        public void StateUpdate()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
