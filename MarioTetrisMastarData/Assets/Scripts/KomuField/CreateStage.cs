using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CreateStage : MonoBehaviour
{
    [SerializeField] private TextAsset saveFile;

    CreateStageData stageData;

    List<int[]> list = new List<int[]>();


    [SerializeField] private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        StreamReader reader = new StreamReader(Application.dataPath + "/CreateStage/" + saveFile.name + ".json"); //受け取ったパスのファイルを読み込む
        string datastr = reader.ReadToEnd();//ファイルの中身をすべて読み込む
        reader.Close();//ファイルを閉じる

        stageData = JsonUtility.FromJson<CreateStageData>(datastr);

        list = Utility_.StringListToIntList(stageData.datastr);

        for (int i = 0;i < list.Count;i++)
        {
            for (int j = 0;j < list[i].Length;j++)
            {
                if (list[i][j] != 0)
                {
                    if (list[i][j] < Utility_.BROCK_NUMBER_COUNT)
                    {
                        GameObject obj = Instantiate(Utility_.objectGeter[list[i][j]]);
                        obj.transform.position = FieldInfo.FieldInfoToVec(new FieldInfo(i, j));
                        // objects.Add(obj);
                    }
                    else if (list[i][j] < Utility_.BROCK_NUMBER_COUNT + Utility_.ENEMY_NUMBER_COUNT)
                    {
                        GameObject obj = Instantiate(Utility_.enemyGeter[list[i][j] - Utility_.BROCK_NUMBER_COUNT]);
                        obj.transform.position = FieldInfo.FieldInfoToVec(new FieldInfo(i, j));
                    }
                    else if (list[i][j] == Utility_.PLAYER_NUMBER)
                    {
                        Utility_.playerObject.transform.position = FieldInfo.FieldInfoToVec(new FieldInfo(i, j));
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(Utility_.playerObject.transform.position.x,Utility_.playerObject.transform.position.y,-10);
    }
}
