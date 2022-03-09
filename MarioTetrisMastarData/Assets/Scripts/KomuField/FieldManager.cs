using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Field
{
    public class FieldManager : MonoBehaviour
    {
        [SerializeField] private FieldBase baseScene;
        [SerializeField] private GameObject mainCamara;
        FieldBase nowField;
        GameObject activeSceneObject;
        FieldState state;
        [SerializeField] private List<string> debugMs;

        private void Start()
        {
            Utility_.MessageSetting(false);
            state = FieldState.NORMAL;
            activeSceneObject = Instantiate(baseScene.gameObject);
            nowField = baseScene;
            nowField.fieldcomplete = FieldChenge;
            nowField.OpenField();
        }

        void Awake()
        {
            //state = FieldState.NORMAL;
            //activeSceneObject = Instantiate(baseScene.gameObject);
            //nowField = baseScene;
            //nowField.fieldcomplete = FieldChenge;
            //nowField.OpenField();
        }

        // Update is called once per frame
        void Update()
        {
            mainCamara.transform.position = 
                new Vector3(Mathf.Clamp(Utility_.playerObject.transform.position.x,10,Utility_.FieldData[0].Length - 10),
                            Utility_.playerObject.transform.position.y,
                            -10);

            switch (Utility_.GameState)
            {
                case FieldState.NORMAL:
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        Utility_.OpenMessage(debugMs);
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Action a = DebugEvent;
                        Utility_.EventActiveate(a);
                    }

                    nowField.FieldCheck();

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        List<FieldInfo> fields = new List<FieldInfo>();
                        fields.Add(new FieldInfo(4, 0));
                        fields.Add(new FieldInfo(3, 0));
                        fields.Add(new FieldInfo(2, 0));
                        fields.Add(new FieldInfo(1, 0));

                        nowField.CreateBrock(fields);
                    }
                    break;
                case FieldState.CONVERSATION:
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        Utility_.MessageWriter();
                    }
                    break;
                case FieldState.EVENT:
                    break;
                default:
                    break;
            }

        }

        public void FieldChenge()
        {
            Debug.Log("Close");
            nowField.CloseField();
            FieldBase next = nowField.activeChenger.nextField;
            Destroy(activeSceneObject);
            nowField = next;
            activeSceneObject = Instantiate(nowField.gameObject);
            nowField.fieldcomplete = FieldChenge;
            nowField.OpenField();
        }

        public void DebugEvent()
        {
            Instantiate(Utility_.enemyGeter[0]);

            StartCoroutine(eve());
        }

        IEnumerator eve ()
        {
             yield return new WaitForSeconds(10);

            Utility_.EventEnd();
        }
    }

}