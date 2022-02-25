using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class FieldManager : MonoBehaviour
    {
        [SerializeField] private FieldBase baseScene;
        FieldBase nowField;
        GameObject activeSceneObject;
        FieldState state;

        private void Start()
        {
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
            switch (state)
            {
                case FieldState.NORMAL:
                    nowField.FieldCheck();

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        List<FieldInfo> fields = new List<FieldInfo>();
                        fields.Add(new FieldInfo(0, 0));
                        fields.Add(new FieldInfo(0, 1));
                        fields.Add(new FieldInfo(0, 2));
                        fields.Add(new FieldInfo(0, 3));

                        nowField.CreateBrock(fields);
                    }
                    break;
                case FieldState.CONVERSATION:
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
    }

}