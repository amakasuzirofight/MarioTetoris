using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Inputer;
using RobotItem;
using ItemGenerater;
using Tetris;

namespace Select
{
    public class SelectController : MonoBehaviour, ISelectedItem
    {
        [SerializeField] GameObject RobotObj;
        [SerializeField] List<Sprite> itemSprites;
        [SerializeField] Sprite nullSpr;
        [SerializeField] Image tetrisImage;
        [SerializeField] Image itemImage;
        [SerializeField] Image selectCursor;

        IGetItemBox getItemBox;
        IItemDataChange itemDataChange;
        IRemoveItems removeItems;
        ISelectInput selectInput;
        IGenerator generator;

        SelectState selectState;
        int selectItemWeigthNum;
        int selectItemHighNum;
        int selectHighMaxNum;//�ő�A�C�e����

        int tetrisSelectCount;
        int spinCount;
        TetrisAngle tetrisAngle;
        Dictionary<ItemName, int> selectItemDic = new Dictionary<ItemName, int>();
        Dictionary<ItemName, Sprite> ItemSpriteDic = new Dictionary<ItemName, Sprite>();
        List<ItemName> haveItemName = new List<ItemName>();
        List<TetrisTypeEnum> tetrisTable = new List<TetrisTypeEnum>();
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

            for (int i = 0; i < itemSprites.Count; i++)
            {
                ItemSpriteDic.Add((ItemName)i, itemSprites[i]);
            }
            selectItemDic = getItemBox.GetItemBox();

            selectState = SelectState.FirstSelect;
            TetrisTableReflash();
        }
        void Update()
        {
          
            ViewItemCursle();
        }
        public ItemName ISelectedItem()
        {
            throw new System.NotImplementedException();
        }
        void CursollMove(SelectButtonType buttonType)
        {
            //�A�C�e���I�𒆂̎�
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
            //�X�J
            //if (selectItemWeigthNum+num > 3) selectItemWeigthNum = 0;
            //if (selectItemWeigthNum + num < 0) selectItemWeigthNum = 2;
            if (selectItemWeigthNum + num >= int.MaxValue) selectItemWeigthNum = 0;

            selectItemWeigthNum += num;
            selectItemWeigthNum = Mathf.Clamp(selectItemWeigthNum, 0, 2);
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
                //����������ǂ�
                if (spinCount + num < 0)
                {
                    spinCount = 4;
                }
                if (spinCount + num > 4)
                {
                    spinCount = 0;
                }
                spinCount += num;
            }
        void ViewItemCursle()
        {
            Debug.Log($"haveItemName count = {haveItemName.Count} || number = {selectItemHighNum}");
            if (ItemSpriteDic.Count == 0)
            {
                itemImage.sprite = nullSpr;
            }
            else
            {
                Debug.Log($"itemSprteDic count = {ItemSpriteDic.Count} || number = {haveItemName[selectItemHighNum]}");
                itemImage.sprite = ItemSpriteDic[haveItemName[selectItemHighNum]];
            }
            if (selectItemWeigthNum % 2 == 0)
            {
                selectCursor.transform.position = itemImage.transform.position;
            }
            else
            {
                selectCursor.transform.position = tetrisImage.transform.position;
            }
        }
        void SynchroItem(Dictionary<ItemName, int> data)
        {
            Debug.Log("���񂭂�A�C�e�� "+data.Count());
            selectItemDic = data;

            haveItemName = new List<ItemName>(selectItemDic.Keys);
            //var query = haveItemName.OrderBy(x => x).Where(x => x > 0);
            for (int i = 0; i < selectItemDic.Count(); i++)
            {
                //�����ĂȂ��A�C�e���͍폜
                if (haveItemName[i] == 0) haveItemName.RemoveAt(i);
            }
            //�A�C�e���ԍ����ɕ��ёւ�
            haveItemName.Sort();
            selectHighMaxNum = haveItemName.Count();
        }
        void SynchroTetris(int num)
        {
            tetrisSelectCount = num;
        }
        void GenerateItem()
        {
            if (selectState == SelectState.Spin)
            {
                if (selectItemWeigthNum % 2 == 0)
                {
                    selectState = SelectState.Spin;
                    return;
                }
                generator.GenerateItem(haveItemName[selectItemWeigthNum], RobotObj.transform.position);
            }
            //��]���[�h������e�g���X����
            else if (selectState == SelectState.FirstSelect)
            {
                GenerateTetris();
            }
        }
        void GenerateTetris()
        {
            TetrisTypeEnum type = GetRandomTetrisType();
            //����
            generator.GenerateItem(type, (TetrisAngle)spinCount, FieldInfo.VecToFieldInfo(RobotObj.transform.position));

        }
        #region tetrisUtility
        TetrisTypeEnum oldTetrisType;
        TetrisTypeEnum GetRandomTetrisType()
        {
            //�e�[�u���ɉ��������Ă��Ȃ��ꍇ
            if (tetrisTable.Count() == 0)
            {
                //�e�[�u���𐶐����Ȃ���
                TetrisTableReflash();
            }
            //�����_���Ńe�g���~�m���쐬
            TetrisTypeEnum rand;
            while (true)
            {
                //�O��Ɠ����e�g���X�͏o���Ȃ�
                rand = (TetrisTypeEnum)Random.Range(0, tetrisTable.Count());
                if (rand != oldTetrisType) break;
            }
            //���X�g����폜
            tetrisTable.Remove(rand);
            oldTetrisType = rand;
            return rand;
        }
        void TetrisTableReflash()
        {
            for (int i = 0; i < 7; i++)
            {
                tetrisTable.Add((TetrisTypeEnum)i);
            }
        }
        /// <summary>
        /// �I��p�X�e�[�^�X
        /// </summary>
        private enum SelectState
        {
            FirstSelect, Spin
        }
        #endregion


    }
}

