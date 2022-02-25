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

        //public void Start()
        //{
        //    WriteJson();
        //}

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
            StageClearFlg stage;
            stage.clearFlg_1 = false;
            stage.clearFlg_2 = false;
            stage.clearFlg_3 = false;
            stage.clearFlg_4 = false;
            stage.clearFlg_5 = false;
            stage.clearFlg_6 = false;
            stage.clearFlg_7 = false;
            stage.clearFlg_8 = false;
            stage.clearFlg_9 = false;
            stage.clearFlg_10 = false;

            StreamWriter writer = new StreamWriter(Application.dataPath + "/JsonData/" + stageFlgs.name + ".json");

            string jsonstr = JsonUtility.ToJson(stage);//受け取ったPlayerDataをJSONに変換
            writer.WriteLine(jsonstr);
            writer.Flush();//バッファをクリアする
            writer.Close();//ファイルをクローズする
        }
    }
}