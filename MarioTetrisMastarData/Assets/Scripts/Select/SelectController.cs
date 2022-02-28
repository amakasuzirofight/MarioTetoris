using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Inputer;
using RobotItem;
using ItemGenerater;
using Tetris;

namespace Select
{
    public class SelectController : MonoBehaviour, ISelectedItem
    {
        [SerializeField] Sprite stoneSpr;
        [SerializeField] GameObject RobotGenerateObj;

        IGetItemBox getItemBox;
        IItemDataChange itemDataChange;
        IRemoveItems removeItems;
        ISelectInput selectInput;
        IGenerator generator;

        SelectState selectState;
        int selectItemWeigthNum;
        int selectItemHighNum;
        int selectHighMaxNum;//最大アイテム数

        int tetrisSelectCount;
        TetrisAngle tetrisAngle;
        Dictionary<ItemName, int> SelectItemDic = new Dictionary<ItemName, int>();
        List<ItemName> haveItemName;
        List<Tetris.TetrisTypeEnum> tetrisTable = new List<Tetris.TetrisTypeEnum>();

        private void Awake()
        {
            Utility.Locator<ISelectedItem>.Bind(this);
        }
        // Start is called before the first frame update
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


            selectState = SelectState.FirstSelect;
        }
        void Update()
        {
            //selectState = selectItemWeigthNum % 2 == 0 ? SelectState.FirstSelect : SelectState.Spin;

            ViewItemCursle();
        }
        public ItemName ISelectedItem()
        {
            throw new System.NotImplementedException();
        }
        void CursollMove(SelectButtonType buttonType)
        {
            //アイテム選択中の時
            if (selectState == SelectState.FirstSelect)
            {
                switch (buttonType)
                {
                    case SelectButtonType.MouceLeft:
                        CursollSlide(-1);
                        break;
                    case SelectButtonType.MouceRight:
                        CursollSlide(1);
                        break;
                    case SelectButtonType.ArrowDown:
                      
                        GenerateItem();
                        break;
                    case SelectButtonType.Non:
                        break;
                }

            }
            else
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
                        GenerateItem();
                        break;
                    case SelectButtonType.Non:
                        break;
                    default:
                        break;
                }
            }
        }
        void CursollSlide(int num)
        {
            //杞憂
            //if (selectItemWeigthNum+num > 3) selectItemWeigthNum = 0;
            //if (selectItemWeigthNum + num < 0) selectItemWeigthNum = 2;
            if (selectItemWeigthNum + num >= int.MaxValue) selectItemWeigthNum = 0;
            selectItemWeigthNum += num;
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
        void SpinMino(int num)
        {

        }
        void ViewItemCursle()
        {
            Debug.Log(selectItemWeigthNum);

            Debug.Log(haveItemName[selectItemHighNum]);
        }
        void SynchroItem(Dictionary<ItemName, int> data)
        {
            SelectItemDic = data;
            //アイテム番号順に並び替え
            haveItemName = new List<ItemName>(SelectItemDic.Keys);
            haveItemName.Sort();

            //めっちゃ不安
            //SelectItemDic.OrderBy(x => (int)x.Key);
            //selectHighMaxNum = SelectItemDic.Count;
        }
        void SynchroTetris(int num)
        {
            tetrisSelectCount = num;
        }
        void GenerateItem()
        {
            if (selectItemWeigthNum % 2 == 0)
            {
                selectState = SelectState.Spin;
                return;
            }
            generator.GenerateItem(haveItemName[selectItemWeigthNum], RobotGenerateObj.transform.position);
        }
        void GenerateTetris()
        {
            TetrisTypeEnum generateType;
            //テーブルに何も入っていない場合
            if (tetrisTable.Count() == 0)
            {
                //テーブルを生成しなおす
                TetrisTableReflash();
            }
            TetrisTypeEnum rand = (TetrisTypeEnum)Random.Range(0, tetrisTable.Count());
            //generator.GenerateItem(rand,);
        }
        void TetrisTableReflash()
        {
            for (int i = 0; i < 7; i++)
            {
                tetrisTable.Add((TetrisTypeEnum)i);
            }
        }
        /// <summary>
        /// 選択用ステータス
        /// </summary>
        private enum SelectState
        {
            FirstSelect, Spin
        }

    }
}

