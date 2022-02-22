using Inputer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputer;
namespace Player
{
    public class PlayerWalk : IPlayerAction
    {
        public event ChangeStater changeStateEvent;
        public event CoreUpdate coreUpdateEvent;
        public event CheckGounder checkGround;

        float _speed;
        Vector3 _playerPosition;
        //public PlayerWalk(float speed, Vector3 pos)
        //{
        //    _speed = speed;
        //    _playerPosition = pos;
        //}



        public void StateUpdate(PlayerCore core)
        {
            _playerPosition = core.playerPos;
            Walk(core);
        }
        void Walk(PlayerCore core)
        {
            //スピード取得
            _speed = core.moveSpeed;
            //向き調整
            _speed *= core.movedirection == 1 ? 1 : -1;
            //移動したい座標を作る
            float anotherX = _playerPosition.x += _speed;
            Vector3 anotherPos = new Vector3(anotherX, _playerPosition.y, _playerPosition.z);

            //-----移動先に障害物がないか確認する---------
            //Debug.Log(anotherPos);
            //確認したい方向を取得
            Difference movedDifference = _speed == 1 ? Difference.RIGHT : Difference.LEFT;
            //Fieldに聞く
            if (checkGround(PlayerCore.Cordrectiondifference(anotherPos, movedDifference, core), movedDifference) == FieldNumber.GROUND
                ||
                checkGround(PlayerCore.Cordrectiondifference(anotherPos, movedDifference, core), movedDifference) == FieldNumber.MINO)
            {
                //障害物発見につき移動させない
                coreUpdateEvent(core);
            }
            else
            {
                //移動処理 情報を書き換え
                core.playerPos = anotherPos;
                coreUpdateEvent(core);
            }
            //if (checkGround(anotherPos, movedDifference) == FieldNumber.GROUND || checkGround(anotherPos, movedDifference) == FieldNumber.MINO)
            //{

            //}

        }


    }


}


