using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStay :IPlayerAction

{
        public event ChangeStater changeStateEvent;
        public event CoreUpdate coreUpdateEvent;
        public event CheckGounder checkGround;

        public PlayerStay()
        {

        }
       

     
        public void StateUpdate(PlayerCore core)
        {
            Debug.Log("ステイ！！！！！");
        }
    }

}

