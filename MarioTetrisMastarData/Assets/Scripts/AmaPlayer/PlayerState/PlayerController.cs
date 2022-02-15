using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] PlayerState state;
        [SerializeField] Test.TestField field;
        [SerializeField] PlayerScriptable playerScriptable;
        private PlayerMove playerMove;
        PlayerCore playerCore;

        IPlayerAction[] playerActions;
        void Start()
        {
            playerCore = new PlayerCore(playerScriptable, transform.position) ;
            playerMove = new PlayerMove(playerCore);
          
                playerActions[0] = Locator<IPlayerAction>.GetT(0);
                playerActions[1] = Locator<IPlayerAction>.GetT(1);
                playerActions[2] = Locator<IPlayerAction>.GetT(2);
        }

        // Update is called once per frame
        void Update()
        {
            switch (playerCore.playerState)
            {
                case PlayerState.Stay:
                    
                    break;
                case PlayerState.Walk:
                    playerMove.PlayerWalkCulculate(playerCore.playerPos,field.testFieldArray);
                    break;
            }
            
        }
    }
}


