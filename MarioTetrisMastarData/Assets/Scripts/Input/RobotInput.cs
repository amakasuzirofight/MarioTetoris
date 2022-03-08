using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputer
{
    public class RobotInput : MonoBehaviour, IRobotInput
    {
        float movePower;
        void Awake()
        {
            Utility.Locator<IRobotInput>.Bind(this);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movePower = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                movePower = -1;
            }
            else
            {
                movePower = 0;
            }
        }
        float IRobotInput.MovePower() 
        {
            return movePower;
        }
    }

}
