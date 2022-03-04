using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Inputer;
using RobotItem;
using ItemGenerater;
using Tetris;
using Select;

public class SelecterControl : MonoBehaviour, ISelectedItem
{
    [SerializeField] GameObject RobotObj;
    [SerializeField] List<Sprite> itemSprites;
    [SerializeField] Sprite nullSpr;
    [SerializeField] Image tetrisImage;
    [SerializeField] Image itemImage;
    [SerializeField] Image selectCursor;
    [SerializeField] Text tetrisCountText;
    [SerializeField] Text ItemCountText;

    IGetItemBox getItemBox;
    IItemDataChange itemDataChange;
    IRemoveItems removeItems;
    ISelectInput selectInput;
    IGenerator generator;

    //現在何を選択しているかステータス
    SelectState selectState;
    //現在の横カーソル番号
    int selectItemWeigthNum;
    //値を書き換えるときにカーソルを動かす
    int selectItemWeight
    {
        get
        {
            return selectItemWeigthNum;
        }
        set
        {
            selectItemWeigthNum = Mathf.Clamp(value, 0, 1);

            ChangedWightCursor(selectItemWeigthNum);
        }
    }
    //現在の縦カーソル
    int selectItemHighNum;
    //アイテムの縦の上限値
    int selectHighMaxNum;

    int spinCount;

    int tetCount;
    //名前に対応したロゴを格納
    Dictionary<ItemName, Sprite> ItemSpriteDic = new Dictionary<ItemName, Sprite>();
    //アイテムをどれぐらい持っているか
    Dictionary<ItemName, int> selectItemDic = new Dictionary<ItemName, int>();
    Dictionary<ItemName, int> selectItemProp
    {
        get
        {
            return selectItemDic;
        }
        set
        {
            ViewItemList = ViewItem(value);
            selectHighMaxNum = ViewItemList.Count();
            selectItemDic = value;
        }
    }
    //UIに表示する一つ以上のリスト
    List<ItemName> ViewItemList = new List<ItemName>();
    //テトリス生成用リスト
    List<TetrisTypeEnum> tetrisTable = new List<TetrisTypeEnum>();
    //控えテトリス
    TetrisTypeEnum[] RandomTetrises = new TetrisTypeEnum[3];

    private void Awake()
    {
        Utility.Locator<ISelectedItem>.Bind(this);
    }
    void Start()
    {
        getItemBox = Utility.Locator<IGetItemBox>.GetT();
        itemDataChange = Utility.Locator<IItemDataChange>.GetT();
        itemDataChange.ChangeItemBoxValue += SynchroItem;
        itemDataChange.ChangeTetPieceValue += SynchroTetris;

        removeItems = Utility.Locator<IRemoveItems>.GetT();
        selectInput = Utility.Locator<ISelectInput>.GetT();
        selectInput.OnSelectButton += CursollMove;
        selectInput.MouceWhileEvent += CursollScroll;

        generator = Utility.Locator<IGenerator>.GetT();

        TetrisTableReflash();
        //どんなテトリスをだすかを最初に3つ生成
        for (int i = 0; i < 3; i++)
        {
            RandomTetrises[i] = GetRandomTetrisType();
        }
    }

    void Update()
    {
    }
    void CursollMove(SelectButtonType buttonType)
    {
        //アイテム選択中の時
        if (selectState == SelectState.Select)
        {
            switch (buttonType)
            {
                case SelectButtonType.MouceLeft:
                    selectItemWeight++;
                    break;
                case SelectButtonType.MouceRight:
                    selectItemWeight--;
                    break;
                case SelectButtonType.ArrowDown:
                    if (selectItemWeight == 0)
                    {
                        //生成できる数に満たない場合リターン
                        if (getItemBox.GetTetris() < 4) return;
                        spinCount = 0;
                        selectState = SelectState.Spin;
                    }
                    else
                    {
                        GenerateItem();
                    }

                    break;
                case SelectButtonType.Non:
                    break;
            }
        }
        if (selectState == SelectState.Spin)
        {


            switch (buttonType)
            {
                case SelectButtonType.MouceLeft:
                    SpinMino(-1);
                    break;
                case SelectButtonType.MouceRight:
                    SpinMino(1);
                    break;
                case SelectButtonType.ArrowDown:

                    GenerateTetris(RandomTetrises[0]);
                    break;
                case SelectButtonType.Non:
                    break;
                default:
                    break;
            }
            GenerateTetrisVision();
        }
    }
    #region テトリス
    void SpinMino(int num)
    {
        //超えたらもどす
        if (spinCount + num < 0)
        {
            spinCount = 4;
        }
        else if (spinCount + num > 4)
        {
            spinCount = 0;
        }
        else
        {
            spinCount += num;
        }
    }

