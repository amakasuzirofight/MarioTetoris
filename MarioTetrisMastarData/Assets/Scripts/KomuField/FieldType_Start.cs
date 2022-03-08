using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class FieldType_Start : FieldBase
    {
        StartStete state;

        List<string> contnt_1 = new List<string>();
        List<string> contnt_2 = new List<string>();
        List<string> contnt_3 = new List<string>();
        public override void OpenField()
        {
            base.OpenField();
        }

        public override void FieldCheck()
        {
            switch (state)
            {
                case StartStete.CONVERSATION_1:

                    break;
                case StartStete.MOVE_DEMO:

                    break;
                case StartStete.CONVERSATION_2:

                    break;
                case StartStete.MOVE_DEMO_ROBO:

                    break;
                case StartStete.CONVERSATION_3:

                    break;
                case StartStete.NONE:

                    break;
            }
        }

        public override void CloseField()
        {
            DestroyObjects();
        }

        public void Event()
        {

        }

        public enum StartStete
        {
            CONVERSATION_1,
            MOVE_DEMO,
            CONVERSATION_2,
            MOVE_DEMO_ROBO,
            CONVERSATION_3,
            NONE
        }
    }
}