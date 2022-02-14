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
            //プレイヤーの範囲
            playerMaxHigh = core.playerHigh;
            playerMaxRight = core.playerWidth / 2;
            playerMaxLeft = -(core.playerWidth / 2);
        }
        public Vector3 PlayerWalkCulculate(Vector3 playerPos, float[,] FieldInfo)
        {
            //現在の座標を配列に変換
            Vector2 posToField = Test.TestField.PosToArray(playerPos);
            //Test.TestField.IsBetween(playerPos)
            //地面を踏んでいるか
            if (IsFrontTouchGround(posToField,FieldInfo))
            {
                //踏んでいた場合歩く

            }
            return playerPos;
        }
        //足元確認
        bool IsFrontTouchGround(Vector2 arrayNum, float[,] FieldInfo)
        {
            //if (FieldInfo[arrayNum.x, arrayNum.y - 1])
            //{

            //}
            return true;
        }

    }

}
