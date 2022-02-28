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
        state = EditState.Item_select;
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
        debugText.text = $"create position = {position.height},{position.width}\ncreate Number = {num}\nS Key StageSave\nActiveMode = {state}\nA Key Let's Play";

        switch (state)
        {
            case EditState.Glid_select:
                systemText.text = "backSpace Key => Brock Select\nEnter Key => ItemCreate";
                curcolMove();
                if (Input.GetKeyDown(KeyCode.Backspace)) StateChenge(EditState.Item_select);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    AddItems[position] = num;
                    FieldObject[position] = Instantiate(sample);
                    FieldObject[position].transform.position = FieldInfo.FieldInfoToVec(position);
                }
                break;
            case EditState.Elase_Mode:
                systemText.text = "backSpace Key => Brock Select\nEnter Key => ItemElase";
                curcolMove();
                if (Input.GetKeyDown(KeyCode.Backspace)) StateChenge(EditState.Item_select);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Debug.Log("delete");
                    AddItems[position] = 0;
                    if (FieldObject[position] != null) Destroy(FieldObject[position]);
                }
                break;
            case EditState.Item_select:
                systemText.text = $"up or down ArrowKey => select Chenge\nSpace Key => ItemElase\nEnter Key => Glid select";
                if (Input.GetKeyDown(KeyCode.Space)) StateChenge(EditState.Elase_Mode);
                SelectTime();
                break;
            case EditState.Enemy_select:
                systemText.text = $"up or down ArrowKey => select Chenge\nSpace Key => ItemElase\nEnter Key => Glid select";
                if (Input.GetKeyDown(KeyCode.Space)) StateChenge(EditState.Elase_Mode);
                SelectTime();
                break;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("CreateStage_");
        }
    }

    public void CreateSample()
    {
        Destroy(sample);
        if (state == EditState.Item_select) sample = Instantiate(Utility_.objectGeter[num]);
        else if (state == EditState.Enemy_select) sample = Instantiate(Utility_.enemyGeter[num]);
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

    public void StateChenge(EditState newState,int number = 0)
    {
        switch (newState)
        {
            case EditState.Item_select:
                position = new FieldInfo(0,0);
                num = 1;
                state = EditState.Item_select;
                CreateSample();
                limitNumber = 5;
                downLimitNumber = 1;
                break;
            case EditState.Enemy_select:
                position = new FieldInfo(0, 0);
                num = 0;
                state = EditState.Enemy_select;
                CreateSample();
                limitNumber = 2;
                downLimitNumber = 0;
                break;
            case EditState.Glid_select:
                num += number;
                state = EditState.Glid_select;
                // cursor = Instantiate(sample);
                break;
            case EditState.Elase_Mode:
                num = 0;
                limitNumber = 0;
                Destroy(sample);
                //Destroy(cursor);
                //cursor = null;
                state = EditState.Elase_Mode;
                break;
        }
    }

    public void SelectTime()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && num + 1 < limitNumber)
        {
            num++;
            CreateSample();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && num - 1 >= downLimitNumber)
        {
            num--;
            CreateSample();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (state == EditState.Item_select) StateChenge(EditState.Enemy_select);
            else
            {
                state--;
                StateChenge(state);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (state == EditState.Enemy_select) StateChenge(EditState.Item_select);
            else
            {
                state++;
                StateChenge(state);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (state == EditState.Enemy_select) StateChenge(EditState.Glid_select, Utility_.BROCK_NUMBER_COUNT);
            else StateChenge(EditState.Glid_select);
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
        Item_select,
        Enemy_select,
        Elase_Mode,
        Glid_select,
    }
}
