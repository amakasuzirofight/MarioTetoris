using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputer;
using System.Reflection;

namespace Robot
{
    public class RobotCore : MonoBehaviour,IGetPositionToInfo,IRobotUpdate

    {
        [SerializeField] float moveSpeed;
        [SerializeField] GameObject inputObj;
        [SerializeField] RobotMove robotMove;
        [SerializeField] float MaxRoboPositionX;
        [SerializeField] float MinRoboPositionX;

        IRobotInput robotInput;
        private void Awake()
        {
            Utility.Locator<IGetPositionToInfo>.Bind(this);
        }
        // Start is called before the first frame update
        void Start()
        {
            robotInput = Utility.Locator<IRobotInput>.GetT();
        }

        // Update is called once per frame
          
        public void RobotUpdate()
        {
        }

        public void RobotFixedUpdate()
        {
            WalkJudde();
        }
        private void FixedUpdate()
        {
            //WalkJudde();

        }
        void WalkJudde()
        {
            if (robotInput.MovePower() != 0)
            {
            }
            robotMove.ExecutionRoboWalk(moveSpeed * robotInput.MovePower());

        }

        public FieldInfo GetPositionToInfo()
        {
            //あとでオフセット直す
            return FieldInfo.VecToFieldInfo(transform.position);
        }

   
    }



}
