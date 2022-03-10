using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Field
{
    public class FieldType_Start : FieldBase
    {
        StartStete state;

        [SerializeField,TextArea] private List<string> contnt_1 = new List<string>();

        [SerializeField,TextArea] private List<string> contnt_2 = new List<string>();

        [SerializeField, TextArea] private List<string> contet_3 = new List<string>();

        [SerializeField] private TextAsset csvData;

        List<GameObject> grounds = new List<GameObject>();

        bool eventflg = false;
        EventState state_e;
        public override void OpenField()
        {
            state = StartStete.EVENT_1;
            Utility_.CsvToIntList(csvData);
            CreateCharacters();
            grounds = CreateField(Utility_.FieldData);
            Utility_.playerObject.transform.position = FieldInfo.FieldInfoToVec(new FieldInfo(3,3));
            Utility_.robotObject.transform.position = FieldInfo.FieldInfoToVec(new FieldInfo(0,9));
        }

        public override void FieldCheck()
        {
            if (eventflg) return;
            switch (state)
            {
                case StartStete.EVENT_1:
                    state_e = EventState.STATE_1;
                    Action ac = Event;
                    Utility_.EventActiveate(ac);
                    eventflg = true;
                    break;
                case StartStete.CONVERSATION_1:
                    Utility_.MessageSetting(true);
                    Utility_.OpenMessage(contnt_1,"カイ");
                    state++;
                    break;
                case StartStete.EVENT_2:
                    state_e = EventState.STATE_1;
                    ac = Event_;
                    Utility_.EventActiveate(ac);
                    eventflg = true;
                    break;
                case StartStete.CONVERSATION_2:
                    Utility_.OpenMessage(contnt_2,"???");
                    state++;
                    break;
                case StartStete.CONVERSATION_3:
                    Utility_.OpenMessage(contet_3, "カイ");
                    state++;
                    break;
                case StartStete.NONE:
                    ChengerCheck();
                    break;
            }
        }

        public override void CloseField()
        {
            DestroyObjects(grounds);
        }

        public void Event()
        {
            switch (state_e)
            {
                case EventState.STATE_1:
                    if (Utility_.robotObject.transform.position.y >= -3) Utility_.robotObject.transform.position += new Vector3(0, -0.1f, 0);
                    else state_e++;
                    break;
                case EventState.STATE_2:
                    if (Utility_.playerObject.transform.position.x < Utility_.robotObject.transform.position.x - 3) Utility_.playerObject.transform.position += new Vector3(0.05f, 0, 0);
                    else state_e = EventState.STATE_5;
                    break;
                case EventState.STATE_3:
                    break;
                case EventState.STATE_4:
                    break;
                case EventState.STATE_5:
                    eventflg = false;
                    Utility_.EventEnd();
                    state++;
                    break;
            }

        }

        public void Event_()
        {
            switch (state_e)
            {
                case EventState.STATE_1:
                    if (Utility_.robotObject.transform.position.y <= 0) Utility_.robotObject.transform.position += new Vector3(0, 0.1f, 0);
                    else state_e = EventState.STATE_5;
                    break;
                case EventState.STATE_2:
                    break;
                case EventState.STATE_3:
                    break;
                case EventState.STATE_4:
                    break;
                case EventState.STATE_5:
                    eventflg = false;
                    Utility_.EventEnd();
                    state++;
                    break;
            }
        }

        //public IEnumerator Event()
        //{
        //    Utility_.EventActiveate();
        //    yield return new WaitForSeconds(1);
        //    state++;
        //    eventflg = false;
        //    Utility_.EventEnd();
        //}

        //public IEnumerator Event_()
        //{
        //    Utility_.EventActiveate();
        //    yield return new WaitForSeconds(1);
        //    state++;
        //    eventflg = false;
        //    Utility_.EventEnd();
        //}

        public enum EventState
        {
            STATE_1,
            STATE_2,
            STATE_3,
            STATE_4,
            STATE_5
        }

        public enum StartStete
        {
            EVENT_1,
            CONVERSATION_1,
            EVENT_2,
            CONVERSATION_2,
            CONVERSATION_3,
            NONE
        }
    }
}