using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class komuTestSC : MonoBehaviour
{
    [SerializeField] private TextAsset stageData;
    List<string[]> stage_csv = new List<string[]>();
    List<int[]> stage_csv_int = new List<int[]>();
    StreamWriter writer;
    [SerializeField] private Text text;
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject mino;
    GameObject obj;

    List<Brock> activeBrock;

    int brNum = 10;

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
            Debug.Log(FieldNumberGeter(new Vector3(2,-7,0)));
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
            activeBrock[activeBrock.Count - 1].csv_pos.Add(new FieldInfo(1,1));
            activeBrock[activeBrock.Count - 1].csv_pos.Add(new FieldInfo(0,0));
            activeBrock[activeBrock.Count - 1].csv_pos.Add(new FieldInfo(0,1));
            activeBrock[activeBrock.Count - 1].csv_pos.Add(new FieldInfo(0,2));
            activeBrock[activeBrock.Count - 1].stateChenge(true);

            for (int i = 0; i < activeBrock[0].csv_pos.Count;i++)
            {
                activeBrock[activeBrock.Count - 1].minos.Add(Instantiate(mino));
            }

            brNum++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("fal");

            stage_csv_int[0][7] = brNum;
            stage_csv_int[0][8] = brNum;
            stage_csv_int[0][9] = brNum;
            stage_csv_int[0][10] = brNum;

            activeBrock.Add(new Brock(brNum));
            activeBrock[activeBrock.Count - 1].csv_pos.Add(new FieldInfo(0, 7));
            activeBrock[activeBrock.Count - 1].csv_pos.Add(new FieldInfo(0, 8));
            activeBrock[activeBrock.Count - 1].csv_pos.Add(new FieldInfo(0, 9));
            activeBrock[activeBrock.Count - 1].csv_pos.Add(new FieldInfo(0, 10));
            activeBrock[activeBrock.Count - 1].stateChenge(true);

            for (int i = 0; i < activeBrock[0].csv_pos.Count; i++)
            {
                activeBrock[activeBrock.Count - 1].minos.Add(Instantiate(mino));
            }

            brNum++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("fal");

            stage_csv_int[0][2] = brNum;
            stage_csv_int[0][3] = brNum;
            stage_csv_int[0][4] = brNum;
            stage_csv_int[0][5] = brNum;

            activeBrock.Add(new Brock(brNum));
            activeBrock[activeBrock.Count - 1].csv_pos.Add(new FieldInfo(0, 2));
            activeBrock[activeBrock.Count - 1].csv_pos.Add(new FieldInfo(0, 3));
            activeBrock[activeBrock.Count - 1].csv_pos.Add(new FieldInfo(0, 4));
            activeBrock[activeBrock.Count - 1].csv_pos.Add(new FieldInfo(0, 5));
            activeBrock[activeBrock.Count - 1].stateChenge(true);

            for (int i = 0; i < activeBrock[0].csv_pos.Count; i++)
            {
                activeBrock[activeBrock.Count - 1].minos.Add(Instantiate(mino));
            }

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
                if (stage_csv_int[i][j] == 1)
                {
                    obj = Instantiate(ground);
                    obj.transform.position = new Vector3(j, i * -1, 0);
                }
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
                for (int j = 0; j < activeBrock[i].csv_pos.Count; j++)
                {
                    if (activeBrock[i].csv_pos[j].height >= stage_csv_int.Count - 1) // 最下層であるなら落下防止
                    {
                        Debug.Log("先がないため移動を中止します");
                        activeBrock[i].stateChenge(false);
                        break;
                    }
                    else if (activeBrock[i].brockNumGet() != stage_csv_int[(int)activeBrock[i].csv_pos[j].height + 1][(int)activeBrock[i].csv_pos[j].width]
                             && stage_csv_int[(int)activeBrock[i].csv_pos[j].height + 1][(int)activeBrock[i].csv_pos[j].width] != 0) // 移動先が違うブロックであるなら落下防止
                    {
                        activeBrock[i].stateChenge(false);
                        Debug.Log("先にブロックを確認したため移動を中止します");
                        Debug.Log(activeBrock[i].stateCheck());
                        break;
                    }
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
        for (int j = 0; j < activeBrock[i].csv_pos.Count; j++)
        {
            stage_csv_int[(int)activeBrock[i].csv_pos[j].height][(int)activeBrock[i].csv_pos[j].width] = 0;
            activeBrock[i].csv_pos[j] += new FieldInfo(1, 0);
            activeBrock[i].minos[j].transform.position = new Vector3((int)activeBrock[i].csv_pos[j].width, (int)activeBrock[i].csv_pos[j].height * -1,0);
            stage_csv_int[(int)activeBrock[i].csv_pos[j].height][(int)activeBrock[i].csv_pos[j].width] = activeBrock[i].brockNumGet();
        }
    }

    public List<int[]> StageDataGeter()
    {
        return stage_csv_int;
    }

    public FieldNumber FieldNumberGeter(Vector3 pos,Difference difference = Difference.STAY)
    {
        FieldNumber fieldnum = FieldNumber.NULL;
        int number = -1;
        switch (difference)
        {
            case Difference.UP:
                number = stage_csv_int[((int)pos.y + 1) * - 1][(int)pos.x];
                break;
            case Difference.DOWN:
                number = stage_csv_int[((int)pos.y - 1)* - 1][(int)pos.x];
                break;
            case Difference.LEFT:
                number = stage_csv_int[(int)pos.y * - 1][(int)pos.x - 1];
                break;
            case Difference.RIGHT:
                number = stage_csv_int[(int)pos.y * - 1][(int)pos.x + 1];
                break;
            case Difference.STAY:
                number = stage_csv_int[(int)pos.y * - 1][(int)pos.x];
                break;

        }

        if (number >= 0)
        {
            number = number >= 10 ? 10 : number;
            return fieldnum = (FieldNumber)number;
        }
        else
        {
            Debug.LogError("確認できない値を検知しました");
            return fieldnum = FieldNumber.NULL;
        }
    }
}
