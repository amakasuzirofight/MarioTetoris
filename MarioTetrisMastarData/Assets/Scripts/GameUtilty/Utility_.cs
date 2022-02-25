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

    private static Text systemMessage;
    private static List<int[]> csvData;
    private static List<bool> stageFlgList;

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

    public static List<int[]> CsvToIntList(TextAsset baseData)
    {
        StringReader reader = new StringReader(baseData.text);
        List<string[]> csvData = new List<string[]>();
        List<int[]> returnData = new List<int[]>();
        while (reader.Peek() != -1) // reader.PeaekÇ™-1Ç…Ç»ÇÈÇ‹Ç≈
        {
            string line = reader.ReadLine(); // àÍçsÇ∏Ç¬ì«Ç›çûÇ›
            csvData.Add(line.Split(',')); // , ãÊêÿÇËÇ≈ÉäÉXÉgÇ…í«â¡
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

    public static void CsvWriter(FieldInfo position,int number)
    {
        csvData[position.height][position.width] = number;
    }

    public static List<int[]> FieldData
    {
        get => csvData;
        private set => csvData = value;
    }
}

public struct Stage1ClearFlg
{
    public bool clearFlg1_1;
    public bool clearFlg1_2;
    public bool clearFlg1_3;
    public bool clearFlg1_4;
    public bool clearFlg1_5;
    public bool clearFlg1_6;
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

public enum ItemNumber
{

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

    public static FieldInfo operator +(FieldInfo base_,FieldInfo add)
    {
        return new FieldInfo(base_.height + add.height,base_.width + add.width);
    }

    public static FieldInfo operator -(FieldInfo base_, FieldInfo add)
    {
        return new FieldInfo(base_.height - add.height, base_.width - add.width);
    }

    public static FieldInfo VecToFieldInfo(Vector2 vec)
    {
        return new FieldInfo((int)vec.y,(int)vec.x);
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