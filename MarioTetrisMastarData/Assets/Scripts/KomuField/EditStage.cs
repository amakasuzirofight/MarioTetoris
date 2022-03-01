using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class EditStage : MonoBehaviour
{
    Dictionary<FieldInfo, int> AddItems = new Dictionary<FieldInfo, int>();
    Dictionary<FieldInfo, GameObject> FieldObject = new Dictionary<FieldInfo, GameObject>();
    [SerializeField] private TextAsset saveFile;
    [SerializeField] private GameObject cam;

    SelectState state_;
    EditState state;
    FieldInfo position;
    int num;
    int limitNumber;
    int downLimitNumber;

    [SerializeField] private Text debugText;
    [SerializeField] private Text systemText;

    const int ACTICVE_STAGELIMIT = 20;

    [SerializeField] GameObject sample;
    [SerializeField] GameObject cursor;
    [SerializeField] Vector2 samplePos;
    // Start is called before the first frame update
    void Start()
    {
        position = new FieldInfo(0,0);
        state = EditState.Glid_select;
        state_ = SelectState.Item_select;
        limitNumber = 5;
        downLimitNumber = 1;

        for (int i = 0;i < ACTICVE_STAGELIMIT;i++)
        {
            for (int j = 0;j < ACTICVE_STAGELIMIT;j++)
            {
                AddItems[new FieldInfo(j, i)] = 0;
                FieldObject[new FieldInfo(j, i)] = null;
            }
        }
        num = 1;
        CreateSample();
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = $"create position = {position.height},{position.width}\ncreate Number = {num}\nS Key StageSave\nEditMode = {state}\nSelectMode = {state_}\nA Key Let's Play";

        systemText.text = $"up or down ArrowKey => select Chenge\nSpace Key => ItemElase\nEnter Key => Glid select";
        SelectTime();

        switch (state)
        {
            case EditState.Glid_select:
                systemText.text = "backSpace Key => Brock Select\nEnter Key => ItemCreate";
                curcolMove();
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    int addNum = state_ == SelectState.Item_select ? 0 : Utility_.BROCK_NUMBER_COUNT;
                    if (FieldObject[position] != null) Destroy(FieldObject[position]);
                    AddItems[position] = num + addNum;
                    FieldObject[position] = Instantiate(sample);
                    FieldObject[position].transform.position = FieldInfo.FieldInfoToVec(position);
                }
                break;
            case EditState.Elase_Mode:
                systemText.text = "backSpace Key => Brock Select\nEnter Key => ItemElase";
                curcolMove();
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Debug.Log("delete");
                    AddItems[position] = 0;
                    if (FieldObject[position] != null) Destroy(FieldObject[position]);
                }
                break;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("CreateStage_");
        }
    }

    public void CreateSample()
    {
        Destroy(sample);
        if (state_ == SelectState.Item_select) sample = Instantiate(Utility_.objectGeter[num]);
        else if (state_ == SelectState.Enemy_select) sample = Instantiate(Utility_.enemyGeter[num]);
        sample.transform.position = samplePos;
    }

    public void curcolMove()
    {
        cursor.transform.position = FieldInfo.FieldInfoToVec(position);
        if (Input.GetKeyDown(KeyCode.RightArrow) && position.width + 1 < ACTICVE_STAGELIMIT) position.width++;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && position.width - 1 >= 0) position.width--;
        if (Input.GetKeyDown(KeyCode.UpArrow) && position.height - 1 >= 0) position.height--;
        if (Input.GetKeyDown(KeyCode.DownArrow) && position.height + 1 < ACTICVE_STAGELIMIT) position.height++;
    }

    public void StateChenge(SelectState newState)
    {
        switch (newState)
        {
            case SelectState.Item_select:
                num = 1;
                state_ = SelectState.Item_select;
                CreateSample();
                limitNumber = 5;
                downLimitNumber = 1;
                break;
            case SelectState.Enemy_select:
                num = 0;
                state_ = SelectState.Enemy_select;
                CreateSample();
                limitNumber = 2;
                downLimitNumber = 0;
                break;
        }
    }

    public void StateChenge(EditState newState,int number = 0)
    {
        switch (newState)
        {
            case EditState.Glid_select:
                num += number;
                state = EditState.Glid_select;
                break;
            case EditState.Elase_Mode:
                num = 0;
                limitNumber = 0;
                Destroy(sample);
                state = EditState.Elase_Mode;
                break;
        }
    }

    public void SelectTime()
    {
        if (Input.GetKeyDown(KeyCode.D) && num + 1 < limitNumber)
        {
            num++;
            CreateSample();
        }
        if (Input.GetKeyDown(KeyCode.A) && num - 1 >= downLimitNumber)
        {
            num--;
            CreateSample();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (state_ == SelectState.Item_select) StateChenge(SelectState.Enemy_select);
            else StateChenge(SelectState.Item_select);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (state_ == SelectState.Item_select) StateChenge(SelectState.Enemy_select);
            else StateChenge(SelectState.Item_select);
        }
    }

    public void Save()
    {
        CreateStageData createStage = new CreateStageData();

        for (int i = 0;i < ACTICVE_STAGELIMIT;i++)
        {
            for (int j = 0;j < ACTICVE_STAGELIMIT;j++)
            {
                createStage.datastr += AddItems[new FieldInfo(i,j)].ToString();
                if (j != ACTICVE_STAGELIMIT - 1) createStage.datastr += ",";
            }

            createStage.datastr += "\n";
        }

        StreamWriter writer = new StreamWriter(Application.dataPath + "/CreateStage/" + saveFile.name + ".json");

        string jsonstr = JsonUtility.ToJson(createStage);// JSONに変換
        writer.WriteLine(jsonstr);
        writer.Flush();//バッファをクリアする
        writer.Close();//ファイルをクローズする

        Debug.Log("save Compleate");
    }

    public enum EditState
    {
        Elase_Mode,
        Glid_select,
    }

    public enum SelectState
    {
        Item_select,
        Enemy_select,
    }
}

