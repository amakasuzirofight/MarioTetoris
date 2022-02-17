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

        IPlayerAction[] playerActions;
        bool IsGround;
        void Start()
        {
            playerActions = new IPlayerAction[4];
            playerCore = new PlayerCore(playerScriptable, transform.position);
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
            //StateChangeByInput();
            //playerActions[(int)playerCore.playerState].StateUpdate(playerCore);

        }
        void StateChangeByInput()
        {
            playerCore.SetMoveDirection(charInputter.MoveInput());
            FieldNumber underField = komuTestSC.FieldNumberGeter(playerCore.playerPos, Difference.DOWN);
            if (underField == FieldNumber.GROUND || underField == FieldNumber.MINO)//�n�ʂɏ���Ă���ꍇ
            {
                if (charInputter.JumpInput())//�W�����v��������Ă����ꍇ
                {
                    if (playerCore.canJump)//�W�����v���łȂ��Ƃ�
                    {
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
                ChangeState(PlayerState.FallDown);
            }


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


