using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMove
    {
        float _speed;
        float playerMaxHigh;
        float playerMaxRight;
        float playerMaxLeft;
        // Start is called before the first frame update
        public PlayerMove(PlayerCore core)
        {
            _speed = core.moveSpeed;
            //�v���C���[�͈̔�
            playerMaxHigh = core.playerHigh;
            playerMaxRight = core.playerWidth / 2;
            playerMaxLeft = -(core.playerWidth / 2);
        }
        public Vector3 PlayerWalkCulculate(Vector3 playerPos, float[,] FieldInfo)
        {
            //���݂̍��W��z��ɕϊ�
            Vector2 posToField = Test.TestField.PosToArray(playerPos);
            //Test.TestField.IsBetween(playerPos)
            //�n�ʂ𓥂�ł��邩
            if (IsFrontTouchGround(posToField,FieldInfo))
            {
                //����ł����ꍇ����

            }
            return playerPos;
        }
        //�����m�F
        bool IsFrontTouchGround(Vector2 arrayNum, float[,] FieldInfo)
        {
            //if (FieldInfo[arrayNum.x, arrayNum.y - 1])
            //{

            //}
            return true;
        }

    }

}
