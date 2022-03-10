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
    private static Text nameMessage;
    private static Image systemPanel;
    private static Image namePanel;
    private static List<int[]> csvData;
    private static Stage thisStage = Stage.NONE;
    private static FieldState fieldState;
    private static List<string> messageList;
    private static string messageMaster;
    private static int msIndex;
    private static bool eventFlg = false;

    public static List<bool> stageFlgList;
    public static GameObject playerObject;
    public static GameObject robotObject;

    public const int BROCK_NUMBER_COUNT = 20;
    public const int ENEMY_NUMBER_COUNT = 20;
    public const int PLAYER_NUMBER = 50;

    public static FieldState GameState
    {
        get => fieldState;
        private set => fieldState = value;
    }

    public static void MessageSetting(bool stateChenge = true)
    {
        if (systemMessage == null || systemMessage == default)
        {
            systemMessage = GameObject.Find("Canvas").GetComponentInChildren<Text>();
            systemMessage.gameObject.SetActive(false);
        }
        if (stateChenge) GameState = FieldState.CONVERSATION;
    }

    public static void MessageSetting(Text mainText,Text nameText,Image mainPanel,Image namePanel_)
    {
        systemMessage = mainText;
        nameMessage = nameText;
        systemPanel = mainPanel;
        namePanel = namePanel_;

        systemMessage.gameObject.SetActive(false);
        nameMessage.gameObject.SetActive(false);
        systemPanel.gameObject.SetActive(false);
        namePanel.gameObject.SetActive(false);
    }

    public static void MessageWriter()
    {
        MessageSetting();
        nameMessage.text = messageMaster;
        if (msIndex + 1 != messageList.Count)
        {
            msIndex++;
            systemMessage.text = messageList[msIndex];
        }
        else
        {
            msIndex = 0;
            CloseMessage();
        }
    }

    public static void OpenMessage(List<string> newTexts,string name)
    {
        MessageSetting(true);
        messageMaster = name;
        systemMessage.gameObject.SetActive(true);
        nameMessage.gameObject.SetActive(true);
        messageList = newTexts;
        msIndex = -1;
        MessageWriter();
        systemPanel.gameObject.SetActive(true);
        namePanel.gameObject.SetActive(true);
    }

    private static void CloseMessage()
    {
        systemMessage.gameObject.SetActive(false);
        nameMessage.gameObject.SetActive(false);
        systemMessage.text = default;
        fieldState = FieldState.NORMAL;
        systemPanel.gameObject.SetActive(false);
        namePanel.gameObject.SetActive(false);
    }

    public static void EventActiveate(Action target)
    {
        if (eventFlg) return;
        eventFlg = true;
        fieldState = FieldState.EVENT;
        target();
    }

    public static void EventEnd()
    {
        eventFlg = false;
        fieldState = FieldState.NORMAL;
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

    public static List<FieldInfo> LimitChecker(List<FieldInfo> basePos)
    {
        int count = 0;
        List<FieldInfo> answer = basePos;
        bool endFlg = false;

        while (count < 30)
        {
            count++;
            for (int i = 0;i < answer.Count;i++)
            {
                if (answer[i].height + 1 >= Utility_.FieldData.Count)
                {
                    Debug.LogWarning("限界値です");
                    return basePos;
                }

                if ((Utility_.FieldData[answer[i].height + 1][answer[i].width] < Utility_.BROCK_NUMBER_COUNT 
                    || Utility_.FieldData[answer[i].height + 1][answer[i].width] >= 100)
                    && Utility_.FieldData[answer[i].height + 1][answer[i].width] != 0)
                {
                    endFlg = true;
                }
            }

            if (!endFlg)
            {
                for (int i = 0;i < answer.Count;i++)
                {
                    answer[i] += new FieldInfo(1, 0);
                }
            }
            else
            {
                return answer;
            }
        }

        Debug.LogWarning("例外処理がなされました　値に異変がないか確認してください");
        return basePos;
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