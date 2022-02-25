using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


[Serializable]
public struct PlayerData
{
    public string name;
    public int hp;
    public int attack;
    public int defense;
}

[Serializable]
public struct Data
{
    public string csv_baseData;

    public Data(string create)
    {
        csv_baseData = create;
    }
}

public class komuTestSC_JSON : MonoBehaviour
{
    [SerializeField] private TextAsset csv;
    string datapath;
    List<int[]> fieldPath = new List<int[]>();
    private void Awake()
    {
        //初めに保存先を計算する　Application.dataPathで今開いているUnityプロジェクトのAssetsフォルダ直下を指定して、後ろに保存名を書く
        datapath = Application.dataPath + "/TestJson.json";

        for (int i = 0;i < 9;i++)
        {
            fieldPath.Add(new int[9]);
            for (int j = 0;j < 9;j++)
            {
                fieldPath[i][j] = i;
                Debug.Log(fieldPath[i][j]);
            }
        }
    }

    void Start()
    {
        //PlayerData player1 = new PlayerData();
        //player1.name = "タカシ";
        //player1.hp = 350;
        //player1.attack = 20;
        //player1.defense = 10;
        //SaveTest(player1);
        SaveTest();
    }

    //セーブのメソッド
    public void SaveTest(PlayerData player)
    {
        string jsonstr = JsonUtility.ToJson(player);//受け取ったPlayerDataをJSONに変換
        StreamWriter writer = new StreamWriter(datapath, false);//初めに指定したデータの保存先を開く
        writer.WriteLine(jsonstr);//JSONデータを書き込み
        writer.Flush();//バッファをクリアする
        writer.Close();//ファイルをクローズする
    }

    public void SaveTest()
    {
        StreamWriter writer = new StreamWriter(datapath, false);
        StringReader reader = new StringReader(csv.text);

        string data = JsonUtility.ToJson(new Data(csv.text));
        writer.WriteLine(data);

        writer.Flush();
        writer.Close();
    }
}
