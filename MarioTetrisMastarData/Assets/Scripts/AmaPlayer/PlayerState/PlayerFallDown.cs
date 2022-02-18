using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Player
{
    class PlayerFallDown : IPlayerAction
    {
        public event ChangeStater changeStateEvent;
        public event CoreUpdate coreUpdateEvent;
        public event CheckGounder checkGround;

        float anotherPosX, anotherPosY;

        public PlayerFallDown()
        {

        }

        public void StateUpdate(PlayerCore core)
        {
            Fall(core);
        }
        void Fall(PlayerCore core)
        {
            //移動先の座標を作る
            anotherPosX = core.playerPos.x;
            anotherPosY = core.playerPos.y;

            //落下する座標を設定
            anotherPosY -= core.fallSpeed;

            float direction = core.movedirection;
            //横移動が存在するとき
            if (direction != 0)
            {
                anotherPosX += core.moveSpeed * (direction == 1 ? 1 : -1);
            }
            else
            {
                anotherPosX = core.playerPos.x;
            }
            //移動先の座標にブロックがないか確認
            Difference numberForward, numberDown;
            //移動する方向によって確認先を変更
            numberForward = default;
            //numberForward = direction == 1 ? Difference.RIGHT : Difference.LEFT;
            switch (direction)
            {
                case 0:
                    break;
                case 1:
                    numberForward = Difference.RIGHT;
                    break;
                case -1:
                    numberForward = Difference.LEFT;
                    break;
            }
            numberDown = Difference.DOWN;
            FieldNumber fieldObjX, fieldObjY;
            //移動先にブロックがないか確認
            fieldObjX = checkGround(new UnityEngine.Vector3(anotherPosX, anotherPosY, core.playerPos.z), numberForward);

            //ブロックがあった場合、場所の更新はそのまま
            //X軸
            if (fieldObjX == FieldNumber.GROUND || fieldObjX == FieldNumber.MINO)
            {
                Debug.LogError("X軸、障害物を確認");
                anotherPosX = core.playerPos.x;
            }
            fieldObjY = checkGround(new UnityEngine.Vector3(anotherPosX, anotherPosY, core.playerPos.z), numberDown);
            //Y軸
            if (fieldObjY == FieldNumber.GROUND || fieldObjY == FieldNumber.MINO)
            {
                Debug.LogError("Y軸、障害物を確認");
                //地面を確認したら地面に座標を合わせる
                anotherPosY = (float)Math.Floor(core.playerPos.y);
                changeStateEvent(PlayerState.Stay);
            }

            core.playerPos = new UnityEngine.Vector3(anotherPosX, anotherPosY, core.playerPos.z);
            coreUpdateEvent(core);
        }

   
    }
}
