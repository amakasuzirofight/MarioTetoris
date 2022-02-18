using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerJump : IPlayerAction
    {
        public event ChangeStater changeStateEvent;
        public event CoreUpdate coreUpdateEvent;
        public event CheckGounder checkGround;

        float _movePiece;
        int jumpTimeCount;
        int maxJumpFlame;
        Vector3 anotherPos;
        float anotherPosX, anotherPosY;
        public void StateUpdate(PlayerCore core)
        {
            //フラグを入れる
            //core.SetCanJump(false);
            //ジャンプ時間をフレームに置き換える
            maxJumpFlame = (int)(core.jumpTime * 60);
            Debug.Log(maxJumpFlame);
            Jump(core);
            //Jump2D(core);
        }
        void Jump(PlayerCore core)
        {

            //カウントが一定数以下の場合ジャンプ処理
            if (jumpTimeCount < maxJumpFlame)
            {
                core.SetCanJump(false);
                //移動先の座標を作る
                anotherPosX = core.playerPos.x;
                anotherPosY = core.playerPos.y;
                //ジャンプカウント進める
                jumpTimeCount++;
                //移動量をアニメーションカーブで補正
                _movePiece = core.jumpCurve.Evaluate(1 / (maxJumpFlame / jumpTimeCount));
                //Y軸の座標を作る
                anotherPosY += core.jumpPower + (_movePiece * core.jumpPower);
                //横移動も作る
                float direction = core.movedirection;
                //横移動が存在するとき
                if (direction != 0)
                {
                    anotherPosX += core.moveSpeed * (direction == 1 ? 1 : -1);
                }
                //移動したい座標
                anotherPos = new Vector3(anotherPosX, anotherPosY, core.playerPos.z);

                //移動先の座標にブロックがないか確認
                Difference numberForward, numberUp;
                numberForward = default;
                //移動する方向によって確認先を変更
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
                numberUp = Difference.UP;

                FieldNumber fieldObjX, fieldObjY;
                //移動先にブロックがないか確認
                fieldObjX = checkGround(anotherPos, numberForward);
                //ブロックがあった場合、場所の更新はそのまま
                //X軸
                if (fieldObjX == FieldNumber.GROUND || fieldObjX == FieldNumber.MINO)
                {
                    anotherPosX = core.playerPos.x;
                }
                fieldObjY = checkGround(anotherPos, numberUp);
                //Y軸
                if (fieldObjY == FieldNumber.GROUND || fieldObjY == FieldNumber.MINO)
                {
                    anotherPosY = core.playerPos.y;
                    //頭をぶつけたら落ちる
                    Debug.Log("頭ぶつけそうだから落下");
                    changeStateEvent(PlayerState.FallDown);
                    jumpTimeCount = 0;
                }

                //移動したい座標確定
                Debug.Log(anotherPosY);
                anotherPos = new Vector3(anotherPosX, anotherPosY, core.playerPos.z);
                core.playerPos = anotherPos;
                coreUpdateEvent(core);
            }
            else
            {
                Debug.Log("カウントがいっぱいだから落下");
                //カウントがいっぱいになった時
                jumpTimeCount = 0;
                core.SetCanJump(true);
                coreUpdateEvent(core);
                changeStateEvent(PlayerState.FallDown);
            }

        }

        void Jump2D(PlayerCore core)
        {
            jumpTimeCount++;
            if (jumpTimeCount < maxJumpFlame)
            {
                //まだ上昇中の場合
                Debug.Log("ジャンプ");
            }
            else
            {
                Debug.Log("カウントがいっぱいだから落下");
                jumpTimeCount = 0;
                core.SetCanJump(true);
                changeStateEvent(PlayerState.FallDown);

            }
            coreUpdateEvent(core);
        }
    }

}
