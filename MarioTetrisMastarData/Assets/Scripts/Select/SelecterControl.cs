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
    [Space(20), SerializeField] GameObject guideObj;

    IGetItemBox getItemBox;
    IItemDataChange itemDataChange;
    IRemoveItems removeItems;
    ISelectInput selectInput;
    IGenerator generator;
    IGetTetrisInfo getTetrisInfo;

    //���݉���I�����Ă��邩�X�e�[�^�X
    SelectState selectState;
    //���݂̉��J�[�\���ԍ�
    int selectItemWeigthNum;
    //�l������������Ƃ��ɃJ�[�\���𓮂���
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
    //���݂̏c�J�[�\��
    int selectItemHighNum;
    //�A�C�e���̏c�̏���l
    int selectHighMaxNum;
    int spinCount;
    int tetCount;
    //���O�ɑΉ��������S���i�[
    Dictionary<ItemName, Sprite> ItemSpriteDic = new Dictionary<ItemName, Sprite>();
    //�A�C�e�����ǂꂮ�炢�����Ă��邩
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
    //UI�ɕ\�������ȏ�̃��X�g
    List<ItemName> ViewItemList = new List<ItemName>();
    //�e�g���X�����p���X�g
    List<TetrisTypeEnum> tetrisTable = new List<TetrisTypeEnum>();
    //�T���e�g���X
    TetrisTypeEnum[] RandomTetrises = new TetrisTypeEnum[3];

    //�G�Z�I�u�W�F�N�g�v�[���@�e�g���X�̃K�C�h
    private GameObject[] tetrisGuids = new GameObject[4];
    //���ɏo��K�C�h
    private GameObject[] tetrisUnderGuids = new GameObject[4];
    private void Awake()
    {
        Utility.Locator<ISelectedItem>.Bind(this);
    }
    void Start()
    {
        RobotObj = Utility_.robotObject;
        getItemBox = Utility.Locator<IGetItemBox>.GetT();
        itemDataChange = Utility.Locator<IItemDataChange>.GetT();
        itemDataChange.ChangeItemBoxValue += SynchroItem;
        itemDataChange.ChangeTetPieceValue += SynchroTetris;

        removeItems = Utility.Locator<IRemoveItems>.GetT();
        selectInput = Utility.Locator<ISelectInput>.GetT();
        selectInput.OnSelectButton += CursollMove;
        selectInput.MouceWhileEvent += CursollScroll;

        generator = Utility.Locator<IGenerator>.GetT();
        getTetrisInfo = Utility.Locator<IGetTetrisInfo>.GetT();

        TetrisTableReflash();
        //�ǂ�ȃe�g���X�����������ŏ���3����
        for (int i = 0; i < 3; i++)
        {
            RandomTetrises[i] = GetRandomTetrisType();
        }
        //�G�Z�I�u�W�F�N�g�v�[������
        for (int i = 0; i < 4; i++)
        {
            var temp1 = Instantiate(guideObj);
            temp1.transform.position = new Vector3(-100, -100, 0);
            var temp2 = Instantiate(guideObj);
            temp2.transform.position = new Vector3(-100, -100, 0);
            tetrisGuids[i] = temp1;
            tetrisUnderGuids[i] = temp2;
        }
    }
    private void Update()
    {
        Debug.LogWarning(RandomTetrises[0]);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectState == SelectState.Spin)
            {
                selectState = SelectState.Select;
            }
        }
        if (selectState == SelectState.Spin)
        {
            GenerateTetrisVision(RandomTetrises[0], (TetrisAngle)spinCount, RobotObj.transform.position);
        }
        else
        {
            TetrisVisionBye();
        }
    }

    void CursollMove(SelectButtonType buttonType)
    {
        //�A�C�e���I�𒆂̎�
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
                        //�����ł��鐔�ɖ����Ȃ��ꍇ���^�[��
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
        //��]���[�h�̎�
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
            /* GenerateTetrisVision*//*(RandomTetrises[0], (TetrisAngle)spinCount, RobotObj.transform.position);*/
        }
    }
    #region �e�g���X
    void SpinMino(int num)
    {
        spinCount += num;
        //����������ǂ�
        if (spinCount + num < 0)
        {
            spinCount = 3;
        }
        else if (spinCount + num > 4)
        {
            spinCount = 0;
        }

    }
    Field.FieldBase fieldBase;
    //���Ƃł��
    /// <summary>
    /// �e�g���X�̃K�C�h����
    /// </summary>
    /// <param name="tetrisType"></param>
    /// <param name="angle"></param>
    /// <param name="generatePos"></param>
    void GenerateTetrisVision(TetrisTypeEnum tetrisType, TetrisAngle angle, Vector3 generatePos)
    {
        fieldBase = Utility.Locator<Field.FieldBase>.GetT();
        FieldInfo info = FieldInfo.VecToFieldInfo(RobotObj.transform.position);
        List<FieldInfo> fieldInfos = new List<FieldInfo>();
        TetrisScriptableObject tetrisScriptable = getTetrisInfo.GetTetrimino(tetrisType, angle);
        for (int i = 3; i >= 0; i--)
        {
            for (int j = 0; j < 4; j++)
            {
                if (tetrisScriptable.tetriminoArrays[i, j])
                {
                    //�f�[�^��ǂ�Ŕz�����ѕς���
                    FieldInfo infomation;
                    infomation.width = j + info.width;
                    infomation.height = i + info.height;

                    fieldInfos.Add(infomation);
                }
            }
        }
        for (int i = 0; i < fieldInfos.Count; i++)
        {
            tetrisGuids[i].transform.position = FieldInfo.FieldInfoToVec(fieldInfos[i]);
        }
        //�����ɂ��\��
        generatedUnderVision(fieldInfos);
    }
    void TetrisVisionBye()
    {
        for (int i = 0; i < 4; i++)
        {
            tetrisGuids[i].transform.position = new Vector3(-100, 100, 0);
            tetrisUnderGuids[i].transform.position = tetrisGuids[i].transform.position;

        }
    }
    void generatedUnderVision(List<FieldInfo> infos)
    {
        List<FieldInfo> fieldInfos = Brock.LimitChecker(infos);

        for (int i = 0; i < fieldInfos.Count; i++)
        {
            tetrisUnderGuids[i].transform.position = FieldInfo.FieldInfoToVec(fieldInfos[i]);
        }
    }
    void GenerateTetris(TetrisTypeEnum tetrisType)
    {
        generator.GenerateItem(tetrisType, (TetrisAngle)spinCount, FieldInfo.VecToFieldInfo(RobotObj.transform.position));
        //�V�����e�g���X�̃f�[�^�𐶐�
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
        //�{�b�N�X�Ɍ��������Ƃ�`����
        removeItems.RemoveTetrisPiece(4);
        tetrisCountText.text = tetCount.ToString();
    }

    TetrisTypeEnum oldTetrisType;
    List<TetrisTypeEnum> GeneratedList = new List<TetrisTypeEnum>();

    /// <summary>
    /// �����_���ȃe�g���X���擾
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
            //�����_���Ő���
            rand = (TetrisTypeEnum)Random.Range(0, 7);
            //���̒l������ĂȂ����m�F
            if (!GeneratedList.Contains(rand) && oldTetrisType != rand)
            {
                //��x���������~�m���u���b�N���X�g�ɓo�^
                GeneratedList.Add(rand);
                oldTetrisType = rand;
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
    #region �K�C�h

    #endregion
    #region �A�C�e��
    void GenerateItem()
    {
        //Debug.LogWarning(selectItemHighNum);
        //generator.GenerateItem(ViewItem(selectItemDic)[selectItemHighNum], RobotObj.transform.position);
    }
    /// <summary>
    /// �A�C�e��Dic��������Ă��Ȃ��A�C�e������菜���ĕ��ѕς���@
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

    //�A�C�e���̌����ω������Ƃ��ɓ���������
    void SynchroItem(Dictionary<ItemName, int> data)
    {
        selectItemDic = data;
    }
    #endregion

    #region �J�[�\���ړ�
    /// <summary>
    /// �J�[�\���ړ�
    /// </summary>
    /// <param name="�J�[�\���ړ��p�J�E���g"></param>
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
        selectItemHighNum += num;
        if (selectItemHighNum + num > selectHighMaxNum)
        {
            selectItemHighNum = 0;
        }
        else if (selectItemHighNum + num < 0)
        {
            selectItemHighNum = selectHighMaxNum;
        }
        //else
        //{
        //}
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
