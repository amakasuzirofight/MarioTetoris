using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace Field
{
    public class FieldManager : MonoBehaviour
    {
        [SerializeField] private FieldBase baseScene;
        [SerializeField] private GameObject mainCamara;
        FieldBase nowField;
        GameObject activeSceneObject;
        FieldState state;

        List<GameObject> enemys = new List<GameObject>();
        List<Connector.IEnemyUpdateSendable> enemyUpdates = new List<Connector.IEnemyUpdateSendable>();
        Mario.IPlayerUpdate coreUpdate;
        Robot.IRobotUpdate robotUpdate;
        Inputer.ISelectInputUpdate InputUpdate;

        [SerializeField] private TextAsset saveData;
        [SerializeField] private FieldBase defaultScene;

        private void Start()
        {
            Utility_.MessageSetting(false);
            state = FieldState.NORMAL;
            if (flgChecker()) activeSceneObject = Instantiate(defaultScene.gameObject);
            else activeSceneObject = Instantiate(baseScene.gameObject);
            nowField = activeSceneObject.GetComponent<FieldBase>();
            nowField.fieldcomplete = FieldChenge;
            nowField.OpenField();
            enemys = nowField.enemys;
            coreUpdate = Utility_.playerObject.GetComponent<Mario.IPlayerUpdate>();
            robotUpdate = Utility_.robotObject.GetComponent<Robot.IRobotUpdate>();
            InputUpdate = GameObject.Find("Input 1").GetComponent<Inputer.ISelectInputUpdate>();
            for (int i = 0;i < enemys.Count;i++)
            {
                enemyUpdates.Add(enemys[i].GetComponent<Connector.IEnemyUpdateSendable>());
                if (enemyUpdates[i] == null) enemyUpdates[i] = enemys[i].GetComponentInChildren<Connector.IEnemyUpdateSendable>();
            }
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
                    nowField.FieldCheck();
                    coreUpdate.MarioUpdate();
                    robotUpdate.RobotUpdate();
                    InputUpdate.SelectUpdate();
                    for (int i = 0;i < enemyUpdates.Count;i++)
                    {
                        enemyUpdates[i].EnemyUpdate();
                    }
                    break;
                case FieldState.CONVERSATION:
                    for (int i = 0; i < enemyUpdates.Count; i++)
                    {
                        enemyUpdates[i].EnemyVelocityDefault();
                    }
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Utility_.MessageWriter();
                    }
                    break;
                case FieldState.EVENT:
                    Utility_.EventExecution();
                    for (int i = 0; i < enemyUpdates.Count; i++)
                    {
                        enemyUpdates[i].EnemyVelocityDefault();
                    }
                    break;
                default:
                    break;
            }
        }

        private void FixedUpdate()
        {
            if (Utility_.GameState == FieldState.NORMAL)
            {
                coreUpdate.MarioFixedUpdate();
                robotUpdate.RobotFixedUpdate();
            }
        }

        public void FieldChenge()
        {
            Debug.Log("Close");
            nowField.CloseField();
            FieldBase next = nowField.activeChenger.nextField;
            Destroy(activeSceneObject);
            activeSceneObject = Instantiate(next.gameObject);
            nowField = activeSceneObject.GetComponent<FieldBase>();
            nowField.fieldcomplete = FieldChenge;
            nowField.OpenField();
            enemyUpdates = new List<Connector.IEnemyUpdateSendable>();
            Utility_.robotObject.transform.position = new Vector3(Utility_.playerObject.transform.position.x,Utility_.playerObject.transform.position.y + 4);
            enemys = nowField.enemys;
            for (int i = 0; i < enemys.Count; i++)
            {
                enemyUpdates.Add(enemys[i].GetComponent<Connector.IEnemyUpdateSendable>());
                if (enemyUpdates[i] == null) enemyUpdates[i] = enemys[i].GetComponentInChildren<Connector.IEnemyUpdateSendable>();
            }
        }

        public bool flgChecker()
        {
            StreamReader reader = new StreamReader(Application.dataPath + "/JsonData/" + saveData.name + ".json");
            string data = reader.ReadToEnd();
            reader.Close();

            StageClearFlg clearFlg = JsonUtility.FromJson<StageClearFlg>(saveData.text);

            List<bool> flgs = new List<bool>();
            flgs.Add(false);
            flgs.Add(clearFlg.clearFlg_1);
            flgs.Add(clearFlg.clearFlg_2);
            flgs.Add(clearFlg.clearFlg_3);
            flgs.Add(clearFlg.clearFlg_4);
            flgs.Add(clearFlg.clearFlg_5);
            flgs.Add(clearFlg.clearFlg_6);
            flgs.Add(clearFlg.clearFlg_7);
            flgs.Add(clearFlg.clearFlg_8);
            flgs.Add(clearFlg.clearFlg_9);
            flgs.Add(clearFlg.clearFlg_10);

            for (int i = 0;i < flgs.Count;i++)
            {
                if (flgs[i]) return true;
            }

            return false;
        }
    }

}