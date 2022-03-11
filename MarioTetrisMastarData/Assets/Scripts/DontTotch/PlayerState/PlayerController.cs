using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Inputer;
namespace Player
{
    public class PlayerController : MonoBehaviour, ICoreGetter
    {
        [SerializeField] PlayerState state;
        [SerializeField] PlayerScriptable playerScriptable;
        [SerializeField] GameObject inputObj;
        [SerializeField] komuTestSC komuTestSC;
        ICharInputter charInputter;
        private PlayerMove playerMove;
        PlayerCore playerCore;
        PlayerInfo playerInfo;
        IPlayerAction[] playerActions;
        bool IsGround;
        void Start()
        {
            playerActions = new IPlayerAction[4];
            playerCore = new PlayerCore(playerScriptable, transform.position);
            //�Q�[�����̍\���̂̒l��ݒ�
            #region
            //playerInfo.moveSpeed = playerCore.moveSpeed;
            //playerInfo.jumpPower = playerCore.jumpPower;
            //playerInfo.jumpTime = playerCore.jumpTime;
            //playerInfo.playerPos = playerCore.playerPos;
            //playerInfo.moveSpeed = playerCore.moveSpeed;
            #endregion
            charInputter = inputObj.GetComponent<ICharInputter>();

            playerActions[0] = new PlayerStay();
            playerActions[1] = new PlayerWalk();
            playerActions[2] = new PlayerJump();
            playerActions[3] = new PlayerFallDown();

            for (int i = 0; i < playerActions.Length; i++)
            {
                playerActions[i].coreUpdateEvent += CoreUpdate;
                playerActions[i].changeStateEvent += ChangeState;
                playerActions[i].checkGround += FieldNumberGeter2D;
            }

        }

        // Update is called once per frame
        void Update()
        {
            //Debug.LogWarning(charInputter.MoveInput());
            StateChangeByInput();
            playerActions[(int)playerCore.playerState].StateUpdate(playerCore);
            //Debug.Log("State Is " + playerCore.playerState);

        }
        void StateChangeByInput()
        {
            playerCore.SetMoveDirection(charInputter.MoveInput());
            //FieldNumber underField = komuTestSC.FieldNumberGeter(playerCore.playerPos, Difference.DOWN);
            IsGround = JudgeGround(playerCore.playerPos, playerCore);
            Debug.Log(IsGround);
            //if (underField == FieldNumber.GROUND || underField == FieldNumber.MINO)//�n�ʂɏ���Ă���ꍇ
            if(IsGround)
            {
                //�W�����v�t���O������
                playerCore.SetCanJump(true);
                if (charInputter.JumpInput())//�W�����v��������Ă����ꍇ
                {
                    if (playerCore.canJump)//�W�����v���ł���Ƃ�
                    {
                        Debug.Log("�W�����v�Ɉڍs");
                        ChangeState(PlayerState.JumpUp);
                    }
                }
                else if (playerCore.movedirection != 0)//�W�����v���łȂ��ĉ��ړ����������ꍇ����
                {
                    playerCore.SetState(PlayerState.Walk);
                }
                else
                {
                    playerCore.SetState(PlayerState.Stay);
                }
            }
            else //�󒆂ɂ���ꍇ����������
            {
                Debug.Log("�󒆂ɂ��邽�ߗ���");
                if (playerCore.canJump == true)
                {
                    ChangeState(PlayerState.FallDown);

                }
            }
        }
        bool JudgeGround(Vector3 pos, PlayerCore core)
        {
            return
                (
                  FieldNumberGeter2D(PlayerCore.Cordrectiondifference(pos, Difference.LEFT, core), Difference.DOWN) == FieldNumber.GROUND
                  ||
                  FieldNumberGeter2D(PlayerCore.Cordrectiondifference(pos, Difference.LEFT, core), Difference.DOWN) == FieldNumber.MINO
                 )
                 &&
                 (
                    FieldNumberGeter2D(PlayerCore.Cordrectiondifference(pos, Difference.RIGHT, core), Difference.DOWN) == FieldNumber.GROUND
                    ||
                    FieldNumberGeter2D(PlayerCore.Cordrectiondifference(pos, Difference.RIGHT, core), Difference.DOWN) == FieldNumber.MINO
                 );
        }
        void CoreUpdate(PlayerCore core)
        {
            playerCore = core;
        }
        void ChangeState(PlayerState state)
        {
            playerCore.SetState(state);
        }
        public FieldNumber FieldNumberGeter2D(Vector3 pos, Difference difference = Difference.STAY)
        {
            return komuTestSC.FieldNumberGeter(pos, difference);
        }

        public PlayerCore GetCore()
        {
            return playerCore;
        }

    }
}


