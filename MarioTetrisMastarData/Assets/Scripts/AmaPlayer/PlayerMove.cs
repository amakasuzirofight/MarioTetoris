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
            //�Փ˂��Ă��邩
            if(IsFrontTouchGround())
            {
                //���݂̍��W��z��ɕϊ�

            }
            return playerPos;
        }
        bool IsFrontTouchGround ()
        {

            return true;
        }

    }

}