    //あとでやる
    void GenerateTetrisVision()
    {

    }
    void GenerateTetris(TetrisTypeEnum tetrisType)
    {
        generator.GenerateItem(tetrisType, (TetrisAngle)spinCount, FieldInfo.VecToFieldInfo(RobotObj.transform.position));
        //新しいテトリスのデータを生成
        for (int i = 0; i < RandomTetrises.Length; i++)
        {

            if (i != RandomTetrises.Length - 1)
            {
                RandomTetrises[i] = RandomTetrises[i + 1];
            }
            else
            {
                RandomTetrises[i] = GetRandomTetrisType();
            }
        }
        //ボックスに減ったことを伝える
        removeItems.RemoveTetrisPiece(4);
        tetrisCountText.text = tetCount.ToString();
    }

    TetrisTypeEnum oldTetrisType;
    List<TetrisTypeEnum> GeneratedList = new List<TetrisTypeEnum>();

    /// <summary>
    /// ランダムなテトリスを取得
    /// </summary>
    /// <returns></returns>
    TetrisTypeEnum GetRandomTetrisType()
    {
        TetrisTypeEnum rand;
        if (GeneratedList.Count == 7)
        {
            TetrisTableReflash();
        }

        while (true)
        {
            //ランダムで生成
            rand = (TetrisTypeEnum)Random.Range(0, 7);
            //その値が被ってないか確認
            if (!GeneratedList.Contains(rand) && oldTetrisType != rand)
            {
                //一度生成したミノをブラックリストに登録
                GeneratedList.Add(rand);
                oldTetrisType = rand;
                Debug.LogWarning(rand);
                return rand;
            }
        }
    }
    void TetrisTableReflash()
    {
        //for (int i = 0; i < 7; i++)
        //{
        //    tetrisTable.Add((TetrisTypeEnum)i);
        //}
        GeneratedList.Clear();
    }
    void SynchroTetris(int num)
    {
        tetCount = num;
    }

    #endregion
    #region アイテム
    void GenerateItem()
    {
        generator.GenerateItem(ViewItem(selectItemDic)[selectItemHighNum], RobotObj.transform.position);
    }
    /// <summary>
    /// アイテムDicから入っていないアイテムを取り除いて並び変える　
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    List<ItemName> ViewItem(Dictionary<ItemName, int> items)
    {
        List<ItemName> itemNames = new List<ItemName>();
        foreach (var temp in items)
        {
            if (temp.Value != 0)
            {
                itemNames.Add(temp.Key);
            }
        }
        itemNames.Sort();
        return itemNames;
    }

    //アイテムの個数が変化したときに同期させる
    void SynchroItem(Dictionary<ItemName, int> data)
    {
        selectItemDic = data;
    }
    #endregion

    #region カーソル移動
    /// <summary>
    /// カーソル移動
    /// </summary>
    /// <param name="カーソル移動用カウント"></param>
    void ChangedWightCursor(int count)
    {
        if (count % 2 != 0)
        {
            selectCursor.transform.position = tetrisImage.transform.position;
        }
        else
        {
            selectCursor.transform.position = itemImage.transform.position;
        }

    }
    void CursollScroll(float power)
    {
        if (selectState == SelectState.Spin) return;
        int num = power > 0 ? 1 : -1;
        if (selectItemHighNum + num > selectHighMaxNum)
        {
            selectItemHighNum = 0;
        }
        else if (selectItemHighNum + num < 0)
        {
            selectItemHighNum = selectHighMaxNum;
        }
        else
        {
            selectItemHighNum += num;
        }
    }
    #endregion

    public ItemName ISelectedItem()
    {
        throw new System.NotImplementedException();
    }
    private enum SelectState
    {
        Select, Spin
    }
}
