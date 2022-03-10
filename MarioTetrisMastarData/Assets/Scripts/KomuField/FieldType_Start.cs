using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class FieldType_Start : FieldBase
    {
        StartStete state;

        [SerializeField,TextArea] private List<string> contnt_1 = new List<string>();

        [SerializeField,TextArea] private List<string> contnt_2 = new List<string>();

        [SerializeField] private TextAsset csvData;

        List<GameObject> grounds = new List<GameObject>();

        bool eventflg = false;
        public override void OpenField()
        {
            state = StartStete.CONVERSATION_1;
            Utility_.CsvToIntList(csvData);
            CreateCharacters();
            grounds = CreateField(Utility_.FieldData);
        }

        public override void FieldCheck()
        {
            if (eventflg) return;
            switch (state)
            {
                case StartStete.EVENT_1:
                    StartCoroutine(Event());
                    eventflg = true;
                    break;
                case StartStete.CONVERSATION_1:
                    Utility_.MessageSetting(true);
                    Utility_.OpenMessage(contnt_1,"ÉJÉC");
                    state++;
                    break;
                case StartStete.EVENT_2:
                    StartCoroutine(Event_());
                    eventflg = true;
                    break;
                case StartStete.CONVERSATION_2:
                    Utility_.OpenMessage(contnt_2,"???");
                    state = StartStete.NONE;
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

        public IEnumerator Event()
        {
            yield return new WaitForSeconds(1);
            state++;
            eventflg = false;
        }

        public IEnumerator Event_()
        {
            yield return new WaitForSeconds(1);
            state++;
            eventflg = false;
        }

        public enum StartStete
        {
            EVENT_1,
            CONVERSATION_1,
            EVENT_2,
            CONVERSATION_2,
            NONE
        }
    }
}