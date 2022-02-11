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
        public PlayerState playerState;
        PlayerScriptable playerScriptable;
        public PlayerCore(PlayerScriptable scriptable, Vector3 startPos)
        {
            playerScriptable = scriptable;
            moveSpeed = playerScriptable.walkSpeed;
            jumpPower = playerScriptable.jumpPower;
            jumpTime = playerScriptable.jumpTime;
            playerPos = startPos;
            
        }
    }
    public enum PlayerState
    {
        Stay,Walk
    }
}
