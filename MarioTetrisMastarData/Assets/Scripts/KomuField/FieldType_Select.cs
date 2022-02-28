using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace Field
{
    public class FieldType_Select : FieldBase
    {
        [SerializeField] private TextAsset field_csv;
        [SerializeField] private TextAsset stageFlgs;
        [SerializeField] private Stage stage;
        private List<GameObject> objects;

        public override void OpenField()
        {
            ReadJson();
            activeChenger = default;
            Utility_.CsvToIntList(field_csv);
            CreateCharacters();
            objects = CreateField(Utility_.FieldData);
        }

        public override void FieldCheck()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                WriteJson();
            }

            ChengerCheck();
        }

        public override void CloseField()
        {
            DestroyObjects(objects);
            objects = new List<GameObject>();
        }

        private void ReadJson()
        {
            StreamReader reader = new StreamReader(Application.dataPath + "/JsonData/" + stageFlgs.name + ".json");
            string data = reader.ReadToEnd();
            reader.Close();

            StageClearFlg clearFlg = JsonUtility.FromJson<StageClearFlg>(stageFlgs.text);

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

            Utility_.StageFlgSeter(flgs,stage);
        }

        private void WriteJson()
        {
            Debug.Log("save");

            StageClearFlg stage;
            stage.clearFlg_1 = Utility_.stageFlgList[1];
            stage.clearFlg_2 = Utility_.stageFlgList[2];
            stage.clearFlg_3 = Utility_.stageFlgList[3];
            stage.clearFlg_4 = Utility_.stageFlgList[4];
            stage.clearFlg_5 = Utility_.stageFlgList[5];
            stage.clearFlg_6 = Utility_.stageFlgList[6];
            stage.clearFlg_7 = Utility_.stageFlgList[7];
            stage.clearFlg_8 = Utility_.stageFlgList[8];
            stage.clearFlg_9 = Utility_.stageFlgList[9];
            stage.clearFlg_10 = Utility_.stageFlgList[10];

            StreamWriter writer = new StreamWriter(Application.dataPath + "/JsonData/" + stageFlgs.name + ".json");

            string jsonstr = JsonUtility.ToJson(stage);// JSONに変換
            writer.WriteLine(jsonstr);
            writer.Flush();//バッファをクリアする
            writer.Close();//ファイルをクローズする
        }
    }
}