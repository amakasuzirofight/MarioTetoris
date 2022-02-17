using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Player
{
    public delegate void ChangeStater(PlayerState state);
    public delegate void CoreUpdate(PlayerCore core);
    public delegate FieldNumber CheckGounder(Vector3 pos,Difference diffrence);

    interface IPlayerAction
    {
        event ChangeStater changeStateEvent;
        event CoreUpdate coreUpdateEvent;
        event CheckGounder checkGround;
        public void StateUpdate(PlayerCore core);
        //public void CheckField(komuTestSC komuTest);
    }
}

