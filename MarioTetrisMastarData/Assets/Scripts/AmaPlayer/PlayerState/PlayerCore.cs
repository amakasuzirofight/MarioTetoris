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
        public float fallSpeed { get; set; }
        public Vector3 playerPos { get; set; }
        public float playerHigh { get; set; }
        public float playerWidth { get; set; }
        public AnimationCurve jumpCurve { get; private set; }

        public bool canJump { get; private set; }
        public float movedirection { get;private set; }
        public PlayerState playerState { get; private set; }
        private PlayerScriptable playerScriptable;
        public PlayerCore(PlayerScriptable scriptable, Vector3 startPos)
        {
            //各種ステータスをスクリプタブルからとってくる
            playerScriptable = scriptable;
            moveSpeed = playerScriptable.walkSpeed;
            jumpPower = playerScriptable.jumpPower;
            jumpTime = playerScriptable.jumpTime;
            playerHigh = playerScriptable.playerHigh;
            playerWidth = playerScriptable.playerWidth;
            jumpCurve = playerScriptable.jumpCurve;
            fallSpeed = playerScriptable.fallSpeed;
            canJump = true;

            playerPos = startPos;
        }
        public void SetState(PlayerState state)
        {
            playerState = state;
        }
        public void SetCanJump(bool isJ)
        {
            canJump = isJ;
        }
        public void SetMoveDirection(float power)
        {
            movedirection = power;
        }
    }
    public enum PlayerState
    {
        Stay=0,Walk=1,JumpUp=2,FallDown=3
    }
    public struct PlayerInfo
    {
        public float moveSpeed ;
        public float jumpPower ;
        public float jumpTime ;
        public Vector3 playerPos;
        public float playerHigh ; 
        public float playerWidth;
        public bool isJump;

    }

}
