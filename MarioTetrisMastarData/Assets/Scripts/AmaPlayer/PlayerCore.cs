using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class PlayerCore
    {
        public float moveSpeed { get; set; }
        public float jumpPower { get; set; }
        public float jumpTime { get; set; }
        public Vector3 playerPos { get; set; }
        public float playerHigh { get; set; }
        public float playerWidth { get; set; }

        public PlayerState playerState;
        PlayerScriptable playerScriptable;
        public PlayerCore(PlayerScriptable scriptable, Vector3 startPos)
        {
            //各種ステータスをスクリプタブルからとってくる
            playerScriptable = scriptable;
            moveSpeed = playerScriptable.walkSpeed;
            jumpPower = playerScriptable.jumpPower;
            jumpTime = playerScriptable.jumpTime;
            playerHigh = playerScriptable.playerHigh;
            playerWidth = playerScriptable.playerWidth;

            playerPos = startPos;
            
        }
    }
    public enum PlayerState
    {
        Stay,Walk
    }
}
