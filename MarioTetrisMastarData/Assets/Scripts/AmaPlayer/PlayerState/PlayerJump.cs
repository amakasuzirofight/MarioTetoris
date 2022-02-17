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
            //�t���O������
           // core.SetCanJump(false);
          //  Jump(core);

        }
        void Jump(PlayerCore core)
        {
           
            //�J�E���g����萔�ȉ��̏ꍇ�W�����v����
            if(jumpTimeCount>maxJumpFlame)
            {
                core.SetCanJump(false); 
                //�ړ���̍��W�����
                anotherPosX = core.playerPos.x;
                anotherPosY = core.playerPos.y;
                //�W�����v���Ԃ��t���[���ɒu��������
                maxJumpFlame = (int)(core.jumpTime * 60);
                //�W�����v�J�E���g�i�߂�
                jumpTimeCount++;
                //�ړ��ʂ��A�j���[�V�����J�[�u�ŕ␳
                _movePiece = core.jumpCurve.Evaluate(1 / (maxJumpFlame / jumpTimeCount));
                //Y���̍��W�����
                anotherPosY += _movePiece * core.jumpPower;
                //���ړ������
                float direction = core.movedirection;
                //���ړ������݂���Ƃ�
                if (direction != 0)
                {
                    anotherPosX += core.moveSpeed * direction == 1 ? 1 : -1;
                }
                //�ړ����������W
                anotherPos = new Vector3(anotherPosX, anotherPosY, core.playerPos.z);

                //�ړ���̍��W�Ƀu���b�N���Ȃ����m�F
                Difference numberForward, numberUp;
                //�ړ���������ɂ���Ċm�F���ύX
                numberForward = direction == 1 ? Difference.RIGHT : Difference.LEFT;
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
                //�ړ���Ƀu���b�N���Ȃ����m�F
                fieldObjX = checkGround(anotherPos, numberForward);
                //�u���b�N���������ꍇ�A�ꏊ�̍X�V�͂��̂܂�
                //X��
                if (fieldObjX == FieldNumber.GROUND || fieldObjX == FieldNumber.MINO)
                {
                    anotherPosX = core.playerPos.x;
                }
                fieldObjY = checkGround(anotherPos, numberUp);
                //Y��
                if (fieldObjY == FieldNumber.GROUND || fieldObjY == FieldNumber.MINO)
                {
                    anotherPosY = core.playerPos.y;
                    //�����Ԃ����痎����
                    changeStateEvent(PlayerState.FallDown);
                }


                //�ړ����������W�m��
                anotherPos = new Vector3(anotherPosX, anotherPosY, core.playerPos.z);
                core.playerPos = anotherPos;
                coreUpdateEvent(core);
            }
            else
            {
                //�J�E���g�������ς��ɂȂ�����
                jumpTimeCount = 0;
                core.SetCanJump(true);
                coreUpdateEvent(core);
                changeStateEvent(PlayerState.FallDown);
            }
           
        }
    }

}
