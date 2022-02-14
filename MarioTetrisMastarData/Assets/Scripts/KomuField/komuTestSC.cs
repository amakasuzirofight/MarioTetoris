using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class komuTestSC : MonoBehaviour
{
    [SerializeField] private TextAsset stageData;
    List<string[]> stage_csv = new List<string[]>();
    StreamWriter writer;
    [SerializeField] private Text text;

    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        StringReader reader = new StringReader(stageData.text);

        // , �ŕ�������s���ǂݍ���
        // ���X�g�ɒǉ����Ă���
        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine(); // ��s���ǂݍ���
            stage_csv.Add(line.Split(',')); // , ��؂�Ń��X�g�ɒǉ�
        }

        // csvDatas[�s][��]���w�肵�Ēl�����R�Ɏ��o����
        for (int i = 0;i < stage_csv.Count;i++)
        {
            for (int j = 0;j < stage_csv[0].Length;j++)
            {
                Debug.Log(stage_csv[i][j]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("debug");
            writer = new StreamWriter("Assets/KomuFile/StageData.csv", append: false);
            for (int i = 0;i < stage_csv.Count;i++)
            {
                for (int j = 0; j < stage_csv[0].Length;j++)
                {
                    if (stage_csv[i][j] == "1")
                    {
                        stage_csv[i][j] = "2,";
                        writer.Write(stage_csv[i][j]);
                    }
                    else
                    {
                        writer.Write(stage_csv[i][j] + ",");
                    }
                }
                writer.WriteLine();
            }

            writer.Close();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("fal");

            stage_csv[0][0] = "2,";
            stage_csv[0][1] = "2,";
            stage_csv[0][2] = "2";

            //writer = new StreamWriter("Assets/KomuFile/StageData.csv", append: false);

            //for (int i = 1;i < stage_csv.Count;i++)
            //{
            //    for (int j = 0;j < stage_csv[0].Length;j++)
            //    {
            //        if (i == 0) stage_csv[i][j] = "2";
            //        writer.Write(stage_csv[i][j] + ",");
            //    }
            //    writer.WriteLine();
            //}
            //writer.Close();
        }

        count++;

        if (count == 30)
        {
            check();
            count = 0;
        }


        text.text = "";
        for (int i = 0;i < stage_csv.Count;i++)
        {
            for (int j = 0;j < stage_csv[0].Length;j++)
            {
                text.text += stage_csv[i][j] + ",";
            }
            text.text += "\n";
        }
    }

    public void check()
    {
        writer = new StreamWriter("Assets/KomuFile/StageData.csv", append: false);

        for (int i = stage_csv.Count - 1;i > 0;--i)
        {
            for (int j = stage_csv[0].Length - 1; j > 0;--j)
            {
                if (stage_csv[i][j] == "2," && i + 1 < stage_csv.Count - 1)
                {
                    stage_csv[i + 1][j] = "2,";
                    writer.Write(stage_csv[i + 1][j]);
                    stage_csv[i][j] = "0,";
                    writer.Write(stage_csv[i][j]);
                }
                else if (stage_csv[i][j] == "2" && i + 1 < stage_csv.Count - 1)
                {
                    stage_csv[i + 1][j] = "2";
                    writer.Write(stage_csv[i + 1][j]);
                    stage_csv[i][j] = "0";
                    writer.Write(stage_csv[i][j]);
                }
                else
                {
                    if (j == stage_csv[0].Length - 1)
                    {
                        writer.Write(stage_csv[i][j]);
                    }
                    else
                    {
                        writer.Write(stage_csv[i][j] + ",");
                    }
                }
            }

            writer.WriteLine();
        }

        //for (int i = 0 ; i < stage_csv.Count;i++)
        //{
        //    for(int j = 0; j < stage_csv[0].Length;j++)
        //    {
        //        if (stage_csv[i][j] == "2" && i + 1 <= stage_csv.Count)
        //        {
        //            stage_csv[i + 1][j] = "2,";
        //            writer.Write(stage_csv[i + 1][j]);
        //            stage_csv[i][j] = "0,";
        //            writer.Write(stage_csv[i][j]);
        //        }
        //        else if (i + 1 > stage_csv.Count)
        //        {
        //            stage_csv[i + 1][j] = "0,";
        //            writer.Write(stage_csv[i][j]);
        //        }
        //    }
        //}

        writer.Close();
    }
}
