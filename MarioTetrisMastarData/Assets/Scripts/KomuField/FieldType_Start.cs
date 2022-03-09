using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class FieldType_Start : FieldBase
    {
        StartStete state;

        [SerializeField] private List<string> contnt_1 = new List<string>();

        [SerializeField] private TextAsset csvData;
        public override void OpenField()
        {
            state = StartStete.CONVERSATION_1;
            Utility_.CsvToIntList(csvData);
            CreateCharacters();
            CreateField(Utility_.FieldData);
        }

        public override void FieldCheck()
        {
            switch (state)
            {
                case StartStete.CONVERSATION_1:
                    Utility_.MessageSetting(true);
                    Utility_.OpenMessage(contnt_1);
                    state = StartStete.NONE;
                    break;
                //case StartStete.MOVE_DEMO:

                //    break;
                //case StartStete.CONVERSATION_2:

                //    break;
                //case StartStete.MOVE_DEMO_ROBO:

                //    break;
                //case StartStete.CONVERSATION_3:

                //    break;
                case StartStete.NONE:
                    ChengerCheck();
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