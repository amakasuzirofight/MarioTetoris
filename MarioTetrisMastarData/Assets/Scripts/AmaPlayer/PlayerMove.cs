using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMove
    {
        float _speed;
        // Start is called before the first frame update
        public PlayerMove(PlayerCore core)
        {
            _speed =  core.moveSpeed;
        }
        public Vector3 PlayerMoveCulculate(Vector3 playerPos, float[,] FieldInfo)
        {
            //è’ìÀÇµÇƒÇ¢ÇÈÇ©
            if(IsFrontTouchGround())
            {
                //åªç›ÇÃç¿ïWÇîzóÒÇ…ïœä∑

            }
            return playerPos;
        }
        bool IsFrontTouchGround ()
        {

            return true;
        }

    }

}
