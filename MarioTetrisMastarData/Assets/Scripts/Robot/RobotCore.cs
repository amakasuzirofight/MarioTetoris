using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputer;

namespace Robot
{
    public class RobotCore : MonoBehaviour
    {
        [SerializeField] float moveSpeed;
        [SerializeField] GameObject inputObj;
        [SerializeField] RobotMove robotMove;


        IRobotInput robotInput;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void FixedUpdate()
        {
            WalkJudde();
        }
        void WalkJudde()
        {
            if (robotInput.MovePower() != 0)
            {
                robotMove.ExecutionRoboWalk(moveSpeed);
            }
        }

    }



}
