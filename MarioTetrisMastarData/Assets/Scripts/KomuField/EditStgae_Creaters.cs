using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class EditStgae_Creaters : MonoBehaviour
{
    Dictionary<FieldInfo, int> AddItems = new Dictionary<FieldInfo, int>();
    Dictionary<FieldInfo, GameObject> FieldObject = new Dictionary<FieldInfo, GameObject>();
    [SerializeField,Header("一時保存ファイル(json形式に限ります)")] private TextAsset temporarilySaved;
    [SerializeField,Header("ステージデータの保存先(csv形式)")] private TextAsset SaveData;
    [SerializeField,Header("ファイルネーム設定")] private string fileName = "CreateData";
    [SerializeField,Header("ステージを移すカメラ設定")] private GameObject cam;

    SelectState state_;
    EditState state;
    FieldInfo position;
    int num;
    int limitNumber;
    int downLimitNumber;

    [SerializeField,Header("デバッグ値を表示するテキスト")] private Text debugText;
    [SerializeField,Header("操作説明を表示するテキスト")] private Text systemText;
    [SerializeField,Header("現在のオブジェクトのsprite表示するパネル")] private Image image;
    [SerializeField,Header("線の表現")] private GameObject glid;

    const int ACTICVE_STAGELIMIT_WIDTH = 80;
    const int ACTICVE_STAGELIMIT_HEIGHT = 20;

    [SerializeField,Header("カーソルにつかうオブジェクト")] GameObject cursor;
    [SerializeField,Header("プレイヤーの初期height値")] private int defaultPlayerPos_H = 17;
    [SerializeField,Header("プレイヤーの初期width値")] private int defaultPlayerPos_W = 3;
    // Start is called before the first frame update
    void Start()
    {
        position = new FieldInfo(0, 0);
        state = EditState.Glid_select;
        state_ = SelectState.Item_select;
        limitNumber = Utility_.BROCK_NUMBER_COUNT;
        downLimitNumber = 1;

        for (int i = 0; i < ACTICVE_STAGELIMIT_WIDTH; i++)
        {
            for (int j = 0; j < ACTICVE_STAGELIMIT_HEIGHT; j++)
            {
                if (i == defaultPlayerPos_W && j == defaultPlayerPos_H)
                {
                    FieldInfo info = new FieldInfo(defaultPlayerPos_H, defaultPlayerPos_W);
                    AddItems[info] = Utility_.PLAYER_NUMBER;
                    FieldObject[info] = Instantiate(new GameObject());
                    SpriteRenderer spRen = FieldObject[info].AddComponent<SpriteRenderer>();
                    spRen.sprite = Utility_.playerObject.GetComponent<SpriteRenderer>().sprite;
                    FieldObject[info].transform.position = FieldInfo.FieldInfoToVec(info);
                }
                else if (j == ACTICVE_STAGELIMIT_HEIGHT - 1)
                {
                    FieldInfo info = new FieldInfo(j, i);
                    AddItems[info] = 1;
                    FieldObject[info] = Instantiate(Utility_.objectGeter[1]);
                    FieldObject[info].transform.position = FieldInfo.FieldInfoToVec(info);
                }
                else
                {
                    FieldInfo info = new FieldInfo(j, i);
                    AddItems[info] = 0;
                    FieldObject[info] = Instantiate(glid);
                    FieldObject[info].transform.position = FieldInfo.FieldInfoToVec(info);
                }
            }
        }
        num = 1;
        CreateSample();
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(cursor.transform.position.x, cursor.transform.position.y, -10);
        debugText.text = $"create position = {position.height},{position.width}\nShift Key StageSave\nEditMode = {state}\nSelectMode = {state_}";
        systemText.text = "W or S Key => SelectModeChenge\nA and D Key => ItemChenge\nCtrl Key => EditMode Chenge\nArrow Key => CursorMove\nSpace Key => AllDelete";

        SelectTime();
        EditModeCommand();

        switch (state)
        {
            case EditState.Glid_select:
                systemText.text += "\nEnter Key => ItemCreate";
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (AddItems[position] == Utility_.PLAYER_NUMBER) return;
                    int addNum = state_ == SelectState.Item_select ? 0 : Utility_.BROCK_NUMBER_COUNT;
                    if (FieldObject[position] != null) Destroy(FieldObject[position]);
                    AddItems[position] = num + addNum;
                    if (state_ == SelectState.Item_select) FieldObject[position] = Instantiate(Utility_.objectGeter[num]);
                    else if (state_ == SelectState.Enemy_select)
                    {
                        GameObject newObj = Instantiate(new GameObject());
                        SpriteRenderer spRen = newObj.AddComponent<SpriteRenderer>();
                        spRen.sprite = Utility_.enemyGeter[num].GetComponent<SpriteRenderer>().sprite;
                        FieldObject[position] = Instantiate(newObj);
                    }
                    FieldObject[position].transform.position = FieldInfo.FieldInfoToVec(position);
                }
                break;
            case EditState.Elase_Mode:
                systemText.text += "\nEnter Key => ItemElase";
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (AddItems[position] == Utility_.PLAYER_NUMBER) return;
                    Debug.Log("delete");
                    AddItems[position] = 0;
                    if (FieldObject[position] != null) Destroy(FieldObject[position]);
                    FieldObject[position] = Instantiate(glid);
                    FieldObject[position].transform.position = FieldInfo.FieldInfoToVec(position);
                }
                break;
        }

        systemText.text += "\n\n!!handling warning!!\nbackSpace Key => ExistingData Create";

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Restore();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AllDelete();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            Save();
        }
    }

    public void EditModeCommand()
    {
        curcolMove();

        if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            state = state == EditState.Glid_select ? EditState.Elase_Mode : EditState.Glid_select;

            StateChenge(state);
        }
    }

    public void CreateSample()
    {
        if (state_ == SelectState.Item_select)
        {
            image.sprite = Utility_.objectGeter[num].GetComponent<SpriteRenderer>().sprite;
        }
        if (state_ == SelectState.Enemy_select)
        {
            image.sprite = Utility_.enemyGeter[num].GetComponent<SpriteRenderer>().sprite;
        }

    }

    public void curcolMove()
    {
        cursor.transform.position = FieldInfo.FieldInfoToVec(position);
        if (Input.GetKeyDown(KeyCode.RightArrow) && position.width + 1 < ACTICVE_STAGELIMIT_WIDTH) position.width++;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && position.width - 1 >= 0) position.width--;
        if (Input.GetKeyDown(KeyCode.UpArrow) && position.height - 1 >= 0) position.height--;
        if (Input.GetKeyDown(KeyCode.DownArrow) && position.height + 1 < ACTICVE_STAGELIMIT_HEIGHT) position.height++;
    }

    public void StateChenge(SelectState newState)
    {
        switch (newState)
        {
            case SelectState.Item_select:
                num = 1;
                state_ = SelectState.Item_select;
                CreateSample();
                limitNumber = Utility_.BROCK_NUMBER_COUNT;
                downLimitNumber = 1;
                break;
            case SelectState.Enemy_select:
                num = 0;
                state_ = SelectState.Enemy_select;
                CreateSample();
                limitNumber = 3;
                downLimitNumber = 0;
                break;
        }
    }

    public void StateChenge(EditState newState, int number = 0)
    {
        switch (newState)
        {
            case EditState.Glid_select:
                if (state_ == SelectState.Item_select) num = 1;
                else num = 0;
                state = EditState.Glid_select;
                break;
            case EditState.Elase_Mode:
                num = 0;
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

        StreamWriter writer = new StreamWriter(Application.dataPath + "/FieldData/" + SaveData.name + ".csv",false);

        for (int i = 0; i < ACTICVE_STAGELIMIT_HEIGHT; i++)
        {
            for (int j = 0; j < ACTICVE_STAGELIMIT_WIDTH; j++)
            {
                writer.Write(AddItems[new FieldInfo(i,j)].ToString());
                createStage.datastr += AddItems[new FieldInfo(i, j)].ToString();
                if (j != ACTICVE_STAGELIMIT_WIDTH - 1)
                {
                    writer.Write(",");
                    createStage.datastr += ",";
                }
            }

            writer.WriteLine();
            createStage.datastr += "\n";
        }

        writer.Flush();
        writer.Close();

        writer = new StreamWriter(Application.dataPath + "/" + fileName + "/" + temporarilySaved.name + ".json");

        string jsonstr = JsonUtility.ToJson(createStage);// JSONに変換
        writer.WriteLine(jsonstr);
        writer.Flush();//バッファをクリアする
        writer.Close();//ファイルをクローズする

        Debug.Log("save Compleate");
    }

    public void AllDelete()
    {
        for (int i = 0;i < ACTICVE_STAGELIMIT_HEIGHT;i++)
        {
            for (int j = 0;j < ACTICVE_STAGELIMIT_WIDTH;j++)
            {
                FieldInfo info = new FieldInfo(i,j);
                Destroy(FieldObject[info]);
                FieldObject[info] = Instantiate(glid);
                FieldObject[info].transform.position = FieldInfo.FieldInfoToVec(info);
                AddItems[info] = 0;
            }
        }
    }

    public void Restore()
    {
        StreamReader reader = new StreamReader(Application.dataPath + "/" + fileName + "/" + temporarilySaved.name + ".json"); //受け取ったパスのファイルを読み込む
        string datastr = reader.ReadToEnd();//ファイルの中身をすべて読み込む
        reader.Close();//ファイルを閉じる

        CreateStageData stageData = JsonUtility.FromJson<CreateStageData>(datastr);

        List<int[]> list = Utility_.StringListToIntList(stageData.datastr);

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list[i].Length; j++)
            {
                FieldInfo info = new FieldInfo(i, j);
                AddItems[info] = list[i][j];
                if (FieldObject[info] != null) Destroy(FieldObject[info]);

                if (list[i][j] != 0)
                {
                    if (list[i][j] < Utility_.BROCK_NUMBER_COUNT)
                    {
                        FieldObject[info] = Instantiate(Utility_.objectGeter[list[i][j]]);
                        FieldObject[info].transform.position = FieldInfo.FieldInfoToVec(info);
                    }
                    else if (list[i][j] < Utility_.BROCK_NUMBER_COUNT + Utility_.ENEMY_NUMBER_COUNT)
                    {
                        FieldObject[info] = Instantiate(new GameObject());
                        SpriteRenderer spRen = FieldObject[info].AddComponent<SpriteRenderer>();
                        spRen.sprite = Utility_.enemyGeter[list[i][j] - Utility_.BROCK_NUMBER_COUNT].GetComponent<SpriteRenderer>().sprite;
                        FieldObject[info].transform.position = FieldInfo.FieldInfoToVec(info);
                    }
                    else if (list[i][j] == Utility_.PLAYER_NUMBER)
                    {
                        FieldObject[info] = Instantiate(new GameObject());
                        SpriteRenderer spRen = FieldObject[new FieldInfo(i, j)].AddComponent<SpriteRenderer>();
                        spRen.sprite = Utility_.playerObject.GetComponent<SpriteRenderer>().sprite;
                        FieldObject[info].transform.position = FieldInfo.FieldInfoToVec(info);

                    }
                }
                else
                {
                    FieldObject[info] = Instantiate(glid);
                    FieldObject[info].transform.position = FieldInfo.FieldInfoToVec(info);
                    AddItems[info] = 0;
                }
            }
        }
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