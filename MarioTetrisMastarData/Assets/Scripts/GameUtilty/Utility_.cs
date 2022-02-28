using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public static class Utility_
{
    public static Dictionary<int, GameObject> objectGeter = new Dictionary<int, GameObject>();
    public static Dictionary<int, GameObject> minoGeter = new Dictionary<int, GameObject>();
    public static Dictionary<int, GameObject> enemyGeter = new Dictionary<int, GameObject>();

    private static Text systemMessage;
    private static List<int[]> csvData;
    private static Stage thisStage = Stage.NONE;
    public static List<bool> stageFlgList;

    public const int BROCK_NUMBER_COUNT = 20;
    public const int ENEMY_NUMBER_COUNT = 20;

    public static void MessageWriter(string message)
    {
        systemMessage.text = message;
    }

    public static void OpenMessage()
    {
        if (systemMessage == null) systemMessage = GameObject.Find("Canvas").GetComponentInChildren<Text>();
        systemMessage.gameObject.SetActive(true);
    }

    public static void CloseMessage()
    {
        systemMessage.text = default;
        systemMessage.gameObject.SetActive(false);
    }

    public static void StageFlgSeter(List<bool> newStage, Stage stage)
    {
        if (stage == thisStage) return;
        else thisStage = stage;
        stageFlgList = newStage;
    }

    public static void FlgChenger(int index)
    {
        stageFlgList[index] = true;
    }

    public static List<int[]> CsvToIntList(TextAsset baseData)
    {
        StringReader reader = new StringReader(baseData.text);
        List<string[]> csvData = new List<string[]>();
        List<int[]> returnData = new List<int[]>();
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvData.Add(line.Split(',')); // , 区切りでリストに追加
        }

        for (int i = 0; i < csvData.Count; i++)
        {
            returnData.Add(new int[csvData[i].Length]);
            for (int j = 0; j < csvData[i].Length; j++)
            {
                returnData[i][j] = Convert.ToInt32(csvData[i][j]);
            }
        }

        FieldData = returnData;

        return returnData;
    }

    public static List<int[]> StringListToIntList(string str)
    {
        StringReader reader = new StringReader(str);
        List<string[]> csvData = new List<string[]>();
        List<int[]> returnData = new List<int[]>();
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvData.Add(line.Split(',')); // , 区切りでリストに追加
        }

        for (int i = 0; i < csvData.Count; i++)
        {
            returnData.Add(new int[csvData[i].Length]);
            for (int j = 0; j < csvData[i].Length; j++)
            {
                returnData[i][j] = Convert.ToInt32(csvData[i][j]);
            }
        }

        FieldData = returnData;

        return returnData;
    }

    public static void CsvWriter(FieldInfo position, int number)
    {
        csvData[position.height][position.width] = number;
    }

    public static List<int[]> FieldData
    {
        get => csvData;
        private set => csvData = value;
    }
}

[Serializable]
public struct StageClearFlg
{
    public bool clearFlg_1;
    public bool clearFlg_2;
    public bool clearFlg_3;
    public bool clearFlg_4;
    public bool clearFlg_5;
    public bool clearFlg_6;
    public bool clearFlg_7;
    public bool clearFlg_8;
    public bool clearFlg_9;
    public bool clearFlg_10;
}

public enum Difference
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    STAY,
    COUNT
}

public enum FieldNumber
{
    NULL = -1,
    NONE = 0,
    GROUND = 1,
    MINO = 10
}

public enum FieldState
{
    NORMAL,
    CONVERSATION,
    EVENT
}

public enum Stage
{
    NONE,
    STAGE1,
    STAGE2,
    COUNT
}

[Serializable]
public struct CreateStageData
{
    [SerializeField] public string datastr;
}

public struct FieldInfo
{
    public int height;
    public int width;

    public FieldInfo(int newHeight, int newWidth)
    {
        height = newHeight;
        width = newWidth;
    }

    public static FieldInfo operator +(FieldInfo base_, FieldInfo add)
    {
        return new FieldInfo(base_.height + add.height, base_.width + add.width);
    }

    public static FieldInfo operator -(FieldInfo base_, FieldInfo add)
    {
        return new FieldInfo(base_.height - add.height, base_.width - add.width);
    }

    public static FieldInfo VecToFieldInfo(Vector2 vec)
    {
        return new FieldInfo((int)vec.y * -1, (int)vec.x);
    }

    public static Vector2 FieldInfoToVec(FieldInfo pos)
    {
        return new Vector2(pos.width, pos.height * -1);
    }
}

public class Brock
{
    public List<FieldInfo> csv_pos;
    public bool fallFlg;
    public List<GameObject> minos;
    private int brockNumber;

    public Brock(int newNum)
    {
        csv_pos = new List<FieldInfo>();
        minos = new List<GameObject>();
        fallFlg = true;
        brockNumber = newNum;
        Debug.Log($"CreateBrock number:{brockNumber}");
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