using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] PlayerState state;
        [SerializeField] Test.TestField field;
        [SerializeField] PlayerScriptable playerScriptable;
        private PlayerMove playerMove;
        PlayerCore playerCore;

      
        void Start()
        {
            playerCore = new PlayerCore(playerScriptable, transform.position) ;
            playerMove = new PlayerMove(playerCore);
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


