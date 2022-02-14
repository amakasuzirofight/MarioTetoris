using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class Brock
{
    public List<Vector2> number;
    public bool fallFlg;
    private int brockNumber;

    public Brock(int newNum)
    {
        number = new List<Vector2>();
        fallFlg = true;
        brockNumber = newNum;
    }

    public void brockNumSet(int num)
    {
        brockNumber = num;
    }

    public int brockNumGet()
    {
        return brockNumber;
    }

    public void stateChenge(bool fallState)
    {
        fallFlg = fallState;
        Debug.Log($"chenge => {fallFlg}");
    }

    public bool stateCheck()
    {
        return fallFlg;
    }
}

public class komuTestSC : MonoBehaviour
{
    [SerializeField] private TextAsset stageData;
    List<string[]> stage_csv = new List<string[]>();
    List<int[]> stage_csv_int = new List<int[]>();
    StreamWriter writer;
    [SerializeField] private Text text;

    List<Brock> activeBrock;

    int brNum = 2;

    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        activeBrock = new List<Brock>();
        Log();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Log();
        }

        if (count > 3000)
        {
            Debug.Log("処理終了");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("fal");

            stage_csv_int[0][0] = brNum;
            stage_csv_int[0][1] = brNum;
            stage_csv_int[0][2] = brNum;
            stage_csv_int[1][1] = brNum;

            activeBrock.Add(new Brock(brNum));
            activeBrock[activeBrock.Count - 1].number.Add(new Vector2(1, 1));
            activeBrock[activeBrock.Count - 1].number.Add(new Vector2(0,0));
            activeBrock[activeBrock.Count - 1].number.Add(new Vector2(0,1));
            activeBrock[activeBrock.Count - 1].number.Add(new Vector2(0,2));
            activeBrock[activeBrock.Count - 1].stateChenge(true);

            brNum++;
        }

        count++;

        if (count % 30 == 0)
        {
            check();
        }


        text.text = "";
        for (int i = 0;i < stage_csv_int.Count;i++)
        {
            for (int j = 0;j < stage_csv_int[0].Length;j++)
            {
                text.text += stage_csv_int[i][j] + ",";
            }
            text.text += "\n";
        }
    }

    private void LateUpdate()
    {
        for (int i = 0;i < activeBrock.Count;i++)
        {
            if (!activeBrock[i].stateCheck())
            {
                activeBrock.RemoveAt(i);
                i--;
                return;
            }
        }
    }

    public void Log()
    {
        stage_csv = new List<string[]>();
        StringReader reader = new StringReader(stageData.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            stage_csv.Add(line.Split(',')); // , 区切りでリストに追加
        }

        for (int i = 0;i < stage_csv.Count;i++)
        {
            stage_csv_int.Add(new int[stage_csv[i].Length]);
            for (int j = 0;j < stage_csv[i].Length;j++)
            {
                stage_csv_int[i][j] = Convert.ToInt32(stage_csv[i][j]);
            }
        }

        // csvDatas[行][列]を指定して値を自由に取り出せる
        for (int i = 0; i < stage_csv.Count; i++)
        {
            for (int j = 0; j < stage_csv[0].Length; j++)
            {
                Debug.Log("string = " + stage_csv[i][j]);
                Debug.Log("int    = " + stage_csv_int[i][j]);
            }
        }
    }

    public void check()
    {
        using (writer = new StreamWriter("Assets/KomuFile/StageData.csv", append: false))
        {
            // 確認
            for(int i = 0;i < activeBrock.Count;i++)
            {
                if (!activeBrock[i].stateCheck()) return;
                for (int j = 0; j < activeBrock[i].number.Count; j++)
                {
                    if (activeBrock[i].number[j].x >= stage_csv_int.Count - 1) // 最下層であるなら落下防止
                    {
                        Debug.Log("先がないため移動を中止します");
                        activeBrock[i].stateChenge(false);
                        break;
                    }
                    else if (activeBrock[i].brockNumGet() != stage_csv_int[(int)activeBrock[i].number[j].x + 1][(int)activeBrock[i].number[j].y]
                             && stage_csv_int[(int)activeBrock[i].number[j].x + 1][(int)activeBrock[i].number[j].y] != 0) // 移動先が違うブロックであるなら落下防止
                    {
                        activeBrock[i].stateChenge(false);
                        Debug.Log("先にブロックを確認したため移動を中止します");
                        Debug.Log(activeBrock[i].stateCheck());
                        break;
                    }
                    //else // 通常落下
                    //{
                    //    stage_csv_int[(int)activeBrock[i].number[j].x][(int)activeBrock[i].number[j].y] = 0;
                    //    activeBrock[i].number[j] += new Vector2(1, 0);
                    //    stage_csv_int[(int)activeBrock[i].number[j].x][(int)activeBrock[i].number[j].y] = activeBrock[i].brockNumGet();
                    //}
                }

                if (activeBrock[i].stateCheck()) Fall(i);
            }

            // 記述
            for (int i = 0;i < stage_csv_int.Count;i++)
            {
                for (int j = 0;j < stage_csv_int[i].Length;j++)
                {
                    writer.Write(stage_csv_int[i][j].ToString());
                    if (j < stage_csv_int[i].Length - 1) writer.Write(",");
                }

                writer.WriteLine();
            }
        }
    }

    public void Fall(int i)
    {
        for (int j = 0; j < activeBrock[i].number.Count; j++)
        {
            stage_csv_int[(int)activeBrock[i].number[j].x][(int)activeBrock[i].number[j].y] = 0;
            activeBrock[i].number[j] += new Vector2(1, 0);
            stage_csv_int[(int)activeBrock[i].number[j].x][(int)activeBrock[i].number[j].y] = activeBrock[i].brockNumGet();
        }
    }
}
