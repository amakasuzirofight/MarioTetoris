using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Player
{
    public delegate void ChangeStater(PlayerState state);
    public delegate void CoreUpdate(PlayerInfo info);

    interface IPlayerAction
    {
        event ChangeStater changeStateEvent;
        event CoreUpdate coreUpdateEvent;
        public void StateUpdate();
    }
}

