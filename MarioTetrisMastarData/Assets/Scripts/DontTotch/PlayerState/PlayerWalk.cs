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
            //�X�s�[�h�擾
            _speed = core.moveSpeed;
            //��������
            _speed *= core.movedirection == 1 ? 1 : -1;
            //�ړ����������W�����
            float anotherX = _playerPosition.x += _speed;
            Vector3 anotherPos = new Vector3(anotherX, _playerPosition.y, _playerPosition.z);

            //-----�ړ���ɏ�Q�����Ȃ����m�F����---------
            //Debug.Log(anotherPos);
            //�m�F�������������擾
            Difference movedDifference = _speed == 1 ? Difference.RIGHT : Difference.LEFT;
            //Field�ɕ���
            if (checkGround(PlayerCore.Cordrectiondifference(anotherPos, movedDifference, core), movedDifference) == FieldNumber.GROUND
                ||
                checkGround(PlayerCore.Cordrectiondifference(anotherPos, movedDifference, core), movedDifference) == FieldNumber.MINO)
            {
                //��Q�������ɂ��ړ������Ȃ�
                coreUpdateEvent(core);
            }
            else
            {
                //�ړ����� ������������
                core.playerPos = anotherPos;
                coreUpdateEvent(core);
            }
            //if (checkGround(anotherPos, movedDifference) == FieldNumber.GROUND || checkGround(anotherPos, movedDifference) == FieldNumber.MINO)
            //{

            //}

        }


    }


}


